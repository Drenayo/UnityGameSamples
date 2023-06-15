using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2_3
{
    public class Tree_
    {
        public class Node
        {
            public string data;
            public List<Node> childList;
            public int Count { get { return childList.Count; } }
            public bool IsEmpty { get { return Count == 0; } }
            public Node(string data)
            {
                this.data = data;
                childList = new List<Node>();
            }
        }
        private Node root;
        public Node Root { get { return root; } }
        public Tree_(string data)
        {
            root = new Node(data);
        }


        // 全局查找节点
        public Node FindNode(string data)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node del = queue.Dequeue();
                if (del.data.Equals(data))
                    return del;

                if (!del.IsEmpty)
                {
                    foreach (Node node in del.childList)
                        queue.Enqueue(node);
                }
            }

            return null;
        }

        // 查找父节点
        public Node FindParentNode(Node sonNode)
        {
            if (sonNode == null) return sonNode;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node del = queue.Dequeue();
                
                if (!del.IsEmpty)
                {
                    foreach (Node node in del.childList)
                    {
                        if (node.Equals(sonNode))
                            return del;
                        else
                            queue.Enqueue(node);
                    }
                }
            }

            return null;
        }

        // 添加子节点
        public void Add(string data, Node parentNode)
        {
            Node node = new Node(data);
            if (root == null)
                root = node;

            parentNode.childList.Add(node);
        }
        public void Add(Node sonNode, Node parentNode)
        {
            if (root == null)
                root = sonNode;

            parentNode.childList.Add(sonNode);
        }

        // 查找子节点
        public Node FindChildNode(Node parentNode, string data)
        {
            if (parentNode == null) throw new System.Exception("父为空！");
            foreach (Node node in parentNode.childList)
            {
                if (node.data.Equals(data))
                {
                    return node;
                }
            }

            return null;
        }

        // 删除子节点
        public Node RemoveChildNode(Node parentNode, Node delNode)
        {
            if (parentNode == null || delNode == null) throw new System.Exception("父或子节点为空！");
            parentNode.childList.Remove(delNode);
            return delNode;
        }
        // 删除节点
        public Node RemoveNode(Node delNode)
        {
            if ( delNode == null) throw new System.Exception("节点为空！");
            FindParentNode(delNode).childList.Remove(delNode);
            return delNode;
        }
        // 修改节点值
        public void ChangeNode(Node node, string value)
        {
            node.data = value;
        }
    }
}
