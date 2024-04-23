using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Storage1
{
    /// <summary>
    /// ��ɫ��Ϣ��
    /// </summary>
    public class CharacterInfo : MonoBehaviour
    {
        // ��ɫ���� �ṹ�����
        public CharacterData characterData;
        // ��ɫ������Ϣ
        public float maxHP;
        public float currHP;

        // �������� ����ɫ������Ϣ��䵽�ṹ�������ȥ������SaveMgr�洢
        public void SaveInfo()
        {
            characterData.currHP = currHP;
            characterData.maxHP = maxHP;
            characterData.pos = transform.localPosition;
            characterData.rot = transform.localEulerAngles;
            characterData.sal = transform.localScale;
        }

        // �������� ���ṹ�������ֵ�����ɫ��ʵ�ּ���
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