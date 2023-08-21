using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_24
{
    public class Solt : MonoBehaviour
    {
        public string tag;
        public DragItem soltItem;

        void Start()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            if (!soltItem && other.gameObject.TryGetComponent<DragItem>(out DragItem item) && item.tag.Equals(tag))
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Debug.Log("鼠标抬起");
                    soltItem = item;
                    FixOrder fixOrder = FixOrder.Instance;
                    if (fixOrder.orderList[fixOrder.fixNumber].Equals(soltItem.tag))
                    {
                        soltItem.transform.position = transform.position;
                        fixOrder.fixNumber++;
                    }
                    else
                    {
                        fixOrder.fixNumber =0;
                        fixOrder.RestetFix();
                    }

                }
            }
        }
    }
}
