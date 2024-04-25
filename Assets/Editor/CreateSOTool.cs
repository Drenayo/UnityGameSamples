//using Sirenix.OdinInspector;
//using Sirenix.OdinInspector.Editor;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class CreateSOTool : OdinEditorWindow
//{
//    [MenuItem("Tools/创建SO工具")]
//    private static void OpenWindow()
//    {
//        GetWindow<CreateSOTool>().Show();
//    }

   
//    public string soName;
//    [FolderPath(AbsolutePath = true)]
//    public string folderPath;
//    [AssetSelector]
//    public ScriptableObject SO;

//    [Button("创建SO",ButtonSizes.Medium)]
//    public void Btn()
//    {
//        ScriptableObject so = ScriptableObject.CreateInstance<ScriptableObject>();
//        AssetDatabase.CreateAsset(so, folderPath);
//        AssetDatabase.SaveAssets();
//    //}
    
//}
