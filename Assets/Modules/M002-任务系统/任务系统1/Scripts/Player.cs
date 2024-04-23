using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DemoTemp
{
    public class Player
    {
        public static Player Instance;
        
        [SerializeField]
        private List<Task> acceptedTasks = new List<Task>();
        [SerializeField]
        private List<Task> completedTasks = new List<Task>();
        
        public Task currentTask;

        public void Awake()
        {
            Instance = this;
        }

        public void AcceptTask(Task task)
        {
            if (currentTask.taskStatus == TaskStatus.Completed)
            {
                completedTasks.Add(currentTask);
                currentTask = task;
                acceptedTasks.Add(currentTask);
            }
            Debug.Log($"玩家接取任务:{task.taskName}");
        }
    }
}
