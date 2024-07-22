#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class CreateScriptableObjectEditor : OdinEditorWindow
{
    public string soName = "NewScriptableObject";
    [FolderPath]
    public string soLocation = "Assets/";
    public string typeStr;
    private Type sotype;

    [MenuItem("Tools/ProjectTools/创建SO工具")]
    private static void OpenWindow()
    {
        var w = GetWindow<CreateScriptableObjectEditor>();
        w.Show();
        w.titleContent = new GUIContent("创建SO工具");

    }

    [Button("创建", ButtonSizes.Medium)]
    public void Btn()
    {
        CreateScriptableObject(FindTypeByName(typeStr));
    }

    private void CreateScriptableObject(Type type)
    {
        ScriptableObject so = ScriptableObject.CreateInstance(type);
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(soLocation + soName + ".asset");
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
#endif
