using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GSFrame1
{
    public class StepManager : MonoBehaviour
    {
        public static StepManager Instance;
        protected void Awake()
        {
            Instance = this;
        }

        [Header("最开始执行的步骤")]
        public StepBase startStep;

        [Header("默认启用禁用列表")]
        public UnityEvent defaultActiveList;

        public void Start()
        {
            defaultActiveList?.Invoke();
            startStep.StartStep();
            Debug.Log(startStep.gameObject.name + "已启动！");
        }
    }
}
