using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z11
{
    public class Role : MonoBehaviour
    {
        public RoleData roleData;
        public Cell currCell;
        private Outline outline;
        public bool selected;

        void Start()
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineColor = Color.yellow;
        }

        // 角色移动
        public void Move(Cell target)
        {
            transform.position = target.transform.position;
            currCell.currRole = null;
            currCell = target;
        }

        // 技能释放
        public void CastingSkill(Role target)
        {
            RoleManager.Instance.CastingSkill(this, target);
        }

        // 设置高亮
        public void SetHighlight(bool isHighLight)
        {
            if (isHighLight)
            {
                outline.OutlineColor = Color.green;
                currCell.SetHighlight(true);
            }

            else if (selected == false)
            {
                outline.OutlineColor = Color.clear;
                currCell.SetHighlight(false);
            }

        }

        // 角色受到伤害
        public void OnHurt(float value)
        {
            Debug.Log("受到伤害!");
            if (roleData.hp - value <= 0)
            {
                Debug.Log("角色死亡");
            }
            // 减血
            roleData.hp -= value;
            // 刷新UI
            transform.Find("HP").gameObject.GetComponentInChildren<HPShowOrHide>().ShowHP(roleData);
        }
    }
}
