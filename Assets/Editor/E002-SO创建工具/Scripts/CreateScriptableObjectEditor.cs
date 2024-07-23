#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace E002
{
    public class CreateScriptableObjectEditor : OdinEditorWindow
    {
        [LabelText("SO名字")]
        public string soName = "NewScriptableObject";
        [FolderPath]
        [LabelText("生成地址")]
        public string soLocation = "Assets/";
        [LabelText("SOClass类型")]
        public string typeStr;
        private Type sotype;

        [MenuItem("Tools/Editor/E002-SO创建工具",priority = 2)]
        private static void OpenWindow()
        {
            var w = GetWindow<CreateScriptableObjectEditor>();
            w.Show();
            w.titleContent = new GUIContent("SO创建工具");

        }

        [Button("创建", ButtonSizes.Medium)]
        public void Btn()
        {
            CreateScriptableObject(FindTypeByName(typeStr));
        }

        private void CreateScriptableObject(Type type)
        {
            ScriptableObject so = ScriptableObject.CreateInstance(type);
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(soLocation + "/" + soName + ".asset");
            AssetDatabase.CreateAsset(so, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // 根据类型名查找类型
        public Type FindTypeByName(string typeName)
        {
            // 获取当前应用程序域中所有已加载的程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // 在每个程序集中查找具有指定名称的类型
            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}


#endif
