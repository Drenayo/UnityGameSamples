using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace D002
{
    public class ColorProgressTypewriter : MonoBehaviour
    {
        public Text textTypeComponent;
        [Header("打字机文本"),TextArea]
        public string typeContent;
        [Header("打字时间间隔")]
        public float typeTime;
        public Color defaultColor; 
        public Color doneColor;

        // 结尾颜色标志
        private string endColorStr = "</color>";

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

        // 得到富文本的开头的颜色
        private string GetStartColorStr(Color color)
        {
            // 用于从Color转为富文本支持的十六进制颜色值
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{hexColor}>";
        }
    }
}
