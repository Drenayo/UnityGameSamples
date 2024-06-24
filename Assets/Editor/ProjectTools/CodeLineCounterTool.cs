using UnityEngine;
using UnityEditor;
using System.IO;

public class CodeLineCounterTool : EditorWindow
{
    private string folderPath;
    private int totalLines;

    [MenuItem("Tools/ProjectTools/代码行数统计工具")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CodeLineCounterTool));
    }

    private void OnGUI()
    {
        GUILayout.Label("Code Line Counter", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        folderPath = EditorGUILayout.TextField("Folder Path:", folderPath);

        if (GUILayout.Button("Count Lines"))
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                Debug.LogError("Folder path is invalid or empty.");
                return;
            }

            CountLines(folderPath);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Total Lines of Code:", totalLines.ToString());
    }

    private void CountLines(string path)
    {
        totalLines = 0;

        string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string[] lines = File.ReadAllLines(file);
            totalLines += lines.Length;
        }
    }
}