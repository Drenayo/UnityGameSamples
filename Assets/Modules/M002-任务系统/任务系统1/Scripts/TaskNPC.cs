using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace DemoTemp
{
    public class TaskNPC : MonoBehaviour,IPointerClickHandler
    {
        public int NPCID;
        public string NPCName;

        [Header("是否存在任务")]
        public bool hasTask;
        [Header("是否可以接取任务")]
        public bool canAcceptTask;
        [Header("当前任务")]
        public Task currentTask;
        [Header("当前任务状态")]
        public TaskStatus CurrentTaskStatus;

        [SerializeField]
        private List<Task> taskList;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"{NPCName}被点击，当前拥有任务数量{taskList.Count}");
        }


    }
}
