using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Z_16
{
    public class LoginSystem : MonoBehaviour
    {
        public InputField ifd_User;
        public InputField ifd_Pass;
        public InputField ifd_Curr;
        public GameObject inputStateGameObject;
        public Text inputStateInfo;
        public GameObject tipsPanel;
        public float tipsPanelHideTime;
        public Dictionary<string, string> dicUserInfo = new Dictionary<string, string>();


        // 注册 账号密码不能为空、用户已被注册、用户成功注册
        // 登录  登录失败，请检查您的用户名和密码
        public void Btn_Reg()
        {
            if (ifd_User.text == string.Empty || ifd_Pass.text == string.Empty)
            {
                ShowTipsPanel("账号密码不能为空！");
                return;
            }
            if (dicUserInfo.ContainsKey(ifd_User.text))
            {
                ShowTipsPanel("用户已被注册！");
                return;
            }

            dicUserInfo.Add(ifd_User.text, ifd_Pass.text);
            ShowTipsPanel("用户成功注册！");

        }

        public void Btn_Login()
        {
            if (dicUserInfo.ContainsKey(ifd_User.text) && dicUserInfo.ContainsValue(ifd_Pass.text))
            {
                ShowTipsPanel("登录成功！");
            }
            else
                ShowTipsPanel("登录失败，请检查您的用户名和密码");
        }


        // 获取输入焦点
        public void GetInputFocus(string str)
        {
            inputStateGameObject.SetActive(true);
            if (str.Equals("账号"))
            {
                ifd_Curr = ifd_User;
                inputStateInfo.text = "正在输入账号...";
            }
            else
            {
                ifd_Curr = ifd_Pass;
                inputStateInfo.text = "正在输入密码...";
            }
        }

        // 按下数字键
        public void Btn_Number(string strNumber)
        {
            ifd_Curr.text += strNumber;
        }


        // 按下删除键
        public void Btn_Remove()
        {
            ifd_Curr.text = string.Empty;
        }

        // 提示窗
        public void ShowTipsPanel(string str)
        {
            tipsPanel.SetActive(true);
            tipsPanel.transform.Find("Text").GetComponent<Text>().text = str;
            StartCoroutine(ShowTipsPanel_E());
        }
        IEnumerator ShowTipsPanel_E()
        {
            yield return new WaitForSeconds(tipsPanelHideTime);
            tipsPanel.SetActive(false);
        }
    }
}
