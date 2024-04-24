using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "GraphView/ScriptGraphAsset")]
public class ScriptGraphAsset : ScriptableObject
{
	public List<NodeData> nodesData = new List<NodeData>();
	public List<NodeLinkData> nodeLinksData = new List<NodeLinkData>();
}

/// <summary>
/// 节点数据
/// </summary>
[Serializable]
public class NodeData
{
	public string nodeName;
	public string nodeID;
	public string nodeType;
	public Vector2 nodePosition;
}

/// <summary>
/// 连接线数据
/// </summary>
[Serializable]
public class NodeLinkData
{
	public string preNodeGuid;
	public string portName;
	public string nextNodeGuid;
}
