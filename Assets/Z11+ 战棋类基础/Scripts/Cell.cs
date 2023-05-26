using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z11
{
    public class Cell : MonoBehaviour
    {
        public Material originalMaterial;
        public Material highlightedMaterial;
        public Role currRole;
        public Transform rolePos;

        void Start()
        {
            rolePos = transform.Find("RolePos");
        }


        void Update()
        {

        }

        // 设置高亮
        public void SetHighlight(bool isHighLight)
        {
            if (isHighLight)
                GetComponent<MeshRenderer>().material = highlightedMaterial;
            else
                GetComponent<MeshRenderer>().material = originalMaterial;
        }

        
    }
}
