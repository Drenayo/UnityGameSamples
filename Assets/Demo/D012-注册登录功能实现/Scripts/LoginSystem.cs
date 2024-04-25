using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D012
{
    public class LoginSystem : MonoBehaviour
    {
        public static LoginSystem Instance;
        public Dictionary<string, string> dicUserInfo = new Dictionary<string, string>();
        public GameObject errorTipsPanel;
        public Text text_ErrorTips;
        [Header("错误面板显示时间")]
        public float tipsPanelShowTime = 1;
        public void Awake()
        {
            Instance = this;
        }

        public void ShowErrorTipsPanel(string str)
        {
            errorTipsPanel.SetActive(true);
            text_ErrorTips.text = str;
        }
    }
}
