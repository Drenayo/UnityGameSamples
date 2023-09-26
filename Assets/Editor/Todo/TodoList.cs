using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/*

This is a simple piece of todo software created by Will Stallwood of Cipher Prime.
http://www.cipherprime.com

*/

public class TodoList : EditorWindow
{
	private static TodoList _window;
	private ListData _listData;
    private string _listDataDirectory = "/Resources/Todo/";
	private string _listDataAssetPath = "Assets/Resources/Todo/TodoList.asset";
	private int _currentOwnerIndex = 0;
	private int _newTaskOwnerIndex = 0;
	private string _newTask;
	private bool showCompletedTasks = true;
	private Vector2 _scrollPosition = Vector2.zero;
	
	[MenuItem ("Window/Todo List %l")]
    public static void Init ()
    {
        // Get existing open window or if none, make a new one:
        _window = ( TodoList )EditorWindow.GetWindow (typeof ( TodoList ));
		_window.title = "Todo List";
		_window.autoRepaintOnSceneChange = false;
    }
    
	public void OnGUI ()
	{	
		// Create our data if we have none.
		if(_listData == null)
		{
			//Debug.Log("no asset file found, need to reload");
			_listData = AssetDatabase.LoadAssetAtPath( _listDataAssetPath, typeof(ListData)) as ListData;
			if(_listData == null)
			{
				//Debug.Log("no asset file found, could not reload");	
				_listData = ScriptableObject.CreateInstance(typeof(ListData)) as ListData;
                System.IO.Directory.CreateDirectory(Application.dataPath + _listDataDirectory);
				AssetDatabase.CreateAsset(_listData, _listDataAssetPath );
				GUI.changed = true;				
			}						
		}

		// display the filter fields
		string[] owners = new string[_listData.owners.Count + 1];
		string[] ownersToSelect = new string[_listData.owners.Count];
		
		owners[0] = "All Tasks";
		for(int i = 0; i < _listData.owners.Count; i++)
		{
			owners[i+1] = _listData.owners[i].name;
			ownersToSelect[i] = _listData.owners[i].name;
		}

		EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Show tasks:", EditorStyles.boldLabel);
            _currentOwnerIndex = EditorGUILayout.Popup(_currentOwnerIndex, owners);
		EditorGUILayout.EndHorizontal();
        
		// display the list
		GUIStyle itemStyle = new GUIStyle(EditorStyles.wordWrappedMiniLabel);
		itemStyle.alignment = TextAnchor.UpperLeft;
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
		int displayCount = 0;
		
		for( int i = 0; i < _listData.items.Count; i++)
		{
			ListItem item = _listData.items[i];
			ListItemOwner owner = item.owner;			
			if(_currentOwnerIndex == 0)
			{	
				itemStyle.normal.textColor = owner.color;
				if(item.isComplete == false)
				{
					displayCount++;
					EditorGUILayout.BeginHorizontal();
					if(EditorGUILayout.Toggle(item.isComplete, GUILayout.Width(20)) == true)
					{
						_listData.items[i].isComplete = true;
					}
					_listData.items[i].task = EditorGUILayout.TextField(item.task, itemStyle);
					int newOwnerIndex = EditorGUILayout.Popup(owner.index, ownersToSelect,GUILayout.Width(60));
					if(newOwnerIndex != owner.index)
					{
						item.owner = _listData.owners[newOwnerIndex];
						_listData.items[i] = item;
					}											
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();
				}
			}else{
				int adjustedIndex = _currentOwnerIndex - 1;
				owner = _listData.owners[adjustedIndex];
				if(owner.name == item.owner.name)
				{
					itemStyle.normal.textColor = owner.color;
					if(item.isComplete == false)
					{
						displayCount++;
						EditorGUILayout.BeginHorizontal();
						if(EditorGUILayout.Toggle(item.isComplete, GUILayout.Width(20)) == true)
						{
							_listData.items[i].isComplete = true;
						}						
						_listData.items[i].task = EditorGUILayout.TextField(item.task, itemStyle);
						int newOwnerIndex = EditorGUILayout.Popup(adjustedIndex, ownersToSelect,GUILayout.Width(60));
						if(newOwnerIndex != adjustedIndex)
						{
							item.owner = _listData.owners[newOwnerIndex];
							_listData.items[i] = item;
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Space();
					}
				}
			}
		}
		
		if(displayCount == 0)
        {
			EditorGUILayout.LabelField("No tasks currently", EditorStyles.largeLabel);
        }
		
		if((showCompletedTasks) && (_currentOwnerIndex == 0))
		{
			itemStyle.normal.textColor = Color.grey;
			for( int i = 0; i < _listData.items.Count; i++)
			{
				if(_listData.items[i].isComplete == true)
				{
					ListItem item = _listData.items[i];
					EditorGUILayout.BeginHorizontal();
					if(EditorGUILayout.Toggle(item.isComplete, GUILayout.Width(20)) == false)
					{
						_listData.items[i].isComplete = false;
					}
					EditorGUILayout.LabelField(item.task, itemStyle);
					if(GUILayout.Button("x",GUILayout.Width(23)))
					{
						_listData.items.RemoveAt(i);
					}	
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Space();
				}					
			}
		}	
		
		EditorGUILayout.EndScrollView();

		// display our task creation area
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Create Task:", EditorStyles.boldLabel);
		_newTaskOwnerIndex  = EditorGUILayout.Popup(_newTaskOwnerIndex, ownersToSelect,GUILayout.Width(60));
		EditorGUILayout.EndHorizontal();
		_newTask = EditorGUILayout.TextField(_newTask, GUILayout.Height(40));
		if( ( GUILayout.Button("Create Task") && _newTask != "" ) )
		{
			// create new task
			ListItemOwner newOwner = _listData.owners[_newTaskOwnerIndex];
			_listData.AddTask(newOwner, _newTask);			
			//EditorUtility.DisplayDialog("Task created for " + newOwner.name, _newTask, "Sweet");
			_newTask = "";
			GUI.FocusControl(null);				
		}
		
		if(GUI.changed)
		{
			//Debug.Log("Save Data: " + _listData.items.Count);
			EditorUtility.SetDirty(_listData);
			AssetDatabase.SaveAssets();
			AssetDatabase.SaveAssets();		
		}	
	}
	
	void OnDestroy()
	{
		EditorUtility.SetDirty(_listData);
		AssetDatabase.SaveAssets();
		AssetDatabase.SaveAssets();
	}	
}
