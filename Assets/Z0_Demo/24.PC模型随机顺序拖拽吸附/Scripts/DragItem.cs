using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_24
{
    public class DragItem : MonoBehaviour
    {
        public string tag;
        public Vector3 startPos;

        void Start()
        {
            startPos = transform.position;    
        }


        public void SetStartPos()
        {
            Debug.Log(tag + "归为");
            transform.position = startPos;
        }
    }
}
