using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2_5
{
    [System.Serializable]
    public class SingleLinkedList
    {
        [System.Serializable]
        public class Node
        {
            public GameNode gameNode;
            public string data;
            public Node next;

            public Node(string data, Node next, GameNode gameNode)
            {
                this.data = data;
                this.next = next;
                this.gameNode = gameNode;
            }
            public Node(string data, Node next) : this(data, next, null) { }
            public Node(string data, GameNode gameObj) : this(data, null, gameObj) { }
            public Node(string data) : this(data, null, null) { }
        }

        private Node head;
        private int count;
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        public SingleLinkedList()
        {
            head = null;
            count = 0;
        }

        private void IsIndexValid(int index)
        {
            if (index < 0 || index >= count)
                throw new System.Exception("下标越界异常！");
        }

        public bool Contains(string data)
        {
            Node cur = head;
            while (cur != null)
            {
                if (cur.data != null && cur.data.Equals(data))
                    return true;
                cur = cur.next;
            }
            return false;
        }

        public void Insert(int index, string data, GameNode gameObj)
        {
            if (index < 0 || index > count)
                throw new System.Exception("插入时，下标越界异常!");

            // 从头节点插入
            if (index == 0)
            {
                Node node = new Node(data, gameObj);
                node.next = head;
                head = node;
            }
            // 从索引处插入，原节点向后移
            else
            {
                Node pre = head;
                for (int i = 0; i < index - 1; i++)
                    pre = pre.next;

                pre.next = new Node(data, pre.next, gameObj);
            }
            count++;
        }

        public Node Serach(string data)
        {
            Node cur = head;
            while (cur != null)
            {
                if (cur.data != null && cur.data.Equals(data))
                    return cur;
                cur = cur.next;
            }
            return null;
        }

        public Node Serach(int index)
        {
            if (index < 0 || index > count)
                throw new System.Exception("下标越界异常!");

            // 获取头
            if (index == 0)
                return head;
            else
            {
                Node pre = head;
                for (int i = 0; i < index - 1; i++)
                    pre = pre.next;

                return pre.next;
            }

        }

        public int GetIndex(string data)
        {
            int i = 0;
            Node cur = head;
            while (cur != null)
            {
                if (cur.data != null && cur.data.Equals(data))
                    return i;
                cur = cur.next;
                i++;
            }
            return -1;
        }

        public string RemoveAt(int index)
        {
            IsIndexValid(index);

            // 删除头节点
            if (index == 0)
            {
                Node del = head;
                head = head.next;
                count--;
                return del.data;
            }

            Node pre = head;
            for (int i = 0; i < index - 1; i++)
                pre = pre.next;
            Node delNode = pre.next;
            pre.next = delNode.next;
            count--;
            return delNode.data;
        }



        public void Print()
        {
            Node cur = head;
            while (cur != null && cur.data != null)
            {
                Debug.Log(cur.data + "->");

                cur = cur.next;
            }

        }
    }
}
