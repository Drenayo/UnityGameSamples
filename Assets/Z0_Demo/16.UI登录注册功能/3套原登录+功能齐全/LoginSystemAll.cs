using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Z_16
{
    public class LoginSystemAll : MonoBehaviour
    {
        public InputField ifd_User;
        public InputField ifd_Pass;
        public InputField ifd_Curr;

        public GameObject gameObj_InputStateTips;
        public GameObject gameObj_ErrorTips;
        public Text txt_InputStateTips;
        public Text txt_ErrorTips;
        public float errorTipsHideTime;
        public Toggle tog_ShowPass;
        public bool IsPlayer = true;
        public Dictionary<string, string> dic_UserInfo = new Dictionary<string, string>();
        private void Start()
        {

        }

        public void Btn_Reg()
        {
            if (IsPlayer)
            {
                //账号密码不能为空、用户名重复、成功注册
                if (ifd_User.text == string.Empty || ifd_Pass.text == string.Empty)
                {
                    ShowErrorTipsPanel("账号密码不能为空！");
                }
                else if (dic_UserInfo.ContainsKey(ifd_User.text))
                {
                    ShowErrorTipsPanel("用户名不能重复注册！");
                }
                else
                {
                    dic_UserInfo.Add(ifd_User.text, ifd_Pass.text);
                    ShowErrorTipsPanel("注册成功！");
                    ifd_User.text = string.Empty;
                    ifd_Pass.text = string.Empty;
                }
            }
            else
            {
                ShowErrorTipsPanel("游客无需注册，直接登录即可！");
            }

        }

        public void Btn_Login()
        {
            if (IsPlayer)
            {
                // 不存在该账号 \密码输入错误
                if (dic_UserInfo.ContainsKey(ifd_User.text))
                {
                    string strValue;
                    if (dic_UserInfo.TryGetValue(ifd_User.text, out strValue))
                    {
                        if (strValue.Equals(ifd_Pass.text))
                            ShowErrorTipsPanel("登录成功！");
                        else
                        {
                            ShowErrorTipsPanel("密码输入错误！");
                        }
                    }
                }
                else
                {
                    ShowErrorTipsPanel("不存在该账号！");
                }
            }
            else
            {
                ShowErrorTipsPanel("登录成功！");
            }
        }

        public void Tog_ShowPass(bool isOn)
        {
            if (isOn)
            {
                ifd_Pass.contentType = InputField.ContentType.Standard;
                ifd_Pass.ActivateInputField();
            }
            else
            {
                ifd_Pass.contentType = InputField.ContentType.Password;
                ifd_Pass.ActivateInputField();
            }
        }

        public void SetUserPlayer(bool b)
        {
            IsPlayer = b;
        }

        // 得到焦点
        public void GetInputFoucePoint(string str)
        {
            gameObj_InputStateTips.SetActive(true);
            if (str.Equals("账号"))
            {
                txt_InputStateTips.text = "正在输入账号..";
                ifd_Curr = ifd_User;
            }
            else
            {
                txt_InputStateTips.text = "正在输入密码..";
                ifd_Curr = ifd_Pass;
            }
        }

        public void Btn_Number(string number)
        {
            ifd_Curr.text += number;
        }

        public void Btn_Remove()
        {
            if(ifd_Curr.text.Length > 0)
            ifd_Curr.text = ifd_Curr.text.Substring(0, ifd_Curr.text.Length - 1);
        }

        public void Btn_Clear()
        {
            ifd_Curr.text = string.Empty;
        }

        private void ShowErrorTipsPanel(string strContent)
        {
            gameObj_ErrorTips.SetActive(true);
            gameObj_ErrorTips.GetComponent<Text>().text = strContent;
            StartCoroutine(ShowErrorTipsPanel_E());
        }

        IEnumerator ShowErrorTipsPanel_E()
        {
            yield return new WaitForSeconds(errorTipsHideTime);
            gameObj_ErrorTips.SetActive(false);
        }
    }
}
