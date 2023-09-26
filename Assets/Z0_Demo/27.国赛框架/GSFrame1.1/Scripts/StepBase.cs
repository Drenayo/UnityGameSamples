using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GSFrame1
{
    public class StepBase : MonoBehaviour
    {
        
        [Header("开始事件")]
        public UnityEvent startEvent;
        [Header("结束事件")]
        public UnityEvent endEvent;
        [Header("下一步执行")]
        public StepBase nextStep;

        // 开始执行
        public void StartStep()
        {
            startEvent?.Invoke();
        }

        // 关闭步骤
        public void CloseStep()
        {
            endEvent?.Invoke();
            Debug.Log($"<color=yellow>[{gameObject.name}]-->[{nextStep.gameObject.name}] 已切换</color>");
        }
    }
}
