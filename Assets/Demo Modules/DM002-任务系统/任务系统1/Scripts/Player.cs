using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DemoTemp
{
    public class Player
    {
        public List<Task> AcceptedTasks { get; set; }
        public List<Task> CompletedTasks { get; set; }
        public Task CurrentTask { get; set; }

        public void AcceptTask(int taskID)
        {
            // 接取任务的逻辑
        }

        public void AbandonTask(int taskID)
        {
            // 放弃任务的逻辑
        }
    }
}
