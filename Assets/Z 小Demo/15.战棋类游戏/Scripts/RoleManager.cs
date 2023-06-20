using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_15
{
    public class RoleManager : SingletonMonoBase<RoleManager>
    {
        public GameObject roleSelectPanel;
        public GameObject roleRoleUIButtonPrefab;
        public GameObject roleSkillPanel;
        public GameObject roleSkillUIButtonPrefab;
        public List<Role> rolePrefabList;

        void Start()
        {

        }

        public void CreateRole(Cell cell)
        {
            // 这里应该弹出选角色列表，然后再让其创建角色
            // 先把UI逻辑写在此处，后续在单独写UI类
            roleSelectPanel.SetActive(true);
            for (int i = 0; i < rolePrefabList.Count; i++)
            {
                GameObject obj = Instantiate(roleRoleUIButtonPrefab, roleSelectPanel.transform);
                Button roleButton = obj.GetComponent<Button>();
                Role rolePrefab = rolePrefabList[i];

                // 读取信息到button 显示
                roleButton.GetComponentInChildren<Text>().text = rolePrefab.roleData.name_;
                // 注册按钮事件
                roleButton.onClick.AddListener(() => OnClick_CreateRole(cell, rolePrefab.gameObject));
            }
        }

        // 释放技能
        public void CastingSkill(Role currRole, Role target)
        {
            roleSkillPanel.SetActive(true);
            for (int i = 0; i < currRole.roleData.skillList.Count; i++)
            {
                GameObject obj = Instantiate(roleSkillUIButtonPrefab, roleSkillPanel.transform);
                Button btn = obj.GetComponent<Button>();
                SkillData skillData = currRole.roleData.skillList[i];

                // 读取信息到button 显示
                btn.GetComponentInChildren<Text>().text = skillData.name_;
                // 注册按钮事件
                btn.onClick.AddListener(() => OnClick_CastingSkill(currRole, skillData, target));
            }
        }


        // 点击对应技能按钮，创建对应技能
        public void OnClick_CastingSkill(Role currRole, SkillData skillData, Role target)
        {
            //Debug.Log(target.name + "受到" + skillData.value + "点伤害！");
            //target.roleData.hp -= skillData.value;
            target.OnHurt(skillData.value);

            roleSkillPanel.SetActive(false);
            foreach (Transform tran in roleSkillPanel.transform)
            {
                Destroy(tran.gameObject);
            }
        }

        // 点击对应角色按钮，创建对应角色
        public void OnClick_CreateRole(Cell cell, GameObject roleObj)
        {
            Role role = Instantiate(roleObj, cell.rolePos.transform).GetComponent<Role>();
            cell.currRole = role;
            role.currCell = cell;

            roleSelectPanel.SetActive(false);
            foreach (Transform tran in roleSelectPanel.transform)
            {
                Destroy(tran.gameObject);
            }

            Player.Instance.playerState = PlayerState.Selected;
        }
    }
}
