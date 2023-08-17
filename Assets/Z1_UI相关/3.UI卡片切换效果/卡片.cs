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

        // 小左小、小右小、小左大（右中）、小右大（左中）、大左小（中左）、大右小（中右）


        public void TurnLeft()
        {
            switch (cardPos)
            {
                case CardPos.中:
                    animator.SetTrigger("大左小");
                    cardPos = CardPos.近左;
                    break;
                case CardPos.近左:
                    animator.SetTrigger("小左小");
                    cardPos = CardPos.远左;
                    break;
                case CardPos.近右:
                    animator.SetTrigger("小左大");
                    cardPos = CardPos.中;
                    break;
                case CardPos.远左: // 循环Bug
                    animator.SetTrigger("小左小");
                    cardPos = CardPos.远左;
                    break;
                case CardPos.远右:
                    animator.SetTrigger("小左小");
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
                    animator.SetTrigger("大右小");
                    cardPos = CardPos.近右;
                    break;
                case CardPos.近左:
                    animator.SetTrigger("小右大");
                    cardPos = CardPos.中;
                    break;
                case CardPos.近右:
                    animator.SetTrigger("小右小");
                    cardPos = CardPos.远右;
                    break;
                case CardPos.远左:
                    animator.SetTrigger("小右小");
                    cardPos = CardPos.近左;
                    break;
                case CardPos.远右:
                    animator.SetTrigger("小右小");
                    cardPos = CardPos.远右;
                    break;
                default:
                    break;
            }
        }
    }
}
