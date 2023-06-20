using System;
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
            eventCenter.Regist("点击", RightClick);



            //SetActive(cube, true);
            ////SetActive(cube, false, 6);// 这里的六秒指的是Start开始执行到第六秒，不是上一个三秒结束再执行六秒
            //StartCoroutine(AA());
        }

        private void RightClick(UnityEngine.Object obj, int param)
        {
            Debug.Log("右键点击！");
            NextStep();
        }

        public override void CloseStep()
        {
            Debug.Log("关闭啦！");
        }
    }
}
