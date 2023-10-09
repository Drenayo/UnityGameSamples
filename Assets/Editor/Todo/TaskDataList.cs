using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;


public class TaskDataList : ScriptableObject
{
	public List<TaskLabel> labels = new List<TaskLabel>();
	public List<Task> tasks = new List<Task>();
	
	public TaskDataList ()
	{	
		labels.Add( new TaskLabel("常规任务", Color.white,0) );		
		labels.Add( new TaskLabel("紧急任务", Color.red,1) );
		labels.Add( new TaskLabel("正在完成", Color.cyan,2) );
		labels.Add( new TaskLabel("注意事项", Color.yellow,3) );								
	}
	
	public void AddTask( TaskLabel owner, string task)
	{
		Task item = new Task( owner, task );
		tasks.Add(item);
	}
}

/// <summary>
/// 任务
/// </summary>
[Serializable]
public class Task
{
	public TaskLabel label;
	public string taskName;
	public bool isComplete;
	
	public Task( TaskLabel label, string task )
	{
		this.label = label;
		this.taskName = task;
		this.isComplete = false;
	}
}

/// <summary>
/// 任务标签
/// </summary>
[Serializable]
public class TaskLabel
{
	public string name;
	public Color color;
	public int labelIndex;
	
	public TaskLabel( string name, Color color , int index)
	{
		this.name = name;
		this.color = color;
		this.labelIndex = index;
	}
}
