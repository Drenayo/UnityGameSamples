using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace D011
{
    public class DragHandle : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
    {
        public string itemTag;

        private Vector3 startPos;
        private Transform startParent;
        private Transform item;
        void Start()
        {
            gameObject.AddComponent<CanvasGroup>();
            gameObject.AddComponent<EventTrigger>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            item = eventData.pointerPress.transform;

            startPos = item.position;
            startParent = item.parent;
            
            item.parent.SetAsLastSibling();
            item.parent.parent.SetAsLastSibling();

            SetCanvasGroup(1f, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            item.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetCanvasGroup(1f, true);

            if (item.parent == startParent)
            {
                item.position = startPos;
            }
        }

        // 设置自身UI不要遮挡射线，保证插槽UIDrop事件能被触发
        public void SetCanvasGroup(float alpha,bool blockRaycasts)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = blockRaycasts;
        }
    }
}
