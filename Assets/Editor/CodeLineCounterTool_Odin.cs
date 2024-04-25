using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using System.IO;
using Sirenix.OdinInspector;

public class CodeLineCounter : OdinEditorWindow
{
    [FolderPath(AbsolutePath = true)]
    [OnValueChanged("OnValueChanged")]
    public string folderPath;

    public int totalLines;

    [MenuItem("Tools/代码统计工具_Odin")]
    public static void ShowWindow()
    {
        GetWindow<CodeLineCounter>().Show();
    }

    public void OnValueChanged()
    {
        totalLines = CountLines(folderPath);
    }

    private int CountLines(string path)
    {
        int totalLines = 0;

        string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string[] lines = File.ReadAllLines(file);
            totalLines += lines.Length;
        }
        return totalLines;
    }
}