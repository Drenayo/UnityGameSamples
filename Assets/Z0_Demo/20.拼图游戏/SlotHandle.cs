using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Z_20
{
    public class SlotHandle : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && transform.childCount == 0)
            {
                DragHandle.item.transform.SetParent(transform);
                DragHandle.item.transform.position = transform.position;
            }
        }

        void Start()
        {

        }
    }
}
