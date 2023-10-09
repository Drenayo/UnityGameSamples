using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class TodoList : EditorWindow
{
	private static TodoList _window;
	private TaskDataList taskDataList;
    private string _listDataDirectory = "/Resources/Todo/";
	private string _listDataAssetPath = "Assets/Resources/Todo/TodoList.asset";
	private int currentSelectLabelIndex = 0;
	private int newTaskLabelIndex = 0;
	private string newTask;
	private bool showCompletedTasks = true;
	private Vector2 scrollPosition = Vector2.zero;
	private GUIStyle taskTextStyle;
	private int displayCount = 0;
	private GUILayoutOption heightOption;
	[MenuItem ("Tool/Todo Window")]
    public static void Init ()
    {
        _window = ( TodoList )EditorWindow.GetWindow (typeof ( TodoList ));
		_window.titleContent = new GUIContent("待办事项");
		// 当场景发生变化，是否重新绘制
		_window.autoRepaintOnSceneChange = false;
    }
    
	public void OnGUI ()
	{
		// 加载或创建任务列表资源
		if (taskDataList == null)
		{
			taskDataList = AssetDatabase.LoadAssetAtPath( _listDataAssetPath, typeof(TaskDataList)) as TaskDataList;
			if(taskDataList == null)
			{
				// 自动创建一个ToDo资源
				taskDataList = ScriptableObject.CreateInstance(typeof(TaskDataList)) as TaskDataList;
                System.IO.Directory.CreateDirectory(Application.dataPath + _listDataDirectory);
				AssetDatabase.CreateAsset(taskDataList, _listDataAssetPath );
				GUI.changed = true;				
			}						
		}

		// +1 是因为下拉列表项需要有一个All Tasks
		string[] labels = new string[taskDataList.labels.Count + 1];
		string[] labelsToSelect = new string[taskDataList.labels.Count];
		
		labels[0] = "All Tasks";
		for(int i = 0; i < taskDataList.labels.Count; i++)
		{
			labels[i+1] = taskDataList.labels[i].name;
			labelsToSelect[i] = taskDataList.labels[i].name;
		}

		// 使用水平布局，后续元素将横向排列
		EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("小梦的待办列表:", EditorStyles.boldLabel);
		// 创建一个下拉列表，index代表当前索引，labels代表需要下拉的项
        currentSelectLabelIndex = EditorGUILayout.Popup(currentSelectLabelIndex, labels);
		newTaskLabelIndex = currentSelectLabelIndex - 1;
		EditorGUILayout.EndHorizontal();
		

		// 显示列表
		taskTextStyle = new GUIStyle(EditorStyles.miniBoldLabel); // EditorStyles.wordWrappedMiniLabel 是 Unity 编辑器提供的一个内置样式，通常用于显示小型标签文本，允许文本自动换行。
		taskTextStyle.alignment = TextAnchor.UpperLeft;
		// taskTextStyle.wordWrap = true;

		// 设置滚动视图
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

		// 显示 未完成 Task项
		for( int i = 0; i < taskDataList.tasks.Count; i++)
		{
			Task task = taskDataList.tasks[i];
			TaskLabel taskLabel = task.label;

			//Debug.Log(currentSelectLabelIndex);
			if(currentSelectLabelIndex == 0 && task.isComplete == false)
			{	
				CreateUnDoneTaskItem(task, i);
			}
            else if(currentSelectLabelIndex > 0)
            {
                int adjustedIndex = currentSelectLabelIndex - 1;
                taskLabel = taskDataList.labels[adjustedIndex];
                if (taskLabel.name == task.label.name && task.isComplete == false)
                {
                    CreateUnDoneTaskItem(task, i);
                }
            }
        }

        // 显示 已完成待办
        for (int i = 0; i < taskDataList.tasks.Count; i++)
        {
            Task task = taskDataList.tasks[i];
            TaskLabel taskLabel = task.label;

            if (currentSelectLabelIndex == 0 && task.isComplete)
            {
                CreateDoneTaskItem(task, Color.gray, i);
            }
			else if(currentSelectLabelIndex > 0)
			{
                int adjustedIndex = currentSelectLabelIndex - 1;
                taskLabel = taskDataList.labels[adjustedIndex];
                if (taskLabel.name == task.label.name && task.isComplete)
                {
                    CreateDoneTaskItem(task, Color.gray, i);
                }
            }
        }

        if (displayCount == 0)
        {
			EditorGUILayout.LabelField("现在是摸鱼时间！~", EditorStyles.largeLabel);
        }
		EditorGUILayout.EndScrollView();

		// 创建任务
		//EditorGUILayout.BeginHorizontal();
		////EditorGUILayout.LabelField("Create Task:", EditorStyles.boldLabel);
		//EditorGUILayout.EndHorizontal();
		newTask = EditorGUILayout.TextField(newTask, GUILayout.Height(40));
		if( ( GUILayout.Button("创建新待办") && newTask != "" ) && newTaskLabelIndex  >= 0)
		{
			TaskLabel newOwner = taskDataList.labels[newTaskLabelIndex];
			taskDataList.AddTask(newOwner, newTask);			
			newTask = "";
			GUI.FocusControl(null);				
		}
		if (GUI.changed)
		{
			EditorUtility.SetDirty(taskDataList);
			AssetDatabase.SaveAssets();	
		}	
	}

	// 创建未完成待办显示项
	public void CreateUnDoneTaskItem(Task currTask,int forI)
    {
		UpdateLable();
		taskTextStyle.normal.textColor = currTask.label.color;
		displayCount++;
		EditorGUILayout.BeginHorizontal();

		if (EditorGUILayout.Toggle(currTask.isComplete, GUILayout.Width(15), GUILayout.Height(28)) == true)
		{
			taskDataList.tasks[forI].isComplete = true;
		}
		taskDataList.tasks[forI].taskName = EditorGUILayout.TextField(currTask.taskName, taskTextStyle);

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}
	
	// 创建已完成待办显示项
	public void CreateDoneTaskItem(Task currTask,Color color,int forI)
    {
		taskTextStyle.normal.textColor = color;
		GUIStyle newStyle = taskTextStyle;
		newStyle.alignment = TextAnchor.MiddleLeft;

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(currTask.taskName, newStyle);
		if (GUILayout.Button("X", GUILayout.Width(18), GUILayout.Height(18)))
		{
			taskDataList.tasks.RemoveAt(forI);
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}

	// 更新Task的Lable配置 保证在Asset的更改能在下次OnGUI生效
	public void UpdateLable()
	{
		for (int i = 0; i < taskDataList.tasks.Count; i++)
		{
			Task task = taskDataList.tasks[i];
			for (int j = 0; j < taskDataList.labels.Count; j++)
            {
				TaskLabel taskLabel = taskDataList.labels[j];
				if (taskLabel.labelIndex == task.label.labelIndex)
                {
					task.label.color = taskLabel.color;
					task.label.name = taskLabel.name;
                }
            }

		}
	}

		void OnDestroy()
	{
		EditorUtility.SetDirty(taskDataList);
		AssetDatabase.SaveAssets();
	}	
}
