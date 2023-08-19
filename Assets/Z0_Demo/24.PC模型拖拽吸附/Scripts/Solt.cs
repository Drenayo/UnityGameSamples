using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_24
{
    public class Solt : MonoBehaviour
    {
        public string tag;
        public bool isNotNull = false;
        public DragItem item;
        void Start()
        {

        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<DragItem>(out item) && item.tag.Equals(tag))
            {
                isNotNull = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<DragItem>(out item) && item.tag.Equals(tag))
            {
                isNotNull = false;
            }
        }

        private void Update()
        {
            if (isNotNull)
            {
                item.transform.position = transform.position;
            }
        }
    }
}
