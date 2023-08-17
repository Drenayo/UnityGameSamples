using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace XXX
{
    public class Drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        public string pTag;
        public UnityEvent doneEvent;
        [HideInInspector]
        public Transform item;
        private Transform startParent;
        private Vector3 startPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            item = eventData.pointerDrag.transform;
            startParent = item.parent;
            startPos = item.position;
            item.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            item.position = Input.mousePosition;
            item.SetAsLastSibling();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (item.parent == startParent)
            {
                item.position = startPos;
            }
        }

        void Start()
        {
            gameObject.AddComponent<CanvasGroup>();
            gameObject.AddComponent<EventTrigger>();
        }



    }
}
