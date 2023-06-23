using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XXX
{
    public class TestClass : MonoBehaviour
    {
        public Son son;
        public virtual void Awake()
        {
            son = Son.Instance;
            Debug.Log("执行父");
        }
    }


}
