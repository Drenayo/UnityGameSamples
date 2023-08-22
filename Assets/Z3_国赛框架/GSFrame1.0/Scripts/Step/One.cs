using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GSFrame;
namespace Z3
{
    public class One : StepBase
    {
        public GameObject cube1;
        public GameObject cube2;
        public bool isWord;
        public bool isRot;
        public override void StartStep()
        {
            base.StartStep();
            nodeMgr.SwitchPosition(cube1.transform, cube2.transform.localPosition, isWord);
            nodeMgr.SwitchRotation(cube1.transform, cube2.transform.localRotation, isWord);

            NextStep(3);
        }

        public override void CloseStep()
        {

            base.CloseStep();
        }
    }
}



