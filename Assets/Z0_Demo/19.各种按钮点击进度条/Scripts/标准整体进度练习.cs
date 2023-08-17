using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Z_19
{
    public class 标准整体进度练习 : MonoBehaviour
    {
        public Image bgImage;
        public float speed;
        public bool isDone;
        public UnityEvent doneEvent;
        public bool IsHover { get; set; }
        public bool IsPress { get; set; }


        private void Update()
        {
            if (IsHover && IsPress && !isDone)
            {
                bgImage.fillAmount += Time.deltaTime * speed;
                if (bgImage.fillAmount >= 1)
                {
                    isDone = true;
                    doneEvent.Invoke();
                }
            }
            else if (bgImage.fillAmount > 0 && !isDone)
            {
                bgImage.fillAmount -= Time.deltaTime * speed;
            }
        }
    }
}
