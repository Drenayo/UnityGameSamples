using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_14
{
    public class Two : StepBase
    {
        public override void StartStep()
        {
            base.StartStep();

            Debug.Log("我是二号！");
        }

        public override void NextStep()
        {

            base.NextStep();
        }
    }
}
