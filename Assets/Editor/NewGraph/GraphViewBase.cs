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
/// 节点工具集主视图窗口
/// </summary>
public class GraphViewBase : GraphView
{
    public StartNode startNode;
    public GraphViewBase() : base()
    {
        // 让GraphView铺满整个Editor窗口
        this.StretchToParentSize();
        // 开启Graph缩放
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        // 添加拖动Content功能
        this.AddManipulator(new ContentDragger());
        // 添加拖拽选中功能
        this.AddManipulator(new SelectionDragger());
        // 添加框选功能
        this.AddManipulator(new RectangleSelector());

        // 添加背景
        Insert(0, new GridBackground());

        // 生成StartNode
        startNode = new StartNode();
        AddElement(startNode);

        // 创建搜索树 初始化
        var searchWindowProvider = ScriptableObject.CreateInstance<SampleSearchWindowProvider>();
        searchWindowProvider.Initialize(this);

        // 单击右键 | 单击空格 “CreateNode” 触发回调
        nodeCreationRequest += c =>
        {
            // 打开搜索树窗口
            SearchWindow.Open(new SearchWindowContext(c.screenMousePosition), searchWindowProvider);
        };
    }


    /// <summary>
    /// 连接节点
    /// </summary>
    /// <param name="sourceNode"></param>
    /// <param name="targetNode"></param>
    public void ConnectNodes(RootNode sourceNode, RootNode targetNode)
    {
        Edge edge = new Edge
        {
            output = sourceNode.outputContainer[0] as Port,
            input = targetNode.inputContainer[0] as Port
        };
        AddElement(edge);
    }


    // 重写GetCompatiblePorts并返回正确的端口 （返回的端口相当于可以相互连接）
    // 无法连接到同一节点
    // 从输入到输入，从输出到输出，不连接
    // 端口上配置的类型不匹配，无法连接
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        foreach (var port in ports.ToList())
        {
            if (startAnchor.node == port.node ||
                startAnchor.direction == port.direction ||
                startAnchor.portType != port.portType)
            {
                continue;
            }

            compatiblePorts.Add(port);
        }
        return compatiblePorts;
    }

    // 执行
    public void Execute()
    {
        Edge rootEdge = startNode.OutputPort.connections.FirstOrDefault();
        if (rootEdge == null)
        {
            Debug.Log("StartNode未连接任何节点");
            return;
        }


        RootNode currentNode = rootEdge.input.node as RootNode;

        while (true)
        {
            currentNode.Execute();

            var edge = currentNode.OutputPort.connections.FirstOrDefault();
            if (edge == null)
            {
                Debug.Log($"{currentNode.title} -- END");
                break;
            }
            currentNode = edge.input.node as RootNode;
        }
    }
}