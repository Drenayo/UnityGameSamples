using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z2_5
{
    public class TipsPanel : MonoBehaviour
    {
        private GameObject tipsText;
        void Start()
        {
            tipsText = GetComponentInChildren<UnityEngine.UI.Text>().gameObject;
        }

        public float times;
        public void SetText(string text)
        {
            tipsText.GetComponent<Text>().text = text;
            tipsText.SetActive(true);
            StartCoroutine(Delay_Display());
        }

        IEnumerator Delay_Display()
        {
            yield return new WaitForSeconds(times);
            tipsText.SetActive(false);
        }
    }
}
