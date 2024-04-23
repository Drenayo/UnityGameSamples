#if ODIN_INSPECTOR

using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;


namespace ToDoPro
{
	// 待做：按照日期排序

	//[CreateAssetMenu(fileName = "ToDoData", menuName = "任务/创建TodoData", order = 451)]
	public class ToDoList : ScriptableObject
	{
		/// <summary>
		/// 任务分组列表
		/// </summary>
		[ListDrawerSettings(HideAddButton = true)] 
		public List<TaskGroup> listGroup = new List<TaskGroup>();

		public ToDoList()
		{
			listGroup.Add(new TaskGroup("分组1", GroupSign.壹));
		}



		// 新列添加到的组
		[LabelText("选择需要添加列表的组：")]
		public GroupSign addSelectGroupSign;

		[Button("添加组"),ButtonGroup("Btn")]
		public void AddGroup()
		{
			listGroup.Add(new TaskGroup("新分组", (GroupSign)Enum.Parse(typeof(GroupSign), listGroup.Count.ToString())));
		}

		[Button("添加列表"), ButtonGroup("Btn")]
		public void AddList()
		{
			foreach (TaskGroup item in listGroup)
			{
				if (item.groupSign == addSelectGroupSign)
				{
					item.taskList.Add(new TaskList("新列表", addSelectGroupSign, (ListSign)Enum.Parse(typeof(ListSign), item.taskList.Count.ToString())));
				}
			}
		}
	}

	/// <summary>
	/// 任务组  一级分类
	/// </summary>
	[System.Serializable]
	public class TaskGroup
	{
		/// <summary>
		/// 分组组名
		/// </summary>
		[HorizontalGroup("Setting"), LabelText("分组名")]
		public string groupName;
		/// <summary>
		/// 分组序号
		/// </summary>
		[HorizontalGroup("Setting"), LabelText("分组序列"),ReadOnly]
		public GroupSign groupSign;

		/// <summary>
		/// 分组所属任务列表
		/// </summary>
		[ListDrawerSettings(HideAddButton = true)]
		public List<TaskList> taskList = new List<TaskList>();

		public TaskGroup(string groupName, GroupSign groupSign)
		{
			this.groupName = groupName;
			this.groupSign = groupSign;
		}
	}

	/// <summary>
	/// 任务列表  二级分类
	/// </summary>
	[Serializable]
	public class TaskList
	{
		/// <summary>
		/// 任务列表名
		/// </summary>
		[HorizontalGroup("Setting",.5f), LabelText("列表名")]
		public string listName;
		/// <summary>
		/// 任务列表颜色
		/// </summary>
		[HorizontalGroup("Setting"), HideLabel]
		public Color color;
		/// <summary>
		/// 任务列表所属分组
		/// </summary>
		[HorizontalGroup("Setting"), HideLabel]
		public GroupSign groupSign;

		/// <summary>
		/// 任务列表序号
		/// </summary>
		[HorizontalGroup("Setting"), HideLabel]
		public ListSign listSign;

		/// <summary>
		/// 任务列表
		/// </summary>
		public List<Task> tasks = new List<Task>();

		public TaskList(string name, GroupSign index,ListSign listSign)
		{
			this.listName = name;
			this.color = Color.white;
			this.groupSign = index;
			this.listSign = listSign;
		}
	}

	/// <summary>
	/// 任务
	/// </summary>
	[Serializable]
	public class Task
	{
		/// <summary>
		/// 任务是否完成
		/// </summary>
		[HorizontalGroup("Setting",.04f), HideLabel]
		public bool isComplete;
		/// <summary>
		/// 任务所属标签序号
		/// </summary>
		[HorizontalGroup("Setting",.15f),HideLabel]
		public ListSign listSign;
		/// <summary>
		/// 创建日期
		/// </summary>
		[ReadOnly,HorizontalGroup("Setting",.25f), HideLabel]
		public string creationDate;
		/// <summary>
		/// 任务名
		/// </summary>
		[HorizontalGroup("Setting",.5f),HideLabel]
		public string taskName;

		public Task(string task, ListSign listSign, string date)
		{
			this.taskName = task;
			this.isComplete = false;
			this.listSign = listSign;
			creationDate = date;
		}
	}



	[Serializable]
	public class ToDoSetting
	{
		/// <summary>
		/// 是否自动生成（当找不到TODO文件的时候，防止Bug,文件被覆盖）
		/// </summary>
		public bool isAutoCreate;
	}

	[Serializable]
	public enum GroupSign
	{
		壹,
		贰,
		叁,
		肆,
		伍,
		陆,
		柒,
		捌,
		玖,
		拾
	}
	
	[Serializable]
	public enum ListSign
	{
		A,
		B,
		C,
		D,
		E,
		F,
		G,
		H,
		I,
		J,
		K,
		L,
		M,
		N,
		O,
		P,
		Q,
		R,
		S,
		T,
		U,
		V,
		W,
		X,
		Y,
		Z
	}
}
#endif