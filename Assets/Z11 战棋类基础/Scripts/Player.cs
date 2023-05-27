using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z11
{
    public enum PlayerState
    {
        // 游览，未选中任何角色
        Visit,

        // 正在选择创建角色
        ChosseRole,

        // 选中角色状态
        Selected,

        // 正在选择使用技能
        ChooseSkill,
    }

    public class Player : SingletonMonoBase<Player>
    {
        public PlayerState playerState;
        public Cell currHoverCell = null;
        public Role currHoverRole = null;
        public Role currSelectRole = null;

        private void Start()
        {
            playerState = PlayerState.Visit;
        }

        void Update()
        {
            RayCheck();
            CreateRole();
            SelectControl();
        }

        // 射线检测基础
        private GameObject RayCheckBase()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.gameObject;
            }
            else
                return null;
        }

        // Cell & Role 检测
        private void RayCheck()
        {
            GameObject ray = RayCheckBase();


            // 检测到Cell
            if (ray && ray.TryGetComponent<Cell>(out Cell cell))
            {
                cell.SetHighlight(true);

                // 检测到上一个Cell和当前的不一样
                if (currHoverCell && currHoverCell != cell)
                {
                    currHoverCell.SetHighlight(false);
                }

                // 更新上一个Cell
                currHoverCell = cell;
            }
            // 检测不到Cell了，最后一个LastHoverCell也要处理
            else if (currHoverCell)
            {
                currHoverCell.SetHighlight(false);
                currHoverCell = null;
            }


            // 检测到Role
            if (ray && ray.TryGetComponent<Role>(out Role role))
            {
                role.SetHighlight(true);
                if (currHoverRole && currHoverRole != role)
                {
                    currHoverRole.SetHighlight(false);
                }

                currHoverRole = role;
            }
            // 检测不到有挂载Role的物体了
            else if (currHoverRole)
            {
                currHoverRole.SetHighlight(false);
                currHoverRole = null;
            }
        }

        // 按住Alt + 鼠标左键 种角色
        private void CreateRole()
        {
            if (currHoverCell && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!currHoverCell.currRole && playerState != PlayerState.ChosseRole)
                {
                    RoleManager.Instance.CreateRole(currHoverCell);
                    playerState = PlayerState.ChosseRole;
                }

            }
        }

        // 点选操作控制
        private void SelectControl()
        {
            // 点中角色时
            if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftAlt))
            {
                // 点击角色时，把selected设true(如果之前已经有角色被选中，则需要把之前角色设flase)
                if (currHoverRole)
                {
                    if (currSelectRole)
                    {
                        currSelectRole.selected = false;
                        currSelectRole.SetHighlight(false);
                        currSelectRole = null;
                    }
                    currSelectRole = currHoverRole;
                    currSelectRole.selected = true;
                    playerState = PlayerState.Selected;
                }
                // 点击地面时，清空选择角色
                if (currHoverCell)
                {
                    if (currSelectRole)
                    {
                        currSelectRole.selected = false;
                        currSelectRole.SetHighlight(false);
                        currSelectRole = null;
                        playerState = PlayerState.Visit;
                    }
                }
            }

            // 右键点击地面，进行操作      // 这个操作应该也是一个列表，达成条件的供玩家选择使用，无法达成的则灰色
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                // 有目标地面和被选中角色 移动
                if (currHoverCell && currSelectRole)
                {
                    currSelectRole.Move(currHoverCell);
                }
                // 有目标点与被选中角色，释放技能
                else if (currHoverRole && currSelectRole)
                {
                    currSelectRole.CastingSkill(currHoverRole);
                    playerState = PlayerState.ChooseSkill;
                }
            }
        }
    }
}
