using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace D003
{
    public class EndColorChangeTypewriter : MonoBehaviour
    {
        public Text textTypeComponent;
        [Header("打字机文本"), TextArea]
        public string typeContent;
        [Header("打字时间间隔")]
        public float typeTime;

        public Color currColor;  // 默认颜色
        public Color doneColor;     // 已完成颜色
        [Header("末尾颜色变化的长度")]
        public int endColorChangeLength;

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

            strTemp = GetStartColorStr(doneColor);
            for (int i = 0; i < typeContent.Length; i++)
            {
                yield return new WaitForSeconds(typeTime);
                strTemp += typeContent[i];

                // <> + 已经打出的字 <> + <> 需要显示几个字的不同颜色变化（记得判定结尾是否越界） <>
                int endChangeLength = endColorChangeLength + 1 + i <= typeContent.Length ? endColorChangeLength : 0;
                textTypeComponent.text = strTemp + endColorStr + GetStartColorStr(currColor) + typeContent.Substring(i + 1, endChangeLength) + endColorStr;
            }
        }

        // 得到富文本的开头的颜色
        private string GetStartColorStr(Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{hexColor}>";
        }
    }
}
