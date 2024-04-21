using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DemoTemp
{
    [System.Serializable]
    public class Task
    {
        public string taskName;
        public int taskID;
        // 任务分类
        public TaskCategory taskCategory;
        // 任务状态
        public TaskStatus taskStatus;
        // 任务详情
        public string taskDetails;
        // 任务奖励
        //public List<> Rewards;
        // 任务完成条件
        public List<Condition> completionConditions;
        // 任务失败条件
        public List<Condition> failureConditions;
        // 任务领取条件
        public List<Condition> acceptanceConditions;

        public void Accept()
        {
            // 任务被接取的逻辑
        }

        public void Abandon()
        {
            // 任务被放弃的逻辑
        }

        public void Complete()
        {
            // 任务被完成的逻辑
        }
    }
}
