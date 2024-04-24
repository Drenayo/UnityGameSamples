using System.Collections;
using UnityEngine;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;

// 根节点
public abstract class RootNode : Node
{
    /// <summary>
    /// 节点的输入端口——单连接
    /// </summary>
    public Port InputPort;
    /// <summary>
    /// 节点的输出端口——单连接
    /// </summary>
    public Port OutputPort;

    public string ID;

    public RootNode()
    {
        InputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        InputPort.portName = "In";
        inputContainer.Add(InputPort);

        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        OutputPort.portName = "Out";
        outputContainer.Add(OutputPort);

        ID = Guid.NewGuid().ToString();

        //// 创建用于显示 id 的文本元素
        //Label idLabel = new Label(id);
        //idLabel.style.color = Color.white; // 设置文本颜色
        //idLabel.style.position = Position.Absolute; // 设置绝对位置
        //idLabel.style.unityTextAlign = TextAnchor.LowerCenter; // 设置文本对齐方式
        //idLabel.style.top = 0; // 设置距离上边缘的距离
        //idLabel.style.left = 0; // 设置距离左边缘的距离
        //idLabel.style.right = 0; // 设置距离右边缘的距离
        //idLabel.style.bottom = 0; // 设置距离下边缘的距离
        //idLabel.style.fontSize = 10; // 设置文本字体大小

        //// 将文本元素添加到节点的 visualContainer 中
        //titleContainer.Add(idLabel);
    }



    /// <summary>
    /// 获取前一个节点
    /// </summary>
    public virtual Node GetPreviousNode()
    {
        return InputPort.connections.FirstOrDefault().output.node;
    }

    /// <summary>
    /// 获取后一个节点
    /// </summary>
    public virtual Node GetNextNode()
    {
        Node node = OutputPort?.connections?.FirstOrDefault()?.input?.node;
        if(node != null)
            return node;
        return null;
    }

    /// <summary>
    /// 执行节点
    /// </summary>
    public virtual void Execute()
    {
        Debug.Log($"{title}-->{GetNextNode()?.title}");
    }
}
