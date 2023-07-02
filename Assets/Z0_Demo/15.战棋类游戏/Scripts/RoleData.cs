using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_15
{
    [CreateAssetMenu(fileName = "RoleData", menuName = "创建配置/Z_15_RoleDataConfig")]
    public class RoleData : ScriptableObject
    {
        public string name_;
        public string info;
        public Sprite sprite;
        public List<SkillData> skillList;

        public float hp;
        public float maxhp;
    }
}
