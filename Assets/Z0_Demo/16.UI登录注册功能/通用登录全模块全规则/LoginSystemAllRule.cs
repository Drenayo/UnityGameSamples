using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_16
{
    public class LoginSystemAllRule : MonoBehaviour
    {
        public static LoginSystemAllRule Instance;
        public Dictionary<string, string> dicUserInfo = new Dictionary<string, string>();
        public GameObject gameObj_ErrorTipsPanel;
        public Text text_ErrorTipsPanel;
        public float errorTipsPanelHideTime;
        public void Awake()
        {
            Instance = this;
        }

        public void ShowErrorTipsPanel(string str)
        {
            gameObj_ErrorTipsPanel.SetActive(true);
            text_ErrorTipsPanel.text = str;
        }
    }
}
