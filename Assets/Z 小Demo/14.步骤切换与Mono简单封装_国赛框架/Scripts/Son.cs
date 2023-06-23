using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XXX
{
    public class Son : TestClass
    {
        public static Son Instance;
        public override void Awake()
        {
            Debug.Log("执行zi");
            Instance = this;
            base.Awake();
        }
    }
}
