using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2
{
    [CreateAssetMenu(fileName = "Item", menuName = "创建配置/Z2")]
    public class Skill : ScriptableObject
    {
        public string skillName;
        public int skillHurt;
        public Sprite skillSprite;
    }
}
