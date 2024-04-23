using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class ScriptSplitter : EditorWindow
{
    private string inputText = "";

    [MenuItem("Tools/Script Splitter")]
    static void Init()
    {
        ScriptSplitter window = (ScriptSplitter)EditorWindow.GetWindow(typeof(ScriptSplitter));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Paste your script code below:");
        inputText = EditorGUILayout.TextArea(inputText, GUILayout.Height(200));

        if (GUILayout.Button("Split and Save"))
        {
            SplitAndSaveScripts(inputText);
        }
    }

    private void SplitAndSaveScripts(string scriptText)
    {
        // 使用正则表达式匹配类和枚举
        Regex classRegex = new Regex(@"(public|private|protected|internal)?\s*class\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*[\s\S]*?(?=\bclass\b|\benum\b|$)");
        Regex enumRegex = new Regex(@"(public|private|protected|internal)?\s*enum\s+([a-zA-Z_][a-zA-Z0-9_]*)\s*[\s\S]*?(?=\bclass\b|\benum\b|$)");

        // 匹配类
        MatchCollection classMatches = classRegex.Matches(scriptText);
        foreach (Match match in classMatches)
        {
            int startIndex = match.Index;
            int endIndex = FindClosingBraceIndex(scriptText, startIndex);
            if (endIndex != -1)
            {
                string className = match.Groups[2].Value;
                string fileContent = AddNamespaceAndUsings(scriptText.Substring(startIndex, endIndex - startIndex + 1));
                SaveScript(className + ".cs", fileContent);
            }
        }

        // 匹配枚举
        MatchCollection enumMatches = enumRegex.Matches(scriptText);
        foreach (Match match in enumMatches)
        {
            int startIndex = match.Index;
            int endIndex = FindClosingBraceIndex(scriptText, startIndex);
            if (endIndex != -1)
            {
                string enumName = match.Groups[2].Value;
                string fileContent = AddNamespaceAndUsings(scriptText.Substring(startIndex, endIndex - startIndex + 1));
                SaveScript(enumName + ".cs", fileContent);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Scripts saved successfully!");
    }

    private int FindClosingBraceIndex(string text, int startIndex)
    {
        int braceCount = 0;
        for (int i = startIndex; i < text.Length; i++)
        {
            if (text[i] == '{')
            {
                braceCount++;
            }
            else if (text[i] == '}')
            {
                braceCount--;
                if (braceCount == 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private string AddNamespaceAndUsings(string content)
    {
        string usingStatements = "using UnityEngine;\nusing System;\nusing System.Collections;\nusing System.Collections.Generic;\n\n";
        string namespaceStatement = "namespace DemoTemp\n{\n";

        return usingStatements + namespaceStatement + content + "\n}\n";
    }

    private void SaveScript(string fileName, string content)
    {
        string filePath = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(filePath, content);
    }
}
