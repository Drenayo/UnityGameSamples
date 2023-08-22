using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GSFrame
{
    public class StepBase : Mono
    {
        [SerializeField]
        protected int stepIndex;
        [SerializeField]
        protected string stepName;
        private int nextStepIndex;

        // 开始执行
        public virtual void StartStep()
        {
            Init();
        }

        // 关闭步骤
        public virtual void CloseStep()
        {
            Debug.Log($"[{stepName}]-->[{stepMgr.GetStep(stepIndex + 1).stepName}] 已切换");
        }


        // 顺序执行
        protected void NextStep(float delayTime = 0)
        {
            timerMgr.StartTimer("顺序执行下一步计时", delayTime, NextStepTrigger);
        }
        private void NextStepTrigger(GameObject gameObj, int param)
        {
            CloseStep();
            stepMgr.GetStep(stepIndex + 1).StartStep();
        }

        // 指定执行下一个步骤
        protected void ChooseNextStep(int nextIndex, float delayTime = 0)
        {
            nextStepIndex = nextIndex;
            timerMgr.StartTimer("指定执行下一步计时", delayTime, ChooseNextStepTrigger);
        }
        private void ChooseNextStepTrigger(GameObject gameObj, int param)
        {
            CloseStep();
            stepMgr.GetStep(nextStepIndex).StartStep();
        }
    }
}
