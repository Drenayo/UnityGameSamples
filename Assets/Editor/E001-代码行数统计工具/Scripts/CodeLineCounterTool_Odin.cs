#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace E001
{
    public class CodeLineCounter : OdinEditorWindow
    {
        [FolderPath(AbsolutePath = true)]
        [OnValueChanged("OnValueChanged")]
        [LabelText("代码文件夹路径")]
        public string folderPath;
        private int totalLines;

        [MenuItem("Tools/Editor/E001-代码行数统计工具", priority = 1)]
        public static void ShowWindow()
        {
            GetWindow<CodeLineCounter>().Show();
            GetWindow<CodeLineCounter>().titleContent = new GUIContent("代码行数统计工具");
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

        protected override void OnGUI()
        {
            base.OnGUI();
            EditorGUILayout.Space();
            GUIStyle style = new GUIStyle(EditorStyles.label)
            {
                fontSize = 25,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField(totalLines.ToString(), style);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
    }
}
#endif
