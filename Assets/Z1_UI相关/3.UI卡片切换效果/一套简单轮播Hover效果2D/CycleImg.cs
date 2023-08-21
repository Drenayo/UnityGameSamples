using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Z1_3
{
    public class CycleImg : MonoBehaviour
    {
        public float startPos = -500;
        private float endPos = 500;

        public Transform cardParentTran;
        public List<RectTransform> cardList;

        public bool isPlay = true;
        public float speed;
        void Start()
        {
            foreach (RectTransform cardSon in cardParentTran)
            {
                cardList.Add(cardSon);
            }
        }


        void Update()
        {
            if (isPlay)
            {
                foreach (var item in cardList)
                {
                    item.anchoredPosition += new Vector2(-1, 0) * Time.deltaTime * speed;
                    if (item.anchoredPosition.x < startPos)
                    {
                        item.anchoredPosition = new Vector3(endPos, 0);
                    }
                }
            }
        }
    }
}
