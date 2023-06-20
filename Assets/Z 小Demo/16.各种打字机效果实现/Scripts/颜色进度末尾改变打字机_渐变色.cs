using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Z_16
{
    public class 颜色进度末尾改变打字机_渐变色 : MonoBehaviour
    {
        public TMP_Text textTypeComponent;
        public string typeContent;
        public float typeTime;

        public Color currColor;  // 默认颜色
        public Color doneColor;     // 已完成颜色
        public int endColorChangeLength; // 末尾颜色有变化的长度
        void Start()
        {
            StartCoroutine(Typing());
        }

        IEnumerator Typing()
        {
            textTypeComponent.text = string.Empty;
            string strTemp = string.Empty;

            strTemp = GetStartColorStr(doneColor);
            for (int i = 0; i < typeContent.Length; i++)
            {
                yield return new WaitForSeconds(typeTime);
                strTemp += typeContent[i];

                // 组合的思想
                // <> + 已经打出的字 <> + <> 需要显示几个字的不同颜色变化（记得判定结尾是否越界） <>
                int endChangeLength = endColorChangeLength + 1 + i <= typeContent.Length ? endColorChangeLength : 0;
                textTypeComponent.text = strTemp + endColorStr + GetStartColorStr(currColor) + typeContent.Substring(i + 1, endChangeLength) + endColorStr;
            }
        }

        // 结尾颜色标志
        private string endColorStr = "</color>";
        // 得到富文本的开头的颜色
        private string GetStartColorStr(Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{hexColor}>";
        }
    }
}
