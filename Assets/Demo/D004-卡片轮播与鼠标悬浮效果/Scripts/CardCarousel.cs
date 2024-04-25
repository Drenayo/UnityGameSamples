using System.Collections.Generic;
using UnityEngine;

namespace D004
{
    public class CardCarousel : MonoBehaviour
    {
        public float startOffset = -500f;
        public float endOffset = 500f;
        [Header("卡片间距")]
        public float cardSpacing = 200f;
        public bool isPlaying = true;
        [Header("速度")]
        public float slideSpeed = 100f;
        [Header("方向控制")]
        public bool slideToLeft = true;

        private List<RectTransform> imageList;

        private void Start()
        {
            imageList = new List<RectTransform>();
            float currentXPosition = startOffset; // 用于追踪每个卡片的当前 X 位置
            foreach (RectTransform child in transform)
            {
                imageList.Add(child);
                child.anchoredPosition = new Vector2(currentXPosition, 0f); // 设置卡片的初始位置
                currentXPosition += cardSpacing; // 将当前位置增加间距，为下一个卡片预留空间
            }
        }

        private void Update()
        {
            if (isPlaying)
            {
                foreach (var image in imageList)
                {
                    float slideDirection = slideToLeft ? -1f : 1f;
                    image.anchoredPosition += new Vector2(slideDirection, 0f) * Time.deltaTime * slideSpeed;
                    if (slideToLeft && image.anchoredPosition.x < startOffset)
                    {
                        image.anchoredPosition = new Vector2(endOffset, 0f);
                    }
                    else if (!slideToLeft && image.anchoredPosition.x > endOffset)
                    {
                        image.anchoredPosition = new Vector2(startOffset, 0f);
                    }
                }
            }
        }
    }
}
