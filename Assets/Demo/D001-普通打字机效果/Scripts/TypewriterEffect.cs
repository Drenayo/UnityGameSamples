using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace D001
{
    public class TypewriterEffect : MonoBehaviour
    {
        [Header("文本组件")]
        public Text textTypeComponent;
        [Header("打字机内容"),TextArea]
        public string typeContent;
        [Header("打字停留间隔")]
        public float typeTime;


        void Start()
        {
            StartCoroutine(Typing());
        }

        IEnumerator Typing()
        {
            textTypeComponent.text = string.Empty;
            string strTemp = string.Empty;
            for (int i = 0; i < typeContent.Length; i++)
            {
                yield return new WaitForSeconds(typeTime);
                strTemp += typeContent[i];
                textTypeComponent.text = strTemp;
            }
        }
    }
}
