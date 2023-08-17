using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XXX
{
    public class Slot : MonoBehaviour,IDropHandler
    {
        public string pTag;
        public static int numberAll = 0;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.gameObject.TryGetComponent<Drag>(out Drag drag))
            {
                if (drag.pTag.Equals(pTag))
                {
                    drag.item.SetParent(transform);
                    drag.item.position = transform.position;
                    numberAll++;
                    if (numberAll == 4)
                    {
                        drag.doneEvent.Invoke();
                    }
                }
            }
        }

        void Start()
        {
            gameObject.AddComponent<EventTrigger>();    
        }
    }
}
