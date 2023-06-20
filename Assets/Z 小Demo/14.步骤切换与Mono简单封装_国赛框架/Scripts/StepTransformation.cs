using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Z_14
{
    /// <summary>
    ///  步骤切换器 方便切换不同步骤的执行顺序，便于调试和管理
    /// </summary>
    public class StepTransformation : MonoBehaviour
    {
        public static StepTransformation Instance;
        public void Awake()
        {
            Instance = this;
        }

        [Header("最开始执行的步骤")]
        public int startStepIndex;

        public List<StepBase> stepList;

        public void Start()
        {
            // 执行步骤
            stepList[startStepIndex - 1].StartStep();
        }


    }

    public class StepBase : Mono
    {
        /// <summary>
        /// 从1开始编号
        /// </summary>
        public int stepIndex;
        public string stepName;

        // 开始执行
        public virtual void StartStep() 
        {
            Debug.Log($"[{stepName}]开启！");
        }

        // 关闭步骤
        public virtual void CloseStep()
        {

        }


        // 顺序执行
        public virtual void NextStep()
        {
            CloseStep();
            Debug.Log($"[{stepName}]关闭!");
            StepTransformation.Instance.stepList[stepIndex].StartStep();
        }

        // 顺序延迟执行
        public virtual void NextStep(float times)
        {
            StartCoroutine(NextStepDelay(times));
        }
        IEnumerator NextStepDelay(float times)
        {
            yield return new WaitForSeconds(times);
            CloseStep();
            Debug.Log($"[{stepName}]延迟关闭!");
            StepTransformation.Instance.stepList[stepIndex].StartStep();
            yield break;
        }


        // 指定执行
        public virtual void SwitchStepByIndex(int index)
        {
            CloseStep();
            Debug.Log($"切换到序号[{index}]的步骤!");
            StepTransformation.Instance.stepList[index-1].StartStep();
        }


        // 延迟指定执行
        public virtual void SwitchStepByIndex(int index,float times)
        {
            StartCoroutine(SwitchStepByIndexDelay(index,times));
        }
        IEnumerator SwitchStepByIndexDelay(int index, float times)
        {
            yield return new WaitForSeconds(times);
            CloseStep();
            Debug.Log($"延迟切换到序号[{index}]的步骤!");
            StepTransformation.Instance.stepList[index - 1].StartStep();
            yield break;
        }
    }
}
