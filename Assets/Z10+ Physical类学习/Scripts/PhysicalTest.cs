using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XXX
{
    public class PhysicalTest : MonoBehaviour
    {
        public BoxCollider boxCollider;
        public float maxDistance;
        RaycastHit hit;
        void Update()
        {
            if (Physics.BoxCast(transform.position, boxCollider.size / 2, transform.forward, out hit, transform.rotation, maxDistance))
            {
                Debug.Log("碰撞！" + hit.collider.name);
            }
        }
    }
}
