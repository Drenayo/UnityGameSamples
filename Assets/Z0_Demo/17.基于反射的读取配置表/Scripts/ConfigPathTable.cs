using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_17
{
    public class ConfigPathTableData : ConfigTableData
    {
        public string Type;
        public string Path;
    }
    public class ConfigPathTable
    {
        private Dictionary<Type, ConfigPathTableData> dicConfigTablePath = new Dictionary<Type, ConfigPathTableData>();
        public ConfigPathTable(string path) //"/Config/Z_17/ConfigPath.csv"
        {
            // 初始化先加载配置表的路径配置表
            // 然后根据泛型类型，来加载路径配置表中的表数据
            ConfigTable<ConfigPathTableData> config = new ConfigTable<ConfigPathTableData>();
            List<ConfigPathTableData> pathList = new List<ConfigPathTableData>();

            pathList = config.Load(path);

            foreach (ConfigPathTableData item in pathList)
            {
                dicConfigTablePath[Type.GetType(typeof(ConfigPathTableData).Namespace + "." + item.Type)] = new ConfigPathTableData() { ID = item.ID, Type = item.Type, Path = item.Path };
            }
        }

        public string GetPathByType(Type type)
        {
            ConfigPathTableData config = new ConfigPathTableData();
            if (dicConfigTablePath.TryGetValue(type, out config))
            {
                return config.Path;
            }
            return null;
        }
    }
}
