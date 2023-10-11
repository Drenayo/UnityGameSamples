//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using System;
//using Sirenix.OdinInspector;

//public class TaskDataList : ScriptableObject
//{
//	[LabelText("分组标签")]
//	public List<TaskLabel> labels = new List<TaskLabel>();
//	[LabelText("待办列表")]
//	public List<Task> tasks = new List<Task>();
	
//	public TaskDataList ()
//	{	
//		labels.Add( new TaskLabel("常规任务", Color.white,0) );		
//		labels.Add( new TaskLabel("紧急任务", Color.red,1) );
//		labels.Add( new TaskLabel("正在完成", Color.cyan,2) );
//		labels.Add( new TaskLabel("注意事项", Color.yellow,3) );								
//	}
	
//	public void AddTask( TaskLabel owner, string task)
//	{
//		Task item = new Task( owner, task );
//		tasks.Add(item);
//	}
//}

///// <summary>
///// 任务
///// 任务名字
///// 任务是否完成
///// 任务标签ID
///// 
///// </summary>
//[Serializable]
//public class Task
//{
//	//public string taskName;
//	//public int labelIndex;
//	//public string timel;
//	//public bool isComplete;
	

//	public Task(string task,int index)
//	{
//		this.labelIndex = index;
//		this.taskName = task;
//		this.isComplete = false;
//	}
//}

///// <summary>
///// 任务标签
///// </summary>
//[Serializable]
//public class TaskLabel
//{
//	public string name;
//	public Color color;
//	public int labelIndex;
	
//	public TaskLabel( string name, Color color , int index)
//	{
//		this.name = name;
//		this.color = color;
//		this.labelIndex = index;
//	}
//}
