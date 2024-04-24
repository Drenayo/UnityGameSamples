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
/// 开始节点
/// </summary>
public class StartNode : RootNode
{
    public StartNode() : base()
    {
        title = "StartNode";
        // 设置为红色，表面上禁止连接
        InputPort.portColor = Color.red;
    }
}
