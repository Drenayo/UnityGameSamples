using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace D005
{
    public class ButtonProgress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Image slider; // 进度条Slider
        public float progressSpeed = 0.5f; // 进度增加速度
        private bool isPress = false; // 是否按下
        private bool isDone = false;
        public UnityEvent doneEvent;
        private void Update()
        {
            if (isPress && !isDone)
            {
                IncrementProgress();
            }
            else if(!isDone)
            {
                DecrementProgress();
            }
        }

        // 当按钮被按下时触发
        public void OnPointerDown(PointerEventData eventData)
        {
            isPress = true;
        }

        // 当按钮被松开时触发
        public void OnPointerUp(PointerEventData eventData)
        {
            isPress = false;
        }

        // 增加进度条进度
        private void IncrementProgress()
        {
            slider.fillAmount += progressSpeed * Time.deltaTime;
            if (slider.fillAmount >= 1f)
            {
                slider.fillAmount = 1f;
                isDone = true;
                doneEvent.Invoke();
                Debug.Log("进度完成");
            }
        }

        // 减少进度条进度
        private void DecrementProgress()
        {
            slider.fillAmount -= progressSpeed * Time.deltaTime;
            if (slider.fillAmount <= 0f)
            {
                slider.fillAmount = 0f;
            }
        }
    }

}
