using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z1_3
{
    public class 卡片切换控制 : MonoBehaviour
    {
        // 遍历播放所有卡片的动画

        public List<卡片> cardList;


        public void Btn_Right()
        {
            foreach (卡片 card in cardList)
            {
                card.TurnRight();
            }
        }

        public void Btn_Left()
        {
            foreach (卡片 card in cardList)
            {
                card.TurnLeft();
            }
        }
    }
}
