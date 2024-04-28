using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D024
{
    public class LinkedListManager : MonoBehaviour
    {
        public SingleLinkedList linkedList;
        public GameObject nodePrefab; // 可视化节点的预制体
        public Transform nodeParent;  // 可视化节点的父物体
        public float nodeInterval;    // 节点间隔
        public float nodeHeight;

        [Space(15)]

        public Color defaultColor;
        public Color pointAtColor;

        [Space(15)]

        public InputField idf_listLength;
        public InputField idf_maxRandomNumber;
        public InputField idf_InsertIndex;
        public InputField idf_InsertData;
        public InputField idf_SearchValue;
        public InputField idf_DeleteValue;
        public Button btn_Reset;
        public Button btn_Insert;
        public Button btn_Serach;
        public Button btn_Delete;
        public TipsPanel tips;
        public CameraUpdate cameraUpdate;
        void Start()
        {
            btn_Reset.onClick.AddListener(Reset);
            btn_Serach.onClick.AddListener(Search);
            btn_Insert.onClick.AddListener(Insert);
            btn_Delete.onClick.AddListener(Delete);
            Reset();
        }

        public void Reset()
        {
            foreach (Transform node in nodeParent)
                Destroy(node.gameObject);

            linkedList = new SingleLinkedList();

            for (int i = 0; i < int.Parse(idf_listLength.text); i++)
            {
                string randomValue = Random.Range(1, int.Parse(idf_maxRandomNumber.text)).ToString();
                GameNode gameNode = Instantiate(nodePrefab).GetComponent<GameNode>();

                gameNode.SetText(randomValue);
                gameNode.SetTextIndex(i);
                gameNode.SetColor(defaultColor);
                gameNode.SetPosition(i + i * nodeInterval, nodeHeight, 0);
                gameNode.transform.SetParent(nodeParent, false);


                linkedList.Insert(i, randomValue, gameNode);
            }

            //Debug.Log(nodeParent.childCount);
            //cameraUpdate.CameraPosUpdate();
            //Debug.Log(nodeParent.childCount);
            tips.SetText($"重置成功！");
        }

        private GameNode preNode;
        private GameNode curNode;

        public void Search()
        {
            if (preNode != null)
                preNode.SetColor(defaultColor);

            SingleLinkedList.Node node = linkedList.Serach(idf_SearchValue.text);

            if (node == null)
            {
                tips.SetText($"查找[{idf_SearchValue.text}]失败！查找值不存在！");
                return;
            }

            curNode = node.gameNode.GetComponent<GameNode>();
            curNode.SetColor(pointAtColor);
            preNode = curNode;
            tips.SetText($"查找[{idf_SearchValue.text}]成功！");

        }

        public void Insert()
        {
            int insertIndex = int.Parse(idf_InsertIndex.text);
            string insertValue = idf_InsertData.text;

            if (insertIndex < 0 || insertIndex > linkedList.Count)
            {
                tips.SetText($"插入[{insertValue}]失败！下标越界！");
                return;
            }

            GameNode gameNode = Instantiate(nodePrefab).GetComponent<GameNode>();
            gameNode.SetColor(defaultColor);
            gameNode.SetText(insertValue);
            gameNode.SetTextIndex(insertIndex);
            gameNode.SetPosition(insertIndex + insertIndex * nodeInterval, nodeHeight, 0);
            gameNode.transform.SetParent(nodeParent, false);

            // 把后续可视化节点依次向后移动  本身位置+i+间隔
            for (int i = insertIndex; i < linkedList.Count; i++)
            {
                linkedList.Serach(i).gameNode.SetPosition(((i + 1) + (i + 1) * nodeInterval), nodeHeight, 0);
                linkedList.Serach(i).gameNode.SetTextIndex(i + 1);
            }

            linkedList.Insert(insertIndex, insertValue, gameNode);
            tips.SetText($"插入[{insertValue}]成功！");
        }

        public void Delete()
        {
            string deleteValue = idf_DeleteValue.text;
            int deleteIndex = linkedList.GetIndex(deleteValue);
            if (deleteIndex == -1)
            {
                tips.SetText($"删除[{deleteValue}]失败！查找不到该值！");
                return;
            }

            Destroy(linkedList.Serach(deleteValue).gameNode.gameObject);
            linkedList.RemoveAt(deleteIndex);

            // 后续向前移动
            for (int i = deleteIndex; i < linkedList.Count; i++)
            {
                linkedList.Serach(i).gameNode.SetPosition(i + i * nodeInterval, nodeHeight, 0);
                linkedList.Serach(i).gameNode.SetTextIndex(i);
            }

            tips.SetText($"删除[{deleteValue}]成功！");
        }
    }
}

// 做相机适配 Index 头标适配 异常的提示
