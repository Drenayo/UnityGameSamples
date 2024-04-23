using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Storage1
{
    /// <summary>
    /// 角色信息类
    /// </summary>
    public class CharacterInfo : MonoBehaviour
    {
        // 角色数据 结构体变量
        public CharacterData characterData;
        // 角色基本信息
        public float maxHP;
        public float currHP;

        // 保存数据 将角色基本信息填充到结构体变量中去，方便SaveMgr存储
        public void SaveInfo()
        {
            characterData.currHP = currHP;
            characterData.maxHP = maxHP;
            characterData.pos = transform.localPosition;
            characterData.rot = transform.localEulerAngles;
            characterData.sal = transform.localScale;
        }

        // 加载数据 将结构体变量的值赋予角色，实现加载
        public void LoadInfo()
        {
            currHP = characterData.currHP;
            maxHP = characterData.maxHP;
            transform.localPosition = characterData.pos;
            transform.localEulerAngles = characterData.rot;
            transform.localScale = characterData.sal;
        }
    }
}