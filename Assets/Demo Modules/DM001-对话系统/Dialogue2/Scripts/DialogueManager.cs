using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dialogue2 
{
    public class DialogueManager : MonoBehaviour
    {
        #region 字段
        public static DialogueManager Instance;

        // 对话数据文本
        public TextAsset dialogDataFile;
        // 左侧头像
        public Image leftRoleAvatar;
        // 右侧头像
        public Image rightRoleAvatar;
        // 左侧名称显示
        public Text leftRoleName;
        // 右侧名称显示
        public Text rightRoleName;
        // 对话文本组件
        public Text dialogText;
        // 继续按钮
        public GameObject btn_Continue;
        // 对话面板
        public GameObject dialogPanel;
        // 选项按钮预制体
        public GameObject optionButtonPrefab;
        // 选项按钮的父对象
        public GameObject optionButtonGroup;

        // 角色立绘 List
        public List<Sprite> roleSpritesList = new List<Sprite>();
        // 角色对应立绘 Dictionary
        public Dictionary<string, Sprite> roleSpriteDic = new Dictionary<string, Sprite>();

        // 把整个文件按行拆分 数组
        public string[] dialogContentRows;
        // 当前对话ID
        public int dialogID;
        // 对话打字机速度
        public float tyepSpeed;
        // 打字是否完成
        public bool isTypeDone;
        #endregion

        #region Mono
        private void Awake()
        {
            Instance = this;

            // 写入角色名称和对应立绘（头像）
            roleSpriteDic["玩家"] = roleSpritesList[0];
            roleSpriteDic["NPC渔夫"] = roleSpritesList[1];
            roleSpriteDic["NPC农夫"] = roleSpritesList[2];

            // 读取对话数据
            dialogContentRows = dialogDataFile.text.Split('\n');
        }
        #endregion

        #region Public

        /// <summary>
        /// 显示对话面板，然后调用此函数即可启动对话系统
        /// </summary>
        /// <param name="index">CSV文件中想要触发的对话序号</param>
        public void SetDialogID(int index)
        {
            dialogID = index;
            UpdateDialog();
        }

        /// <summary>
        /// 继续对话
        /// </summary>
        public void OnClickNextDialog()
        {
            if (isTypeDone)
                UpdateDialog();
        }

        /// <summary>
        /// 选项分支的响应函数
        /// </summary>
        /// <param name="nextID">下一个ID</param>
        private void OnClickOptionDialog(int nextID)
        {
            dialogID = nextID;
            UpdateDialog();

            // 点击之后销毁 所有选项按钮
            foreach (Transform t in optionButtonGroup.transform)
            {
                Destroy(t.gameObject);
            }
        }

        #endregion

        #region Private
        /// <summary>
        /// 更新对话
        /// </summary>
        private void UpdateDialog()
        {
            for (int i = 0; i < dialogContentRows.Length; i++)
            {
                // 将行按列拆分
                string[] cells = dialogContentRows[i].Split(',');

                // 获取行数据的类型
                string dialogType = cells[0];
                int ID;
                int.TryParse(cells[1], out ID);
                string roleName = cells[2];
                bool isLeft = cells[3].Equals("左");
                string contentText = cells[4];
                int nextID;
                int.TryParse(cells[5], out nextID);

                // 判断当前对话ID和文件内ID是否相符
                if (dialogID == ID)
                {
                    // Dialog类型处理
                    if (dialogType.Equals("DiaLog"))
                    {
                        UpdateText(roleName, contentText, isLeft);
                        UpdateImage(roleName, isLeft);
                        dialogID = nextID;
                        btn_Continue.gameObject.SetActive(true);
                        break;
                    }

                    // Choose类型处理
                    else if (dialogType.Equals("Choose"))
                    {
                        btn_Continue.gameObject.SetActive(false);
                        GenerateOption(i);
                    }

                    // End类型处理
                    else if (dialogType.Equals("End"))
                    {
                        dialogPanel.SetActive(false);
                    }
                }
            }
        }

        /// <summary>
        /// 生成对话选项按钮
        /// </summary>
        /// <param name="index">当前行数据</param>
        private void GenerateOption(int index)
        {
            string[] cells = dialogContentRows[index].Split(',');
            string dialogType = cells[0];
            int ID;
            int.TryParse(cells[1], out ID);
            string contentText = cells[4];
            int nextID;
            int.TryParse(cells[5], out nextID);



            // 因为要回调，所以这里要加一个判断
            if (dialogType.Equals("Choose"))
            {
                // 生成按钮
                GameObject optionBtn = Instantiate(optionButtonPrefab, optionButtonGroup.transform);

                // 填入文本
                optionBtn.GetComponentInChildren<Text>().text = contentText;

                // 绑定事件
                optionBtn.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        OnClickOptionDialog(nextID);
                    });

                // 回调继续生成按钮
                GenerateOption(++index);
            }
        }


        /// <summary>
        /// 更新角色对话的名称和内容
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="leftOrRight"></param>
        public void UpdateText(string name, string text, bool leftOrRight)
        {
            if (leftOrRight)
            {
                leftRoleName.gameObject.SetActive(true);
                rightRoleName.gameObject.SetActive(false);

                leftRoleName.text = name;
            }
            else
            {
                leftRoleName.gameObject.SetActive(false);
                rightRoleName.gameObject.SetActive(true);

                rightRoleName.text = name;
            }

            StartCoroutine(Typing_Y(text));
        }

        /// <summary>
        /// 更新角色立绘或头像
        /// </summary>
        /// <param name="name"></param>
        /// <param name="leftOrRight"></param>
        public void UpdateImage(string name, bool leftOrRight)
        {
            if (leftOrRight)
            {
                leftRoleAvatar.gameObject.SetActive(true);
                rightRoleAvatar.gameObject.SetActive(false);

                leftRoleAvatar.sprite = roleSpriteDic[name];

            }
            else
            {
                leftRoleAvatar.gameObject.SetActive(false);
                rightRoleAvatar.gameObject.SetActive(true);

                rightRoleAvatar.sprite = roleSpriteDic[name];
            }
        }


        #endregion

        // 打字机协程
        IEnumerator Typing_Y(string content)
        {
            isTypeDone = false;
            dialogText.text = "";
            for (int i = 0; i < content.Length; i++)
            {
                dialogText.text += content[i];
                yield return new WaitForSeconds(tyepSpeed);
            }
            isTypeDone = true;
        }
    }

}
