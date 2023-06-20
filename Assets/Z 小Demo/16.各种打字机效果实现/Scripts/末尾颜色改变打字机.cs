using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_16
{
    public class 末尾颜色改变打字机 : MonoBehaviour
    {
        public Text textTypeComponent;
        public string typeContent;
        public float typeTime;

        public Color defaultColor;  // 默认颜色
        public Color doneColor;     // 已完成颜色
        public Color currColor;     // 当前正在被打字的颜色



        void Start()
        {
            StartCoroutine(Typing());
        }

        IEnumerator Typing()
        {
            textTypeComponent.text = string.Empty;
            string strTemp = string.Empty;

            // 所有颜色都设置为默认值
            textTypeComponent.color = defaultColor;

            strTemp = GetStartColorStr(doneColor);
            for (int i = 0; i < typeContent.Length; i++)
            {
                yield return new WaitForSeconds(typeTime);


                strTemp += typeContent[i];

                // 最后赋值
                textTypeComponent.text = strTemp + endColorStr;
            }
        }

        // 结尾颜色标志
        private string endColorStr = "</color>";
        // 得到富文本的开头的颜色
        private string GetStartColorStr(Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{hexColor}>";
        }
    }
}


// 1圣1 安 地 列 斯 看 急 急 急 急 急 急<color=D44343>永和九年，岁在癸丑，暮春之初，会于会稽山阴之兰</color>

