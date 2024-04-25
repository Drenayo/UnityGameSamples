using UnityEngine;
using UnityEngine.UI;

namespace D012
{
    public class Login : MonoBehaviour
    {
        public InputField ifd_User;
        public InputField ifd_Pass;
        public Toggle tog_ShowPass;

        void Start()
        {
            tog_ShowPass.onValueChanged.AddListener(ShowPass);
        }

        // 注册
        public void Btn_Login()
        {
            // 该用户名不存在
            // 密码输入错误
            // 账号密码不能为空

            string userStr = ifd_User.text;
            string passStr = ifd_Pass.text;
            LoginSystem loginSystem = LoginSystem.Instance;
            string tempValue = string.Empty;

            if (userStr == string.Empty || passStr == string.Empty)
            {
                loginSystem.ShowErrorTipsPanel("账号密码不能为空！");
            }
            else if (loginSystem.dicUserInfo.ContainsKey(userStr))
            {
                if (loginSystem.dicUserInfo.TryGetValue(userStr, out tempValue))
                {
                    if (tempValue.Equals(passStr))
                    {
                        loginSystem.ShowErrorTipsPanel("登录成功！");
                    }
                    else
                    {
                        loginSystem.ShowErrorTipsPanel("密码输入错误！");
                    }
                }
                
            }
            else
            {
                loginSystem.ShowErrorTipsPanel("该用户名不存在！");
            }

        }

        public void Btn_TempLogin()
        {
            LoginSystem.Instance.ShowErrorTipsPanel("游客登录成功！！");
        }

        // 显示密码
        public void ShowPass(bool isOn)
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

    }
}
