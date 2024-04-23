#if ODIN_INSPECTOR
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.IO;
using System.Collections.Generic;

public class TextProcessingTool : OdinEditorWindow
{
    [MenuItem("Tools/ProjectTools/TextProcessingTool", priority = -999999)]
    public static void Open()
    {
        var win = GetWindow<TextProcessingTool>();
    }


    [FolderPath, LabelText("根目录")]
    public string rootDirectory = @"C:\Users\Drenayo\Desktop\Drenayo\";
    [LabelText("Suffix列表")]
    public string[] fileExtensions = { ".cs", ".txt", ".asset", ".prefab", ".unity", ".anim", ".controller" };

    private List<string> createdFiles = new List<string>();

    [Button("处理Suffix", ButtonSizes.Large)]
    public void Btn()
    {

        ProcessFiles(rootDirectory, fileExtensions);
        SaveCreatedFilesToIni();
        Debug.Log("操作完成！");
    }

    // 处理后缀
    void ProcessFiles(string directory, string[] fileExtensions)
    {
        try
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Array.Exists(fileExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
                {
                    string relativePath = file.Replace(rootDirectory, string.Empty).TrimStart(Path.DirectorySeparatorChar);
                    string newFileName = Path.Combine(directory, Path.GetFileNameWithoutExtension(file));

                    using (StreamReader reader = new StreamReader(file))
                    using (StreamWriter writer = new StreamWriter(newFileName))
                    {
                        string content = reader.ReadToEnd();
                        writer.Write(content);
                    }

                    // Debug.Log($"文件 {file} 处理完成，已创建新文件 {newFileName}");

                    // 记录相对路径和对应的后缀
                    string fileExtension = Path.GetExtension(file);
                    createdFiles.Add(relativePath);

                    // 删除原始文件
                    File.Delete(file);
                    // Debug.Log($"原文件 {file} 已删除");
                }
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                ProcessFiles(subdirectory, fileExtensions);
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"发生错误：{ex.Message}");
        }
    }

    // 创建配置文件
    void SaveCreatedFilesToIni()
    {
        // 获取文件路径
        string filePath = Path.Combine(rootDirectory, "TextProcessing.ini");

        try
        {
            // 确保目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // 创建文件并将 List<string> 写入文件
            File.WriteAllLines(filePath, createdFiles);
            // Debug.Log($"已保存创建的文件信息到 {filePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存文件时发生错误：{ex.Message}");
        }
    }


    // 新增的部分
    [Button("恢复Suffix", ButtonSizes.Large)]
    public void Btn_U()
    {
        //UpdateExtensionsFromIni($"{rootDirectory}\\TextProcessing.ini", rootDirectory);
        ApplyIniSettingsToFiles(rootDirectory + "\\TextProcessing.ini", rootDirectory);
    }

    void ApplyIniSettingsToFiles(string iniFilePath, string filesFolderPath)
    {
        try
        {
            // 读取INI文件内容
            Dictionary<string, string> iniSettings = ReadIniFile(iniFilePath);

            // 遍历文件夹中的文件
            foreach (string filePath in Directory.GetFiles(filesFolderPath, "*.", SearchOption.AllDirectories))
            {
                // 获取文件名（不包含后缀）
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                // 如果INI文件中存在对应路径和文件名，则修改文件后缀
                if (iniSettings.ContainsKey(fileNameWithoutExtension))
                {
                    string newExtension = iniSettings[fileNameWithoutExtension];
                    string newFilePath = Path.ChangeExtension(filePath, newExtension);

                    // 修改文件后缀
                    File.Move(filePath, newFilePath);

                    //  Log.Debug($"文件 {filePath} 的后缀已修改为 {newExtension}");
                }
            }

            Debug.Log("处理完成。");
        }
        catch (Exception ex)
        {
            Debug.Log($"发生错误：{ex.Message}");
        }
    }

    Dictionary<string, string> ReadIniFile(string iniFilePath)
    {
        Dictionary<string, string> iniSettings = new Dictionary<string, string>();

        try
        {
            // 读取INI文件的每一行
            string[] lines = File.ReadAllLines(iniFilePath);

            foreach (string line in lines)
            {
                // 分割路径和文件后缀
                string[] parts = line.Split('\\');
                if (parts.Length >= 2)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(parts[parts.Length - 1]);
                    string fileExtension = Path.GetExtension(parts[parts.Length - 1]);

                    // 将路径和文件后缀添加到字典中
                    iniSettings[fileNameWithoutExtension] = fileExtension;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"读取INI文件时发生错误：{ex.Message}");
        }

        return iniSettings;
    }
}
#endif