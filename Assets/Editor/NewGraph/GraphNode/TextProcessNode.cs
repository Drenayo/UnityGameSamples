using System.Collections;
using UnityEngine;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;

/// <summary>
/// 文本处理节点
/// </summary>
public class TextProcessNode : RootNode
{
    //public Port inputString;
    //public Port outputString;

    public TextProcessNode() : base()
    {
        title = "TextProcessNode";

        //inputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
        //inputContainer.Add(inputString);

        //outputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(string));
        //outputContainer.Add(outputString);
    }

    public override void Execute()
    {
        base.Execute();

        //// 这是的操作是：拿到input所连接的第一条线edge，然后通过前向edge拿到对应Node,然后把对应node as成stringnode,进而拿到文本。
        //var edge = inputString.connections.FirstOrDefault();
        //var node = edge.output.node as StringNode;

        //if (node == null)
        //    return;

        //outputText = ConvertToLowerCase(outputText);


    }

    /// <summary>
    /// 将输入的字符串转换为小写形式
    /// </summary>
    public string ConvertToLowerCase(string input)
    {
        // 使用ToLower()方法将字符串转换为小写形式
        return input.ToLower();
    }
}
