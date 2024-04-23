using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Storage1
{
    // 存档管理类
    public class StorageManager : MonoBehaviour
    {
        #region 单例
        public static StorageManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        // 角色预制体，用于加载时实例化
        public CharacterInfo characterPrefab;
        // 角色数组，用于存放查找到的场景中的角色，然后存档
        public CharacterInfo[] characters;
        // 全部属性结构体变量
        public AllCharacterData allCharacterData;


        #region 公共方法
        /// <summary>
        /// 存档
        /// </summary>
        public void Save()
        {
            Debug.Log("存档");

            // 获取场景中所有角色对象，用于存储角色对象信息
            characters = FindObjectsOfType<CharacterInfo>();

            // 初始化属性结构体数组
            allCharacterData.characterData = new CharacterData[characters.Length];

            // 遍历场景中所有角色对象，把角色对象信息存入结构体中
            for (int i = 0; i < characters.Length; i++)
            {
                // 调用角色对象的信息存储函数
                characters[i].SaveInfo();

                // 把角色对象的结构体信息存入当前结构体数组中
                allCharacterData.characterData[i] = characters[i].characterData;
            }

            // 创建一个存档文件
            FileStream saveFileStream = new FileStream(Path.Combine(Application.dataPath, "4.Storage System/Storage1/Config/存档.txt"), FileMode.Create);

            // 把结构体存入文件中
            // 首先通过ToJson转成string,然后通过GetBytes转成字节数组然后存入文件
            saveFileStream.Write(System.Text.Encoding.Default.GetBytes(JsonUtility.ToJson(allCharacterData)),
             0, System.Text.Encoding.Default.GetBytes(JsonUtility.ToJson(allCharacterData)).Length);

            // 关闭文件
            saveFileStream.Close();
        }

        /// <summary>
        /// 读档
        /// </summary>
        public void Load()
        {
            Debug.Log("读档");

            // 获取场景中所有角色对象，读档时全部删除然后重新实例化
            characters = FindObjectsOfType<CharacterInfo>();

            // 打开存档文件
            FileStream loadFileStream = new FileStream(Path.Combine(Application.dataPath, "4.Storage System/Storage1/Config/存档.txt"), FileMode.Open);

            // 实例化字节数组，用于存放读取出来的存档信息
            byte[] loadByte = new byte[loadFileStream.Length];

            // 读取存档信息
            loadFileStream.Read(loadByte, 0, (int)loadFileStream.Length);

            // 关闭文件
            loadFileStream.Close();

            // 将读取出来的字节数组信息转为string，再通过FromJson解析成特定结构体
            allCharacterData = JsonUtility.FromJson<AllCharacterData>(System.Text.Encoding.Default.GetString(loadByte));

            // 删除场景中所有角色对象
            for (int i = 0; i < characters.Length; i++)
            {
                Destroy(characters[i].gameObject);
            }

            // 重新按照存档内容实例化角色对象和信息
            for (int i = 0; i < allCharacterData.characterData.Length; i++)
            {
                CharacterInfo roleTemp = Instantiate(characterPrefab);

                // 将结构体的信息赋予角色数据结构体变量
                roleTemp.characterData = allCharacterData.characterData[i];

                // 然后调用角色的信息加载函数
                roleTemp.LoadInfo();
            }
        }
        #endregion

    }

    /// <summary>
    /// 角色基本结构体数组，用于存放多个角色信息
    /// </summary>
    [System.Serializable]
    public struct AllCharacterData
    {
        public CharacterData[] characterData;
    }


    /// <summary>
    /// 角色基本信息结构体
    /// 目前只保存了血量和位置信息，后续可以直接扩展
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