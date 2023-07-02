using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_18
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            SQLiteDB sql = new SQLiteDB();
            sql.OpenDB("Data Source=" + Application.dataPath + "/Config/Z_18/Test.db");

            SqliteDataReader sqlReader = sql.ReadFullTable("MapTable");

            while (sqlReader.Read())
            {
                string str = string.Empty;
                str += "ID:";
                str += sqlReader.GetInt32(0).ToString();
                str += ",Name:";
                str += sqlReader.GetString(1);
                str += ",Path:";
                str += sqlReader.GetString(2);
                Debug.Log(str);
            }
        }
    }
}
