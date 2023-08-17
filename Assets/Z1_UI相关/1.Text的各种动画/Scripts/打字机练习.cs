using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace XXX//typeWriter
{
    public class 打字机练习 : MonoBehaviour
    {
        public float typeSpacing;
        public string typeContent;
        public Text textComponent;
        public bool playOnAwake;
        public int endLength;
        public Color defaultColor;
        public Color endColor;
        public string EndColorRichText = "</color>";

        private void Start()
        {
            if (playOnAwake)
                StartTypeWriter();
        }


        public void StartTypeWriter()
        {
            StartCoroutine(StartTypeWriter_E());
        }

        IEnumerator StartTypeWriter_E()
        {
            textComponent.text = string.Empty;
            string strTemp = string.Empty;
            strTemp = GetStartColorRichText(defaultColor);
            for (int i = 0; i < typeContent.Length; i++)
            {
                yield return new WaitForSeconds(typeSpacing);
                strTemp += typeContent[i];
                // 判断
                int indexEndNumber = endLength + i + 1 <= typeContent.Length ? endLength : 0;
                // 赋值
                textComponent.text = strTemp + EndColorRichText + GetStartColorRichText(endColor) + typeContent.Substring(i + 1, indexEndNumber) + EndColorRichText;
            }
        }

        private string GetStartColorRichText(Color col)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(col)}>";
        }
    }
}
