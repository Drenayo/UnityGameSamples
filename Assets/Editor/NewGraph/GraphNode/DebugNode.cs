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
/// 调试节点
/// </summary>
public class DebugNode : RootNode
{
    public Port inputString;

    public DebugNode() : base()
    {
        title = "DebugNode";

        inputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
        inputContainer.Add(inputString);
    }

    public override void Execute()
    {

        base.Execute();

        //var edge = inputString.connections.FirstOrDefault();
        //var node = edge.output.node as StringNode;

        //if (node == null)
        //    return;

        //Debug.Log(node.Text);
    }
}