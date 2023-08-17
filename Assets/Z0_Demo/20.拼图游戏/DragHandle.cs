using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Z_20
{
    public class DragHandle : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
    {
        public static GameObject item;
        public Vector3 startPos;
        public Transform startParent;

        void Start()
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            item = eventData.pointerPress;
            startPos = item.transform.position;
            startParent = item.transform.parent;

            SetCanvasGroup(.5f, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            item.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetCanvasGroup(1f, true);

            if (item.transform.parent == startParent)
            {
                item.transform.position = startPos;
            }
        }

        // 设置自身UI不要遮挡射线，保证插槽UIDrop事件能被触发
        public void SetCanvasGroup(float alpha,bool blockRaycasts)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = blockRaycasts;
        }
    }
}
