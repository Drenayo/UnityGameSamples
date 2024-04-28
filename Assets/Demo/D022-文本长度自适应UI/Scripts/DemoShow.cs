using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace D022
{
    public class DemoShow : MonoBehaviour
    {
        public float speed;

        public Text text1;
        public Text text2;

        public string text1Str;
        public string text2Str;

        void Start()
        {
            StartCoroutine(Typing(text1,speed,text1Str));
            StartCoroutine(Typing(text2,speed,text2Str));
        }

        IEnumerator Typing(Text text,float speed,string str)
        {
            text.text = string.Empty;
            string strTemp = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                yield return new WaitForSeconds(speed);
                strTemp += str[i];
                text.text = strTemp;
            }
        }
    }
}
