using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GSFrame
{
    public class StepManager : Mono
    {
        public static StepManager Instance;
        protected void Awake()
        {
            Instance = this;
            Init();
        }

        [Header("最开始执行的步骤")]
        public int startStepIndex;
        [SerializeField]
        private List<StepBase> stepList;

        public void Start()
        {
            // 执行步骤
            stepList[startStepIndex].StartStep();
        }

        public StepBase GetStep(int index)
        {
            return stepList[index];
        }
    }
}
