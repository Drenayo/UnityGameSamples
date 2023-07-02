using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z_14
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
        }

        public override void CloseStep()
        {

            base.CloseStep();
        }
    }
}



