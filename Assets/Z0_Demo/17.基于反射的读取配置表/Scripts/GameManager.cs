using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_17
{ 
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            ConfigTable<RoleInfoData> config = new ConfigTable<RoleInfoData>();
            config.Load(new ConfigPathTable("/Config/Z_17/ConfigPath.csv").GetPathByType(typeof(RoleInfoData)));

            foreach (KeyValuePair<int, RoleInfoData> info in config.dicConfigTable)
            {
                Debug.Log(info.ToString());
            }
        }
    }
}
