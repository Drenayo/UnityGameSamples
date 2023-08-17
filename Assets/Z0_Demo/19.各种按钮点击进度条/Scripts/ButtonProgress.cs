using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Z_19
{
    public class ButtonProgress : MonoBehaviour
    {
        public bool IsHover { get; set; }
        public bool IsPress { get; set; }
        public bool isDone;

        public float speed;
        public Image image;
        public UnityEvent 完成事件;

        private void Update()
        {
            if (IsHover && IsPress &&!isDone)
            {
                image.fillAmount += Time.deltaTime * speed;
                if (image.fillAmount >= 1)
                {
                    完成事件.Invoke();
                    isDone = true;
                }
            }
            else if (image.fillAmount > 0 && !isDone)
            {
                image.fillAmount -= Time.deltaTime * speed;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                IsPress = false;
            }
        }

        public void AAA()
        {
            Debug.Log("A");
        }
    }
}
