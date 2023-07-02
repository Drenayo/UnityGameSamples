using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_17
{
    public class RolePropertyData: ConfigTableData
    {
        public int HP;
        public int Attack;
        public int Speed;
        public RolePropertyData() { }
        public RolePropertyData(int id, int hp, int attack, int spped)
        {
            this.ID = id;
            this.HP = hp;
            this.Attack = attack;
            this.Speed = spped;
        }
        public override string ToString()
        {
            return $"{ID},{HP},{Attack},{Speed}";
        }
    }
}
