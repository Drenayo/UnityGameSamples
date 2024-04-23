using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DemoTemp
{
    [System.Serializable]
    public class Condition
    {
        // 条件名字
        public string conditionName;
        // 条件ID
        public int conditionID;
        // 条件类型
        public ConditionType conditionType;

        public bool IsSatisfied()
        {
            // 判断条件是否满足的逻辑
            return true;
        }
    }
}
