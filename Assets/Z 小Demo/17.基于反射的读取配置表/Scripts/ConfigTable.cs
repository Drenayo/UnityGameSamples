using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Z_17
{
    public class ConfigTableData
    {
        public int ID;
    }

    public class ConfigTable<TableData> where TableData : ConfigTableData, new()
    {
        public Dictionary<int, TableData> dicConfigTable = new Dictionary<int, TableData>();
        //public ConfigTable(string path)
        //{
        //    //ConfigPathTable configPathTable = new ConfigPathTable();
        //    //string path = configPathTable.GetPathByType(typeof(TableData));
        //    Load(path);
        //}

        // 加载
        public List<TableData> Load(string path)
        {
            byte[] tableBytes;
            tableBytes = File.ReadAllBytes(Application.dataPath + path);
            return Read(tableBytes);
        }

        // 读取
        private List<TableData> Read(byte[] bytes)
        {
            List<TableData> list = new List<TableData>();
            MemoryStream stream = new MemoryStream(bytes);
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gb2312")))
            {
                // 获取字段字符串
                string fieldName = reader.ReadLine();
                // 获取字段数组
                string[] fieldNameArray = fieldName.Split(',');

                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] items = line.Split(',');
                    list.Add(ParseItem(fieldNameArray, items));
                    line = reader.ReadLine();
                }
            }
            return list;
        }

        // 解析
        private TableData ParseItem(string[] fieldNames, string[] items)
        {
            // 初始化
            TableData config = new TableData();
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            foreach (string fieldName in fieldNames)
                fieldInfos.Add(typeof(TableData).GetField(fieldName));


            // 解析
            for (int i = 0; i < fieldInfos.Count; i++)
            {
                FieldInfo field = fieldInfos[i];
                var data = items[i];
                if (items[i].Equals("")) continue; // 空行或ID为空跳过
                try
                {
                    if (field.FieldType == typeof(int))
                    {
                        field.SetValue(config, int.Parse(data));
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        field.SetValue(config, data);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            dicConfigTable[config.ID] = config;
            return config;
        }

        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TableData GetDataByID(int id)
        {
            TableData config = new TableData();
            if (dicConfigTable.TryGetValue(id, out config))
            {
                return config;
            }
            return null;
        }
    }
}
