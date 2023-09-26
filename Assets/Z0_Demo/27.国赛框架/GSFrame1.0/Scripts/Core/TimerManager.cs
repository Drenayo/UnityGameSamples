using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GSFrame
{
    public class TimerManager : Mono
    {
        public static TimerManager Instance;
        protected void Awake()
        {
            Instance = this;
            Init();
        }

        public void StartTimer(string timerName, float timerTime, EventManager.processEvent timerEvent)
        {
            // 注册事件
            eventMgr.Regist(timerName, timerEvent);
            // 启用协程
            StartCoroutine(TimerE(timerName, timerTime));
        }
        IEnumerator TimerE(string timerName, float timerTime)
        {
            yield return new WaitForSeconds(timerTime);
            eventMgr.Trigger(timerName);
        }


        // 停止所有计时器
        public void ClearAllTimer()
        {
            StopAllCoroutines();
        }
    }
}
