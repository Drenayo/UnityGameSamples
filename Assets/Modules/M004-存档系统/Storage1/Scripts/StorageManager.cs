using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Storage1
{
    // �浵������
    public class StorageManager : MonoBehaviour
    {
        #region ����
        public static StorageManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        // ��ɫԤ���壬���ڼ���ʱʵ����
        public CharacterInfo characterPrefab;
        // ��ɫ���飬���ڴ�Ų��ҵ��ĳ����еĽ�ɫ��Ȼ��浵
        public CharacterInfo[] characters;
        // ȫ�����Խṹ�����
        public AllCharacterData allCharacterData;


        #region ��������
        /// <summary>
        /// �浵
        /// </summary>
        public void Save()
        {
            Debug.Log("�浵");

            // ��ȡ���������н�ɫ�������ڴ洢��ɫ������Ϣ
            characters = FindObjectsOfType<CharacterInfo>();

            // ��ʼ�����Խṹ������
            allCharacterData.characterData = new CharacterData[characters.Length];

            // �������������н�ɫ���󣬰ѽ�ɫ������Ϣ����ṹ����
            for (int i = 0; i < characters.Length; i++)
            {
                // ���ý�ɫ�������Ϣ�洢����
                characters[i].SaveInfo();

                // �ѽ�ɫ����Ľṹ����Ϣ���뵱ǰ�ṹ��������
                allCharacterData.characterData[i] = characters[i].characterData;
            }

            // ����һ���浵�ļ�
            FileStream saveFileStream = new FileStream(Path.Combine(Application.dataPath, "4.Storage System/Storage1/Config/�浵.txt"), FileMode.Create);

            // �ѽṹ������ļ���
            // ����ͨ��ToJsonת��string,Ȼ��ͨ��GetBytesת���ֽ�����Ȼ������ļ�
            saveFileStream.Write(System.Text.Encoding.Default.GetBytes(JsonUtility.ToJson(allCharacterData)),
             0, System.Text.Encoding.Default.GetBytes(JsonUtility.ToJson(allCharacterData)).Length);

            // �ر��ļ�
            saveFileStream.Close();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Load()
        {
            Debug.Log("����");

            // ��ȡ���������н�ɫ���󣬶���ʱȫ��ɾ��Ȼ������ʵ����
            characters = FindObjectsOfType<CharacterInfo>();

            // �򿪴浵�ļ�
            FileStream loadFileStream = new FileStream(Path.Combine(Application.dataPath, "4.Storage System/Storage1/Config/�浵.txt"), FileMode.Open);

            // ʵ�����ֽ����飬���ڴ�Ŷ�ȡ�����Ĵ浵��Ϣ
            byte[] loadByte = new byte[loadFileStream.Length];

            // ��ȡ�浵��Ϣ
            loadFileStream.Read(loadByte, 0, (int)loadFileStream.Length);

            // �ر��ļ�
            loadFileStream.Close();

            // ����ȡ�������ֽ�������ϢתΪstring����ͨ��FromJson�������ض��ṹ��
            allCharacterData = JsonUtility.FromJson<AllCharacterData>(System.Text.Encoding.Default.GetString(loadByte));

            // ɾ�����������н�ɫ����
            for (int i = 0; i < characters.Length; i++)
            {
                Destroy(characters[i].gameObject);
            }

            // ���°��մ浵����ʵ������ɫ�������Ϣ
            for (int i = 0; i < allCharacterData.characterData.Length; i++)
            {
                CharacterInfo roleTemp = Instantiate(characterPrefab);

                // ���ṹ�����Ϣ�����ɫ���ݽṹ�����
                roleTemp.characterData = allCharacterData.characterData[i];

                // Ȼ����ý�ɫ����Ϣ���غ���
                roleTemp.LoadInfo();
            }
        }
        #endregion

    }

    /// <summary>
    /// ��ɫ�����ṹ�����飬���ڴ�Ŷ����ɫ��Ϣ
    /// </summary>
    [System.Serializable]
    public struct AllCharacterData
    {
        public CharacterData[] characterData;
    }


    /// <summary>
    /// ��ɫ������Ϣ�ṹ��
    /// Ŀǰֻ������Ѫ����λ����Ϣ����������ֱ����չ
    /// </summary>
    [System.Serializable]
    public struct CharacterData
    {
        public float currHP;
        public float maxHP;
        public Vector3 pos;
        public Vector3 rot;
        public Vector3 sal;
    }

}