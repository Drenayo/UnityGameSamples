using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Z1_3
{
    public class Hover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.pointerEnter.GetComponent<Animator>().Play("放大");
            eventData.pointerEnter.transform.parent.GetComponent<CycleImg>().isPlay = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.pointerEnter.GetComponent<Animator>().Play("缩小");
            eventData.pointerEnter.transform.parent.GetComponent<CycleImg>().isPlay = true;
        }

        void Start()
        {
            
        }


        void Update()
        {
            
        }
    }
}
