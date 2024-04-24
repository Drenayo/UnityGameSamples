using System.Collections;
using UnityEngine;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;

//// String节点
//public class StringNode : ProcessNode
//{
//    private TextField textField;
//    public Port inputString;
//    public string Text
//    {
//        get
//        {
//            return textField.value;
//        }
//        set
//        {
//        }
//    }

//    public StringNode() : base()
//    {
//        title = "String";


//        inputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
//        inputContainer.Add(inputString);

//        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(string));
//        outputContainer.Add(outputPort);

//        textField = new TextField();
//        mainContainer.Add(textField);
//    }

//    public override void Execute()
//    {
//        var edge = inputString.connections.FirstOrDefault();
//        var node = edge.output.node as TextProcessNode;

//       // Text = node.outputText;
//    }
//}


// 使用搜索窗口，可以轻松地创建允许选择节点的 UI。
// 在这个示例中，我们首先创建一个搜索树条目组，然后遍历当前应用程序域中的所有程序集和类型。对于每个类型，我们检查它是否是一个类、非抽象类、是 `SampleNode` 的子类且不是 `SampleNode` 类本身。如果类型符合这些条件，我们将创建一个搜索树条目，并将其添加到搜索树中。最后，返回包含所有搜索树条目的列表。

public class SampleSearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private GraphViewBase graphView;

    public void Initialize(GraphViewBase graphView)
    {
        this.graphView = graphView;
    }

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                // 是类 | 不是抽象类 | 是RootNode子类
                if (type.IsClass && !type.IsAbstract && (type.IsSubclassOf(typeof(RootNode)))
                   && type != typeof(StartNode))

                {
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
                }
            }
        }
        return entries;
    }



    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as System.Type;
        var node = Activator.CreateInstance(type) as RootNode;

        // 在搜索树窗口被选中的Node，会被添加到GraphView窗口中
        graphView.AddElement(node);
        return true;
    }
}