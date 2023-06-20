using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_14
{
    public class OnClickEventTestOne : Mono
    {
        void Start()
        {

        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                eventCenter.Trigger("点击");
            }
        }
    }
}
