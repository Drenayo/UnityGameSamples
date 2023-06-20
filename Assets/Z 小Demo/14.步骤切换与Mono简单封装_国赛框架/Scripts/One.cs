using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z_14
{
    public class One : StepBase
    {
        public GameObject cube;
        public GameObject[] list;
        public override void StartStep()
        {
            base.StartStep();

            Debug.Log(FindGameObject("Cube")==null);
            //SetActive(cube, true);
            ////SetActive(cube, false, 6);// 这里的六秒指的是Start开始执行到第六秒，不是上一个三秒结束再执行六秒
            //StartCoroutine(AA());
        }

        IEnumerator AA()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(1);
                if (i == 2)
                {
                    SwitchStepByIndex(2, 5);
                    yield break;
                }
                Debug.Log(i);
            }
        }


        public override void CloseStep()
        {
            Debug.Log("关闭啦！");
            base.CloseStep();
        }
    }
}
