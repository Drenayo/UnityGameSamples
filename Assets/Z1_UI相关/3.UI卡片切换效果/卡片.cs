using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z1_3
{
    public enum CardPos
    {
        中,
        近左,
        近右,
        远左,
        远右
    }

    public class 卡片 : MonoBehaviour
    {
        public CardPos cardPos;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        public void TurnLeft()
        {
            switch (cardPos)
            {
                case CardPos.中:
                    animator.Play("中左");
                    cardPos = CardPos.近左;
                    break;
                case CardPos.近左:
                    animator.Play("左左");
                    cardPos = CardPos.远左;
                    break;
                case CardPos.近右:
                    animator.Play("右中");
                    cardPos = CardPos.中;
                    break;
                case CardPos.远左:
                    animator.Play("左左");
                    cardPos = CardPos.远左;
                    break;
                case CardPos.远右:
                    animator.Play("右左");
                    cardPos = CardPos.近右;
                    break;
                default:
                    break;
            }
        }

        public void TurnRight()
        {
            switch (cardPos)
            {
                case CardPos.中:
                    animator.Play("中右");
                    cardPos = CardPos.近右;
                    break;
                case CardPos.近左:
                    animator.Play("左中");
                    cardPos = CardPos.中;
                    break;
                case CardPos.近右:
                    animator.Play("右右");
                    cardPos = CardPos.远右;
                    break;
                case CardPos.远左:
                    animator.Play("左右");
                    cardPos = CardPos.近左;
                    break;
                case CardPos.远右:
                    animator.Play("右右");
                    cardPos = CardPos.远右;
                    break;
                default:
                    break;
            }
        }
    }
}
