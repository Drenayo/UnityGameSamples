using UnityEngine;
using UnityEngine.EventSystems;

namespace D004
{
    public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator animator;
        private CardCarousel cycleCtrl;

        private void Start()
        {
            animator = GetComponent<Animator>();
            cycleCtrl = transform.parent.GetComponent<CardCarousel>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            animator.Play("放大");
            cycleCtrl.isPlaying = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            animator.Play("缩小");
            cycleCtrl.isPlaying = true;
        }
    }
}
