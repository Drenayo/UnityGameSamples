using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dialogue1
{
    public class DialogueManager : MonoBehaviour, IPointerClickHandler
    {
        #region 字段
        // 对话面板
        public GameObject diaPanel;
        // 当前说话的角色名字
        public Text textName;
        // 当前说话的文本
        public Text textInfo;
        // 说话文本存储资源 TextAsset格式包括txt\html\xml等等
        // https://docs.unity3d.com/cn/2019.2/Manual/class-TextAsset.html
        public TextAsset textFile;
        // 当前对话索引
        public int dialogIndex;
        // 打字机速度
        public float typeSpeed;
        // 打字是否完成（判断是否可以点击进入下一条对话）
        public bool isTypeFinish;
        // 从TextAsset读取的文本List
        public List<string> textList = new List<string>();
        #endregion
         
        #region 生命周期函数
        void Awake()
        {
            GetTextFormFile(textFile);
        }
        void OnEnable()
        {
            StartCoroutine(SetText());
        }
        #endregion

         
        /// <summary>
        /// 从资源文件中读取文本行 到 List
        /// </summary>
        private void GetTextFormFile(TextAsset file)
        {
            textList.Clear();
            dialogIndex = 0;
            var lineData = file.text.Split('\n');
            foreach (var item in lineData)
            {
                textList.Add(item);
            }
        }

        /// <summary>
        /// 点击UI即可进入下一条对话（方式可更换，比如按下某键进入下一条对话等）
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isTypeFinish)
                return;
            if (dialogIndex == textList.Count)
            {
                diaPanel.SetActive(false);
                dialogIndex = 0;
                return;
            }
            StartCoroutine(SetText());
        }

        // 打字机协程
        IEnumerator SetText()
        {
            isTypeFinish = false;
            textInfo.text = string.Empty;
            SpeakRoleJudgment();
            for (int i = 0; i < textList[dialogIndex].Length; i++)
            {
                textInfo.text += textList[dialogIndex][i];
                yield return new WaitForSeconds(typeSpeed);
            }
            dialogIndex++;
            isTypeFinish = true;
        }

        /// <summary>
        /// 判断当前说话的是哪个角色，在UI显示它的名字，这里是根据资源文件内部的标号判断的 
        /// </summary>
        private void SpeakRoleJudgment()
        {
            switch (textList[dialogIndex].Trim().ToString())
            {
                case "小梦":
                    textName.text = "小梦";
                    break;
                case "店员":
                    textName.text = "店员";
                    break;
            }
            dialogIndex++;
        }
    }
}