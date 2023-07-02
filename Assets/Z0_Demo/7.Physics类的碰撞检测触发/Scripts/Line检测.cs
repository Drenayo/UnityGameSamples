using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Z_7
{
    public class Line检测 : MonoBehaviour
    {
        public LineRenderer line;
        public Vector3 start;
        public Vector3 end;

        void Start()
        {

        }


        void Update()
        {
            start = line.GetPosition(0) + transform.position;
            end = line.GetPosition(1) + transform.position;
            if (Physics.Linecast(start, end))
            {
                Debug.Log("有碰撞！");
            }
        }
    }
}
