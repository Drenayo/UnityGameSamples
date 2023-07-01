using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_17
{
    public class RoleInfoData:ConfigTableData
    {
        public string Name;
        public string Info;
        public RoleInfoData() { }
        public RoleInfoData(int id, string name, string info)
        {
            ID = id;
            Name = name;
            Info = info;
        }
        public override string ToString()
        {
            return $"{ID.ToString()},{Name},{Info}";
        }
    }
}
