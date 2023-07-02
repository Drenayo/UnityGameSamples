using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Z2_2
{

    // 二叉链表表示法，按照二叉树结构存储，按照多叉树表示
    [System.Serializable]
    public class MultiTree<T>
    {
        [System.Serializable]
        public class Node
        {
            public T data;
            public GameObject gameObj; //可视化使用
            public Node firstChild;
            public Node nextSibling;

            public void SetParent(Node parent)
            {
                gameObj.transform.SetParent(parent.gameObj.transform);
            }


            public Node(T data)
            {
                this.data = data;
                gameObj = new GameObject(data.ToString());
            }

            public override string ToString()
            {
                return $"[{data.ToString()}]";
            }
        }

        private int count;
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        private Node root;
        public Node Root { get { return root; } }

        public MultiTree()
        {
            root = null;
            count = 0;
        }
        public MultiTree(T rootName)
        {
            root = new Node(rootName);
            count = 1;
        }


        // 查找节点
        public Node FindNodeByData(T data)
        {
            if (IsEmpty) return null;
            if (root.data.Equals(data)) return root;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node del = queue.Dequeue();
                if (del.data.Equals(data)) return del;

                Node childNode = del.firstChild;
                while (childNode != null)
                {
                    queue.Enqueue(childNode);
                    childNode = childNode.nextSibling;
                }
            }
            return null;
        }

        // 查看节点是否存在
        public bool IsNodeExist(Node node)
        {
            if (IsEmpty) return false;
            if (node == null) return false;
            if (root.Equals(node)) return true;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node del = queue.Dequeue();
                if (del.Equals(node)) return true;

                Node childNode = del.firstChild;
                while (childNode != null)
                {
                    queue.Enqueue(childNode);
                    childNode = childNode.nextSibling;
                }
            }
            return false;
        }

        // 添加
        public void Add(T value, Node parent)
        {
            Node newNode = new Node(value);

            if (IsEmpty) // 若空树，设根节点
            {
                root = newNode;
                count++;
                return;
            }

            if (!IsNodeExist(parent)) return; //查看父节点是否存在

            // 设置父节点，可视化专用
            newNode.SetParent(parent);

            // 检查节点的左子节点(第一个子节点)
            if (parent.firstChild == null)
            {
                parent.firstChild = newNode;
                count++;
            }
            // 检查节点的子节点链
            else
            {
                Node siblingNode = parent.firstChild;

                while (siblingNode.nextSibling != null)
                    siblingNode = siblingNode.nextSibling;

                siblingNode.nextSibling = newNode;
                count++;
            }

        }

        // 层序遍历
        public void LevelOrderTraversal()
        {
            if (IsEmpty) return;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                Debug.Log(node.ToString());
                Node childNode = node.firstChild;
                while (childNode != null)
                {
                    queue.Enqueue(childNode);
                    childNode = childNode.nextSibling;
                }
            }
        }

    }
}
