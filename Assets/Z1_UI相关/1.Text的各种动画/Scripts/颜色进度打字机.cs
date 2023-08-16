using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_16
{
    public class 颜色进度打字机 : MonoBehaviour
    {
        public Text textTypeComponent;
        public string typeContent;
        public float typeTime;

        public Color defaultColor;  // 默认颜色
        public Color doneColor;     // 已完成颜色

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

                // 组合的思想
                // <> + 已经打出的字 <> + <> 末尾未打出的字的剩余 <>
                textTypeComponent.text = strTemp + endColorStr + GetStartColorStr(defaultColor) + typeContent.Substring(i + 1, typeContent.Length - (i + 1)) + endColorStr;
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
