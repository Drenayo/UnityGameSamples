using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Z_16
{
    public class LoginSystemAll : MonoBehaviour
    {
        public bool isPlayer;
        public InputField ifd_User;
        public InputField ifd_Pass;
        public InputField ifd_Curr;
        public GameObject tipsErrorInfo;
        public Text text_TipsErrorInfo;
        public GameObject inputStateInfo;
        public Text text_InputStateInfo;
        public float tipsHidespeed;
        public Toggle tog_ShowPass;

        public Dictionary<string, string> dicUserInfo = new Dictionary<string, string>();

        private void Start()
        {
            isPlayer = true;
            tog_ShowPass.onValueChanged.AddListener(ShowPass);
        }

        public void Btn_Reg()
        {
            // 注册 账号密码不能为空、账号已经被注册、账号成功注册

            if (!isPlayer)
            {
                ShowTipsErrorPanel("游客无需注册，直接登录即可");
                return;
            }

            if (ifd_User.text == string.Empty && ifd_Pass.text == string.Empty)
            {
                ShowTipsErrorPanel("账号密码不能为空！");
                return;
            }
            if (dicUserInfo.ContainsKey(ifd_User.text))
            {
                ShowTipsErrorPanel("账号已经被注册！");
                return;
            }
            dicUserInfo.Add(ifd_User.text, ifd_Pass.text);
            ShowTipsErrorPanel("账号注册成功！");
            ifd_User.text = string.Empty;
            ifd_Pass.text = string.Empty;

        }

        public void Btn_Login()
        {
            // 登录 登录成功、账号密码错误
            if (!isPlayer)
            {
                ShowTipsErrorPanel("游客登录成功！");
                return;
            }

            if (dicUserInfo.ContainsKey(ifd_User.text) && dicUserInfo.ContainsValue(ifd_Pass.text))
            {
                ShowTipsErrorPanel("玩家登录成功！");
            }
            else
            {
                ShowTipsErrorPanel("账号密码错误，请重新输入！");
                ifd_Pass.text = string.Empty;
            }
        }

        // 得到输入框焦点
        public void GetInputFocus(string str)
        {
            inputStateInfo.SetActive(true);
            if (str.Equals("账号"))
            {
                ifd_Curr = ifd_User;
                text_InputStateInfo.text = "正在输入账号...";
            }
            else
            {
                ifd_Curr = ifd_Pass;
                text_InputStateInfo.text = "正在输入密码...";
            }
        }

        public void Btn_Number(string str)
        {
            ifd_Curr.text += str;
        }

        public void Btn_Claer()
        {
            ifd_Curr.text = string.Empty;
        }

        public void Btn_Remove()
        {
            if (ifd_Curr.text.Length != 0)
                ifd_Curr.text = ifd_Curr.text.Substring(0, ifd_Curr.text.Length - 1);
        }

        public void SetIsPlayer(bool b)
        {
            isPlayer = b;
        }

        public void ShowPass(bool isShow)
        {
            if (isShow)
            {
                ifd_Pass.contentType = InputField.ContentType.Alphanumeric;
                ifd_Pass.ActivateInputField();
            }
            else
            {
                ifd_Pass.contentType = InputField.ContentType.Password;
                ifd_Pass.ActivateInputField();
            }
        }

        public void ShowTipsErrorPanel(string str)
        {
            tipsErrorInfo.SetActive(true);
            text_TipsErrorInfo.text = str;
            StopAllCoroutines();
            StartCoroutine(ShowTipsErrorPanel_E());
        }

        IEnumerator ShowTipsErrorPanel_E()
        {
            yield return new WaitForSeconds(tipsHidespeed);
            tipsErrorInfo.SetActive(false);
        }
    }
}
