using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z11
{
    [CreateAssetMenu(fileName ="RoleSkill",menuName = "创建配置/Z11_RoleSKillData")]
    public class SkillData : ScriptableObject
    {
        // 技能ID
        public int id;
        // 技能名称
        public string name_;
        // 技能介绍
        public string info;
        // 技能图标
        public Sprite sprite;
        // 施法时间
        public float castTime;
        // 冷却时间
        public float cooldown;
        // 技能范围
        public float range;
        // 技能伤害
        public float value;


        // 这里先写简单的一种技能方式，就是类似于法师释放魔法，击中直接伤害，不考虑buff,不考虑后续
        // 伤害也只是简单的-hp,不考虑各种物理伤害，魔法伤害，防御值，盔甲等的 混合计算
    }
}
