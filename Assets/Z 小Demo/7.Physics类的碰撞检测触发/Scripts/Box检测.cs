using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_7
{
    public class PhysicalTest : MonoBehaviour
    {
        public Collider[] colliderList;
        public BoxCollider boxCollider;
        public Text text; //输出所有重叠到对象名称
        void Update()
        {
            colliderList = Physics.OverlapBox(transform.position, boxCollider.size / 2, transform.rotation);

            string outStr = string.Empty;
            foreach (Collider collider in colliderList)
            {
                outStr += collider.gameObject.name + "\n";
            }
            text.text = outStr;
            outStr = string.Empty;
        }
    }
}

