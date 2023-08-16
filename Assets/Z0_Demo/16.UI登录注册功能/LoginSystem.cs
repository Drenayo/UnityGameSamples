using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_16
{
    // 登录
    public class LoginSystem : MonoBehaviour
    {
        public InputField ifd_User;
        public InputField ifd_Pass;
        public InputField currFocusIfd;
        public GameObject KeyGameObjectPanel;
        public GameObject TipsPopGameObject;
        
        public Dictionary<string, string> userDic = new Dictionary<string, string>();
        // 请输入账号密码
        // 登录失败，请检查您的用户名和密码
        // 用户已成功注册！

        // 此用户名已被占用，请选择其他用户名

        // 注册
        public void Btn_Reg()
        {
            if (ifd_User.text == string.Empty || ifd_Pass.text == string.Empty)
            {
                TipsPopShow("请输入账号密码");
                return;
            }
            if (userDic.ContainsKey(ifd_User.text))
            {
                TipsPopShow("此用户名已被占用，请选择其他用户名");
                return;
            }
            TipsPopShow("用户已成功注册！");
        }

        // 登录
        public void Btn_Login()
        {
            if (userDic.ContainsKey(ifd_User.text) && userDic.ContainsValue(ifd_Pass.text))
            {
                TipsPopShow("登录成功！");

            }
            else
            {
                TipsPopShow("登录失败，请检查您的用户名和密码");
            }
        }


        // 调用键盘事件
        public void ClickInputEvent(string str)
        {
            KeyGameObjectPanel.SetActive(true);
            KeyGameObjectPanel.transform.Find("Lab_Show/Text").GetComponent<Text>().text = str;
            if (str.Equals("账号"))
                currFocusIfd = ifd_User;
            else
                currFocusIfd = ifd_Pass;
        }

        // 数字键盘
        public void Btn_Number(string number)
        {
            currFocusIfd.text += number;
        }

        // 删除键
        public void Btn_Remove()
        {
            currFocusIfd.text = string.Empty;
            //if (currFocusIfd.text.Length - 1 >= 0)
            //  currFocusIfd.text = currFocusIfd.text.Substring(0, currFocusIfd.text.Length - 1);
        }


        public void TipsPopShow(string str,float time=2)
        {
            TipsPopGameObject.SetActive(true);
            TipsPopGameObject.transform.Find("Text").GetComponent<Text>().text = str;
            StartCoroutine(DelayHideTextPop(time));
        }
        IEnumerator DelayHideTextPop(float t)
        {
            yield return new WaitForSeconds(t);
            TipsPopGameObject.SetActive(false);
        }
    }
}
