using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_14
{
    public class EventManager
    {
        private static EventManager instance;
        public static EventManager GetInstance()
        {
            if (instance == null)
                instance = new EventManager();

            return instance;
        }

        // 声明委托
        public delegate void processEvent(GameObject obj, int param);

        // 声明字典集合 存储指令与事件的映射
        private Dictionary<string, processEvent> eventMap = new Dictionary<string, processEvent>();

        public void Regist(string name, processEvent func)
        {
            if (eventMap.ContainsKey(name))
                eventMap[name] += func;
            else
                eventMap[name] = func;
        }

        public void UnRegist(string name, processEvent func)
        {
            if (eventMap.ContainsKey(name))
                eventMap[name] -= func;
        }

        public void Trigger(string name, GameObject obj = null, int param = 0)
        {
            if (eventMap.ContainsKey(name))
                eventMap[name].Invoke(obj, param);
        }
    }
}
