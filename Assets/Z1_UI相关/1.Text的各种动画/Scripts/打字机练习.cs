using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XXX//typeWriter
{
    public class 打字机练习 : MonoBehaviour
    {
        public float typeSpacing;
        public string typeContent;
        public Text textComponent;

        public Color endColor;
        public Color defaultColor;

        public int endLength;

        public bool playOnAwake;

        private string EndColorRichText = "</color>";

        public void Start()
        {
            if (playOnAwake)
            {
                StartTypeWriter();
            }
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
                int endIndex = endLength + i + 1 <= typeContent.Length ? endLength : 0;
                textComponent.text = strTemp + EndColorRichText + GetStartColorRichText(endColor) + typeContent.Substring(i + 1, endIndex) + EndColorRichText;
            }
        }

        private string GetStartColorRichText(Color col)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGB(col) + ">";
        }
    }
}
