using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2_3
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="创建配置/Z2_3_创建保存节点",fileName ="保存Item节点")]
    public class SaveTree:ScriptableObject
    {
        public List<SaveNode> saveList;
        public SaveNode GetSaveNodeBySleftName(string slefName)
        {
            foreach (SaveNode node in saveList)
            {
                if (node.slefName.Equals(slefName))
                    return node;
            }
            return null;
        }
        public SaveNode GetSaveNodeByParentName(string parentName)
        {
            foreach (SaveNode node in saveList)
            {
                if (node.parentName.Equals(parentName))
                    return node;
            }
            return null;
        }
    }

    // 存储用的节点
    [System.Serializable]
    public class SaveNode
    {
        public string slefName;
        public string parentName;
        public SaveNode(string slef, string parent)
        {
            slefName = slef;
            parentName = parent;
        }
    }

}
