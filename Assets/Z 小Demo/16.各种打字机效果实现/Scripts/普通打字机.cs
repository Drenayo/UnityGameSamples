using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_16
{
    public class 普通打字机 : MonoBehaviour
    {
        public Text textTypeComponent;
        public string typeContent;
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
