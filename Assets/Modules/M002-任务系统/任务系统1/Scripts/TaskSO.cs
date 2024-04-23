using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DemoTemp
{
    [CreateAssetMenu(menuName = "DM配置项/DM002-1/创建任务SO")]
    public class TaskSO : ScriptableObject
    {
        public List<Task> taskList;
    }
}

