using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Z2_3
{
    public class TreeManager : MonoBehaviour
    {
        // 树
        public Tree_ tree;
        // 路径栈 每进入下一层，push一次，方便直接取栈顶新建项，而不是重新查找
        private Stack<Tree_.Node> pathStack = new Stack<Tree_.Node>();
        // 存储
        public SaveTree saveTree;

        public Transform canvas;
        public Transform svContent;
        public Transform popMenuPanel;// 生成的菜单
        public Text treePathText;

        public GameObject g;

        public GameObject itemPrefab;
        public GameObject createItemPanel;
        public GameObject moveItemPanel;
        public GameObject renameItemPanel;
        public GameObject popMenuPanelPrefab;
        void Start()
        {
            tree = new Tree_("Root");
            ReadItem(); //读取
            Push(tree.Root);
            //tree.Add("初始文件夹", tree.Root);
            //tree.Add("Atu_视频", tree.FindChildNode(tree.Root, "初始文件夹"));
            //tree.Add("Btu_项目", tree.FindChildNode(tree.Root, "初始文件夹"));
            //tree.Add("Ctu_娱乐", tree.FindChildNode(tree.Root, "初始文件夹"));

            //tree.Add("植物大战僵尸", tree.FindChildNode(tree.FindChildNode(tree.Root, "初始文件夹"), "Ctu_娱乐"));
            //tree.Add("植物大战小狗", tree.FindChildNode(tree.FindChildNode(tree.Root, "初始文件夹"), "Ctu_娱乐"));

            //tree.Add("学习资料文件夹", tree.Root);
            //tree.Add("影视文件夹", tree.Root);
            //tree.Add("测试文件夹", tree.Root);
        }

        void Update()
        {
            g = GetOverUI();

            // 创建项
            if (Input.GetKeyDown(KeyCode.Mouse1) && GetOverUI().Equals(svContent.parent.gameObject))
            {
                CreateItem(pathStack.Peek());
            }

            // 选中项
            else if (Input.GetKeyDown(KeyCode.Mouse1) && GetOverUI().CompareTag("Item"))
            {
                PopItemMenu(GetOverUI().name);
            }

        }

        #region UI操作

        // 右键Item 弹出菜单
        private void PopItemMenu(string name)
        {
            // 若菜单为空 则实例化一个
            if (popMenuPanel == null)
                popMenuPanel = Instantiate(popMenuPanelPrefab, canvas).transform;

            popMenuPanel.gameObject.SetActive(true);

            // 重置位置
            popMenuPanel.transform.position = Input.mousePosition + new Vector3(130, -130, 0);

            popMenuPanel.transform.Find("Btn_Rename").GetComponent<Button>().onClick.AddListener(() => { RenameItem(tree.FindNode(name)); });
            popMenuPanel.transform.Find("Btn_Delete").GetComponent<Button>().onClick.AddListener(() => { DeletItem(tree.FindNode(name)); });
            popMenuPanel.transform.Find("Btn_Move").GetComponent<Button>().onClick.AddListener(() => { MoveItem(tree.FindNode(name)); });
        }

        // 删除Item *
        public void DeletItem(Tree_.Node node)
        {
            Tree_.Node parentNode = tree.FindParentNode(node);
            Tree_.Node delNode = tree.RemoveNode(node);
            RefUI(parentNode);

            //// 删除记录中的所有子节点项
            //foreach (SaveNode del in saveTree.saveList)
            //{
            //    if (del.parentName.Equals(delNode.data))
            //        saveTree.saveList.Remove(del);
            //}
            popMenuPanel.gameObject.SetActive(false);
        }

        // 移动Item *
        public void MoveItem(Tree_.Node node)
        {
            GameObject obj = Instantiate(moveItemPanel, canvas);

            obj.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                //Debug.Log($"s{node.data} + f{tree.FindParentNode(node).data}");
                // 思路 把当前节点从父节点的子List移除，加入另一个父节点的子List中即可
                Tree_.Node parentNode = tree.FindParentNode(node);
                Tree_.Node delNode = node;
                tree.RemoveChildNode(parentNode, node);

                // 添加到另一个父对象子list
                tree.Add(delNode, tree.FindNode(obj.GetComponentInChildren<InputField>().text));
                RefUI(parentNode);

                // 持久化记录更新
                //saveTree.GetSaveNodeBySleftName(node.data).parentName = obj.GetComponentInChildren<InputField>().text;

                Destroy(obj);
            });
            popMenuPanel.gameObject.SetActive(false);
        }

        // 重命名Item Done
        public void RenameItem(Tree_.Node node)
        {
            GameObject obj = Instantiate(renameItemPanel, canvas);

            obj.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                string newName = obj.GetComponentInChildren<InputField>().text;
                string oldName = node.data;

                // 修改树结构的节点
                tree.ChangeNode(node, newName);
                // 刷新
                RefUI(tree.FindParentNode(node));

                // 修改持久化记录
                saveTree.GetSaveNodeBySleftName(oldName).slefName = newName;
                // 修改记录中的所有子节点项
                foreach (SaveNode del in saveTree.saveList)
                {
                    if (del.parentName.Equals(oldName))
                        del.parentName = newName;
                }
                // 关闭面板
                Destroy(obj);
            });
            popMenuPanel.gameObject.SetActive(false);
        }

        // 新建Item
        private void CreateItem(Tree_.Node parentNode)
        {
            GameObject obj = Instantiate(createItemPanel, canvas);

            // 注册按钮事件
            obj.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                // 添加节点
                Tree_.Node node = new Tree_.Node(obj.GetComponentInChildren<InputField>().text);
                tree.Add(node, parentNode);
                SaveItem(new SaveNode(node.data, parentNode.data));
                RefUI(pathStack.Peek());
                Destroy(obj);
            });
        }

        // 前进按钮
        public void Btn_Next()
        {

        }

        // 回退按钮
        public void Btn_Pre()
        {
            Pop(pathStack.Peek());
            //PrintStackTest(pathStack);
        }

        // 刷新该节点的UI视图 该层被刷新
        private void RefUI(Tree_.Node node)
        {
            //Debug.Log("刷新UI:" + node.data + "子列表");
            foreach (Transform item in svContent)
                Destroy(item.gameObject);

            foreach (Tree_.Node n in node.childList)
            {
                GameObject obj = Instantiate(itemPrefab, svContent);
                obj.name = n.data;
                obj.GetComponentInChildren<Text>().text = n.data;
                obj.GetComponent<Button>().onClick.AddListener(() => { Push(n); });
            }
        }

        // 刷新路径
        private void RefPath(Tree_.Node node, int i)
        {
            if (tree.Root.Equals(node))
                treePathText.text = node.data + "/";

            else if (i == 1)
                treePathText.text += node.data + "/";
            else if (i == -1)
            {
                string pathStr = treePathText.text;
                //treePathText.text = treePathText.text.Substring(0, treePathText.text.Length - node.data.Length + 1);
                //Debug.Log($"原：{pathStr},长：{pathStr.Length},得到的字串:{pathStr.Substring(0, pathStr.Length - node.data.Length - 1)}");
                //Debug.Log($"node字串:{node.data},长：{node.data.Length}");
                treePathText.text = pathStr.Substring(0, pathStr.Length - node.data.Length - 1);
            }
        }

        #endregion



        // 读取
        public void ReadItem()
        {
            foreach (SaveNode node in saveTree.saveList)
            {
                tree.Add(node.slefName, tree.FindNode(node.parentName));
            }
        }

        // 测试遍历栈
        public void PrintStackTest(Stack<Tree_.Node> stack)
        {
            string str = string.Empty;
            foreach (Tree_.Node node in stack)
            {
                str += "<-" + node.data;
            }
            Debug.Log(str);
        }

        private void Push(Tree_.Node node)
        {
            pathStack.Push(node);

            RefUI(pathStack.Peek());
            RefPath(pathStack.Peek(), 1);
        }
        private void Pop(Tree_.Node node)
        {
            if (pathStack.Peek().Equals(tree.Root)) return;
            RefPath(pathStack.Pop(), -1);
            RefUI(pathStack.Peek());
        }

        // 保存
        private void SaveItem(SaveNode saveNode)
        {
            saveTree.saveList.Add(saveNode);
        }

        // 获取当前鼠标悬浮UI
        private GameObject GetOverUI()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();

            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);
            if (results.Count != 0)
            {
                return results[0].gameObject;
            }

            return null;
        }
    }
}
