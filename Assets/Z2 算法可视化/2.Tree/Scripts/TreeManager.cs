using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z2_2
{
    public class TreeManager : MonoBehaviour
    {
        public MultiTree<string> tree;
        public InputField idf_InputName;
        public InputField idf_InputParentName;
        void Start()
        {
            tree = new MultiTree<string>("Root");
            tree.Add("A", tree.Root);
            tree.Add("B", tree.Root);
            tree.Add("C", tree.Root);
        }


        void Update()
        {

        }

        public void Btn_AddNode()
        {
            MultiTree<string>.Node parentNode = tree.FindNodeByData(idf_InputParentName.text);
            string nodeValue = idf_InputName.text;

            if (parentNode != null)
            {
                tree.Add(nodeValue, parentNode);
                Debug.Log("成功添加节点" + nodeValue);
            }
            else
            {
                Debug.Log("找不到父节点！无法添加" + nodeValue);
            }
        }
    }
}
