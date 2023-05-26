using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XXX
{
    public class PhysicalTest : MonoBehaviour
    {
        public Vector3 boxCenter;
        public Vector3 boxSize;
        public Vector3 boxDirection;
        public float maxDistance;
        public LayerMask layerMask;
        void Start()
        {

        }


        void Update()
        {
            RaycastHit hitInfo;
            if (Physics.BoxCast(boxCenter, boxSize / 2, boxDirection, out hitInfo, Quaternion.identity, maxDistance, layerMask))
            {
                Debug.Log(hitInfo.collider.name);
            }

            Debug.DrawRay(boxCenter, boxDirection * maxDistance, Color.green);
            Debug.DrawRay(boxCenter + Quaternion.identity * new Vector3(boxSize.x / 2f, boxSize.y / 2f, 0f), Quaternion.identity * new Vector3(0f, 0f, boxSize.z), Color.red);
            Debug.DrawRay(boxCenter + Quaternion.identity * new Vector3(-boxSize.x / 2f, boxSize.y / 2f, 0f), Quaternion.identity * new Vector3(0f, 0f, boxSize.z), Color.red);
            Debug.DrawRay(boxCenter + Quaternion.identity * new Vector3(boxSize.x / 2f, -boxSize.y / 2f, 0f), Quaternion.identity * new Vector3(0f, 0f, boxSize.z), Color.red);
            Debug.DrawRay(boxCenter + Quaternion.identity * new Vector3(-boxSize.x / 2f, -boxSize.y / 2f, 0f), Quaternion.identity * new Vector3(0f, 0f, boxSize.z), Color.red);
        }
    }
}
