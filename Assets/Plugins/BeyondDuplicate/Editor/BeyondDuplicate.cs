using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
 
/// <summary>
/// This class manages duplicated objects' name.
/// Supports multiple duplication
/// For now, a lot of workaround had to be made in order to know which object has been duplicated. Might be fixed in a near future?
/// Preferences can be edited in 'Preferences/Beyond Duplicate'
/// </summary>
[InitializeOnLoad]
public class BeyondDuplicate
{
	private const string PASTE_COMMAND = "Paste";
	private const string DUPLICATE_COMMAND = "Duplicate";

	private const string ENABLE_PREFS_KEY = "BeyondDuplicate.IsEnabled";
	private const string INCREMENT_PREFS_KEY = "BeyondDuplicate.AutoIncrementDigit";
	private const string PREFAB_DRAG_PREFS_KEY = "BeyondDuplicate.PrefabDrag";

	public static System.Action<GameObject> OnGameObjectDuplicated;

	private static int m_PreviousObjectCount;
	private static string m_LastCommandName = "";
	private static bool m_PrefabDraggedInScene;
	
	private static readonly List<int> m_ExistingIDs = new List<int>();
	
	#region Duplicate
	static BeyondDuplicate()
	{
		// Prevents event being hooked more than once
		EditorApplication.hierarchyWindowItemOnGUI -= OnItemOnGUI;
		EditorApplication.hierarchyWindowItemOnGUI += OnItemOnGUI;

		// Prevents event being hooked more than once
		#if UNITY_2017_OR_NEWER
		EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		#else
		EditorApplication.playmodeStateChanged -= OnPlayModeStateChanged;
		EditorApplication.playmodeStateChanged += OnPlayModeStateChanged;
		#endif
	}

	private static void OnItemOnGUI(int i_InstanceID, Rect i_SelectionRect)
	{
		bool isCurrentCommandValid = Event.current.commandName == PASTE_COMMAND || Event.current.commandName == DUPLICATE_COMMAND;
		if (Event.current.type == EventType.DragPerform || (Event.current.type == EventType.ExecuteCommand && isCurrentCommandValid))
		{
			if (Event.current.type == EventType.DragPerform)
			{
				if(EditorPrefs.GetBool(PREFAB_DRAG_PREFS_KEY, true))
				{
					m_PrefabDraggedInScene = true;
				}
				else
				{
					return;
				}
			}
			else
			{
				m_LastCommandName = Event.current.commandName;
			}

			m_ExistingIDs.Clear();
			// Since the duplicated object takes at least one frame before being instantiated, a reference of all the objects' InstanceID currently in the scene must be taken
			// Will be necessary once the duplicated object(s) has/have been instantiated in the scene 
			GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
			m_PreviousObjectCount = allObjects.Length;

			for (int i = 0; i < allObjects.Length; i++)
			{
				m_ExistingIDs.Add(allObjects[i].GetInstanceID());
			}
			
		}
		else if (m_PreviousObjectCount > 0)
		{
			// Then, a check needs to be made for all the current objects in the scene and see if the previous objects count isn't equal to the current objects count
			// If there is a difference, that means the new duplicated object(s) is/are now really instantiated. This step should take only 1 frame
			GameObject[] currentObjects = GameObject.FindObjectsOfType<GameObject>();
			if (m_PreviousObjectCount != currentObjects.Length)
			{
				m_PreviousObjectCount = 0;
				bool isLastCommandValid = EditorPrefs.GetBool(ENABLE_PREFS_KEY, true) && (m_LastCommandName == PASTE_COMMAND || m_LastCommandName == DUPLICATE_COMMAND || m_PrefabDraggedInScene);
				m_PrefabDraggedInScene = false;
				for (int i = 0; i < currentObjects.Length; i++)
				{
					if (!m_ExistingIDs.Contains(currentObjects[i].GetInstanceID()))
					{
						if (isLastCommandValid)
						{
							string regexValue = null;
							// Prevents an infinite loop. Shouldn't ever happen though
							int failSafeCount = 10;
							do
							{
								// Check if it ends with (Digits). If so, removes it 
								regexValue = Regex.Match(currentObjects[i].name, "\\([^\\d]*(\\d+)[^\\d]*\\).*$").Value;
								if (!string.IsNullOrEmpty(regexValue) && regexValue.EndsWith(")"))
								{
									currentObjects[i].name = currentObjects[i].name.Replace(regexValue, "").TrimEnd();
								}

								failSafeCount--;
							}
							while (!string.IsNullOrEmpty(regexValue) && failSafeCount > 0);
							
							// Increment name's last digit if it is ending with a digit
							if (EditorPrefs.GetBool(INCREMENT_PREFS_KEY, true))
							{
								regexValue = Regex.Match(currentObjects[i].name, "\\d+$").Value;
								if (!string.IsNullOrEmpty(regexValue))
								{
									int digit;
									if (int.TryParse(regexValue, out digit))
									{
										digit++;
										string digitString = digit.ToString();
										int replaceIndex = currentObjects[i].name.Length - digitString.Length;
										currentObjects[i].name = currentObjects[i].name.Remove(replaceIndex).Insert(replaceIndex, digitString);
									}
								}
							}
						}
						
						if (OnGameObjectDuplicated != null)
						{
							OnGameObjectDuplicated(currentObjects[i]);
						}
					}
				}
			}
			else
			{
				ResetAll();
			}
			
		}
	}
	#endregion
	
	#region Misc
	#if UNITY_2017_OR_NEWER
	private static void OnPlayModeStateChanged(PlayModeStateChange i_PlayModeState)
	#else
	private static void OnPlayModeStateChanged()
	#endif
	{
		ResetAll();
	}

	private static void ResetAll()
	{
		m_PreviousObjectCount = 0;
		m_ExistingIDs.Clear();
		m_LastCommandName = "";
	}
	#endregion
	
	#region Preferences
	[PreferenceItem("B. Duplicate")]
	private static void DuplicatePreferencesGUI()
	{
		bool isEnabled = EditorPrefs.GetBool(ENABLE_PREFS_KEY, true);

		EditorGUI.BeginChangeCheck();
		isEnabled = EditorGUILayout.Toggle(new GUIContent("Enable Feature?", "Enabled when copy/pasting (Ctrl+C, Ctrl+V) or duplicating (Ctrl+D) a GameObject(s) in a scene"), isEnabled);
		if (EditorGUI.EndChangeCheck())
		{
			EditorPrefs.SetBool(ENABLE_PREFS_KEY, isEnabled);
		}

		if (isEnabled)
		{
			bool incrementDigitLast = EditorPrefs.GetBool(INCREMENT_PREFS_KEY, true);

			EditorGUI.BeginChangeCheck();
			incrementDigitLast = EditorGUILayout.Toggle(new GUIContent("Auto-Increment Digit?", "If the duplicated object's name ends with a digit, this digit will automatically be incremented. Example: Object01 => Object02"), incrementDigitLast);
			if (EditorGUI.EndChangeCheck())
			{
				EditorPrefs.SetBool(INCREMENT_PREFS_KEY, incrementDigitLast);
			}
			
			bool prefabDrag = EditorPrefs.GetBool(PREFAB_DRAG_PREFS_KEY, true);

			EditorGUI.BeginChangeCheck();
			prefabDrag = EditorGUILayout.Toggle(new GUIContent("Enable with Prefabs?", "If enabled, the feature will also support when a prefab is dragged and dropped in the scene"), prefabDrag);
			if (EditorGUI.EndChangeCheck())
			{
				EditorPrefs.SetBool(PREFAB_DRAG_PREFS_KEY, prefabDrag);
			}
		}
	}
	#endregion
}