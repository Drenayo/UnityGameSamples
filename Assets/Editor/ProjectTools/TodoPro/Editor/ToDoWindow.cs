#if ODIN_INSPECTOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ToDoPro
{
    public class ToDoWindow : EditorWindow
	{
		private ToDoList taskSO;
		private static ToDoWindow _window;

		private static string listSOPath = "Assets/Editor/ProjectTools/TodoPro/Data/ToDo.asset";
		
		private int currTaskGroupIndex = 0; // 当前所选组
		private int currTaskListIndex = 0;  // 当前所选列表

		private List<Task> currTasks;       // 当前需要绘制的任务集合
		private TaskList currTaskList;      // 当前需要绘制的任务列表项

		private string newTask;

		private Vector2 scrollPosition = Vector2.zero;
		private GUIStyle taskTextStyle;
		private int displayCount = 0;
		private GUILayoutOption heightOption;

		[MenuItem("Tools/ProjectTools/ToDo")]
		public static void Init()
		{
			_window = (ToDoWindow)EditorWindow.GetWindow(typeof(ToDoWindow));
			_window.titleContent = new GUIContent("待办");
			// 当场景发生变化，是否重新绘制
			_window.autoRepaintOnSceneChange = false;
		}

		//[PreferenceItem("ToDo Setting")]
		//private static void SelfPreferenceItem()
		//{
		//	EditorGUILayout.LabelField("ToDo源SO：", EditorStyles.boldLabel);
		//	listSOPath = EditorGUILayout.TextField(listSOPath, GUILayout.Height(40));
		//	EditorGUILayout.Space();
		//}

		public void OnGUI()
		{
			// 加载或创建任务列表资源
			if (taskSO == null)
			{
				taskSO = AssetDatabase.LoadAssetAtPath(listSOPath, typeof(ToDoList)) as ToDoList;
				if (taskSO == null)
                {
					EditorGUILayout.LabelField("无待办数据！！！！", EditorStyles.boldLabel);
				}
			}

			// 获取当前组名，列表名，任务名的数组
			string[] taskGroup = new string[taskSO.listGroup.Count];
			string[] taskList = new string[taskSO.listGroup[currTaskGroupIndex].taskList.Count];

			for (int i = 0; i < taskSO.listGroup.Count; i++)
			{
				taskGroup[i] = taskSO.listGroup[i].groupName;
			}

			for (int i = 0; i < taskSO.listGroup[currTaskGroupIndex].taskList.Count; i++)
			{
				taskList[i] = taskSO.listGroup[currTaskGroupIndex].taskList[i].listName;
			}

			// 获取当前需要绘制的任务列表
			currTasks = taskSO.listGroup[currTaskGroupIndex].taskList[currTaskListIndex].tasks;
			currTaskList = taskSO.listGroup[currTaskGroupIndex].taskList[currTaskListIndex];

			// 使用水平布局，后续元素将横向排列
			//EditorGUILayout.BeginHorizontal();
			//EditorGUILayout.LabelField("小梦的", EditorStyles.centeredGreyMiniLabel);
			//EditorGUILayout.EndHorizontal();

			// 创建一个下拉列表，index代表当前索引，labels代表需要下拉的项
			EditorGUILayout.BeginHorizontal();
			currTaskGroupIndex = EditorGUILayout.Popup(currTaskGroupIndex, taskGroup);
			currTaskListIndex = EditorGUILayout.Popup(currTaskListIndex, taskList);
			EditorGUILayout.EndHorizontal();


			// 显示列表
			taskTextStyle = new GUIStyle(EditorStyles.miniBoldLabel); // EditorStyles.wordWrappedMiniLabel 是 Unity 编辑器提供的一个内置样式，通常用于显示小型标签文本，允许文本自动换行。
			taskTextStyle.alignment = TextAnchor.UpperLeft;
			// taskTextStyle.wordWrap = true;

			// 设置滚动视图
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

			if (taskSO.listGroup[currTaskGroupIndex].taskList.Count > 0)
            {
				// 显示 未完成 Task项
				for (int i = 0; i < currTasks.Count; i++)
				{
					if (currTasks.Count > 0 && !currTasks[i].isComplete)
					{
						CreateUnDoneTaskItem(currTasks[i]);
					}
				}

				if (displayCount == 0)
				{
					EditorGUILayout.LabelField("现在是摸鱼时间！~", EditorStyles.largeLabel);
				}

				// 显示 已完成待办
				for (int i = 0; i < currTasks.Count; i++)
				{
					if (currTasks.Count > 0 && currTasks[i].isComplete)
					{
						CreateDoneTaskItem(currTasks[i],i);
					}
				}
			}
			else
            {
				EditorGUILayout.LabelField("当前列表项为空！~", EditorStyles.largeLabel);
			}
			
			displayCount = 0;
			EditorGUILayout.EndScrollView();

			// 创建任务
			newTask = EditorGUILayout.TextField(newTask, GUILayout.Height(40));
			if ((GUILayout.Button("创建新待办") && newTask != ""))
			{
				currTasks.Add(new Task(newTask, currTaskList.listSign, GetDate()));
				newTask = "";
				GUI.FocusControl(null);

				EditorUtility.SetDirty(taskSO);
				AssetDatabase.SaveAssets();
			}


			// 时刻检查并保存，太卡，改成了当标签被编辑时，才被保存
			//if (GUI.changed)
			//{
			//	EditorUtility.SetDirty(taskSO);
			//	AssetDatabase.SaveAssets();
			//}
		}

		// 创建未完成待办显示项
		public void CreateUnDoneTaskItem(Task currTask)
		{
			taskTextStyle.normal.textColor = currTaskList.color;
			displayCount++;
			EditorGUILayout.BeginHorizontal();

			if (EditorGUILayout.Toggle(currTask.isComplete, GUILayout.Width(15), GUILayout.Height(28)) == true)
			{
				currTask.isComplete = true;
			}
			currTask.taskName = EditorGUILayout.TextField(currTask.taskName, taskTextStyle);

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}

		// 创建已完成待办显示项
		public void CreateDoneTaskItem(Task currTask,int index)
		{
			taskTextStyle.normal.textColor = Color.grey;
			GUIStyle newStyle = taskTextStyle;
			newStyle.alignment = TextAnchor.MiddleLeft;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(currTask.taskName, newStyle);
			//if (GUILayout.Button("X", GUILayout.Width(18), GUILayout.Height(18)))
			//{
			//	currTasks.RemoveAt(index);
			//}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}

		/// <summary>
		/// 得到当前日期
		/// </summary>
		/// <returns></returns>
		public string GetDate()
		{
			DateTime currentDate = DateTime.Now;
			string formattedDate = currentDate.ToString("yyyy-MM-dd");
			return formattedDate;
		}

		void OnDestroy()
		{
			if(taskSO!=null)
			EditorUtility.SetDirty(taskSO);
			AssetDatabase.SaveAssets();
		}
	}
}
#endif