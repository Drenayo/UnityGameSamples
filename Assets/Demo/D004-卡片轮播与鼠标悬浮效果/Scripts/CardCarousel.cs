using System.Collections.Generic;
using UnityEngine;

namespace D004
{
    public class CardCarousel : MonoBehaviour
    {
        public float startOffset = -500f;
        public float endOffset = 500f;
        public List<RectTransform> imageList;
        public bool isPlaying = true;
        public float slideSpeed = 100f;
        public bool slideToLeft = true;

        private void Start()
        {
            imageList = new List<RectTransform>();
            foreach (RectTransform child in transform)
            {
                imageList.Add(child);
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
