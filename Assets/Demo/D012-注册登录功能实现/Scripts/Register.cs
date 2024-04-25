using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

namespace D012
{
    public class Register : MonoBehaviour
    {
        public InputField ifd_User;
        public InputField ifd_Pass;
        public InputField ifd_AgainPass;
        public Toggle tog_ShowPass;
        private void Start()
        {
            tog_ShowPass.onValueChanged.AddListener(ShowPass);
        }

        // 注册
        public void Btn_Reg()
        {
            // 账号密码不能为空
            // 账号用户名不能重复
            // 账号只能使用英文和数字
            // 账号长度不能低于4位
            // 密码长度不能低于6位
            // 两次密码不一致
            string pattern = @"^[a-zA-Z0-9]+$";

            string userStr = ifd_User.text;
            string passStr = ifd_Pass.text;
            string agaginPassStr = ifd_AgainPass.text;
            LoginSystem loginSystem = LoginSystem.Instance;

            if (userStr == string.Empty || passStr == string.Empty)
            {
                loginSystem.ShowErrorTipsPanel("账号密码不能为空！");
            }
            else if(loginSystem.dicUserInfo.ContainsKey(userStr))
            {
                loginSystem.ShowErrorTipsPanel("用户名不能重复！");
            }
            else if (!Regex.IsMatch(userStr,pattern))
            {
                loginSystem.ShowErrorTipsPanel("用户名只能使用英文与数字");
            }
            else if (userStr.Length < 4)
            {
                loginSystem.ShowErrorTipsPanel("账号长度不能低于4位");
            }
            else if (passStr.Length < 6)
            {
                loginSystem.ShowErrorTipsPanel("密码长度不能低于6位");
            }
            else if (!passStr.Equals(agaginPassStr))
            {
                loginSystem.ShowErrorTipsPanel("两次密码不一致");
            }
            else
            {
                loginSystem.dicUserInfo.Add(userStr, passStr);
                loginSystem.ShowErrorTipsPanel("注册成功！");
            }
        }

        // 显示密码
        public void ShowPass(bool isOn)
        {
            if (isOn)
            {
                ifd_Pass.contentType = InputField.ContentType.Standard;
                ifd_AgainPass.contentType = InputField.ContentType.Standard;
                ifd_Pass.ActivateInputField();
                ifd_AgainPass.ActivateInputField();
            }
            else
            {
                ifd_Pass.contentType = InputField.ContentType.Password;
                ifd_AgainPass.contentType = InputField.ContentType.Password;
                ifd_Pass.ActivateInputField();
                ifd_AgainPass.ActivateInputField();
            }
        }
    }
}
