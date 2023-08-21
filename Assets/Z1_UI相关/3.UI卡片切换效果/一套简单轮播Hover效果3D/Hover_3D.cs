using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Z1_3
{
    public class Hover_3D : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.pointerEnter.GetComponent<Animator>().Play("放大");
            eventData.pointerEnter.transform.parent.GetComponent<CycleImg_3D>().isPlay = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.pointerEnter.GetComponent<Animator>().Play("缩小");
            eventData.pointerEnter.transform.parent.GetComponent<CycleImg_3D>().isPlay = true;
        }

        void Start()
        {
            
        }


        void Update()
        {
            
        }
    }
}
