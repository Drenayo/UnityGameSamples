using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFrame1
{
    public static class GameExtend
    {
        public static void PosSwitch(this GameObject gameObject,Transform tran)
        {
            gameObject.transform.position = tran.position;
        }
    }
}
