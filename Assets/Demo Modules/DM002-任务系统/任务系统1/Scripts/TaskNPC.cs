using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace DemoTemp
{
    public class TaskNPC : MonoBehaviour,IPointerClickHandler
    {
        public int NPCID { get; set; }
        public string NPCName { get; set; }
        public bool HasTask { get; set; }
        public Task CurrentTask { get; set; }
        public TaskStatus CurrentTaskStatus { get; set; }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(gameObject.name);
        }
    }
}
