using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphViewTools : EditorWindow
{
    private GraphViewBase graphViewBase;
    private GraphSaveUtility graphSaveUtility;
    
    [MenuItem("Tools/Open GraphView")]
    public static void Open()
    {
        var window = GetWindow<GraphViewTools>();
    }

    private void OnEnable()
    {
        graphViewBase = new GraphViewBase()
        {
            style = { flexGrow = 1 }
        };
        graphSaveUtility = GraphSaveUtility.GetInstance(graphViewBase);


        rootVisualElement.Add(graphViewBase);

        // 运行图谱
        Toolbar toolbar = new Toolbar();

        toolbar.Add(new Button(graphSaveUtility.SaveData) { text = "保存" });
        toolbar.Add(new Button(graphSaveUtility.LoadData) { text = "加载" });
        toolbar.Add(new Button(graphViewBase.Execute) { text = "执行" });
        //toolbar.Add(new Button(()=> 
        //{
        //    graphViewBase.DeleteElements(graphViewBase.nodes.ToList());
        //}) { text = "清空" });

        rootVisualElement.Add(toolbar);

        Show();
    }
}


public class GraphSaveUtility
{
    private GraphViewBase graphViewBase;

    // 每次从类里获取edges和nodes时，都会去取graphView里的内容
    private List<Edge> edges => graphViewBase.edges.ToList();
    private List<RootNode> nodes => graphViewBase.nodes.ToList().Cast<RootNode>().ToList();

    public static GraphSaveUtility GetInstance(GraphViewBase graphView)
    {
        return new GraphSaveUtility
        {
            graphViewBase = graphView
        };
    }

    /// <summary>
    /// 保存图谱数据
    /// </summary>
    public void SaveData()
    {
        if (!edges.Any())
            return;

        // 创建一个预制体
        ScriptGraphAsset containerAsset = ScriptableObject.CreateInstance<ScriptGraphAsset>();

        for (int i = 0; i < edges.Count; i++)
        {
            Edge e = edges[i];
            RootNode inputNode = e.input.node as RootNode;
            RootNode outputNode = e.output.node as RootNode;

            containerAsset.nodeLinksData.Add(new NodeLinkData()
            {
                nextNodeGuid = inputNode.ID,
                preNodeGuid = outputNode.ID,
                portName = e.output.portName,
            });
        }

        // 获取所有不为Entry的Node, 这样的Node既有input，也有output
        RootNode[] regularNodes = nodes.ToArray();
        for (int i = 0; i < regularNodes.Length; i++)
        {
            RootNode n = regularNodes[i];

            containerAsset.nodesData.Add(new NodeData()
            {
                nodeID = n.ID,
                nodeName = n.title, 
                nodeType = n.GetType().ToString(),
                nodePosition = n.GetPosition().position
            });
        }


        SaveAsset(containerAsset, $"Assets/Resources/GraphViewData.asset");
        //AssetDatabase.CreateAsset(containerAsset, $"Assets/Resources/GraphViewData.asset");
        //AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// 加载图谱数据
    /// </summary>
    public void LoadData()
    {
        // 清空图谱
        ClearNode();
        // 读取数据
        ScriptGraphAsset scriptGraphAsset = AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>("Assets/Resources/GraphViewData.asset");
        // 读取节点数据
        foreach (var node in scriptGraphAsset.nodesData)
        {
            // 根据节点类型创建对应节点
            System.Type nodeType = System.Type.GetType(node.nodeType);

            RootNode rootNode = (RootNode)System.Activator.CreateInstance(nodeType);
            
            rootNode.ID = node.nodeID;
            rootNode.title = node.nodeName;
            rootNode.SetPosition(new Rect(node.nodePosition, Vector2.zero));
            
            
            // 添加到图谱中
            graphViewBase.AddElement(rootNode);
        }

        // 读取连线数据
        foreach (var link in scriptGraphAsset.nodeLinksData)
        {
            graphViewBase.ConnectNodes(FindNodeByID(link.preNodeGuid), FindNodeByID(link.nextNodeGuid));
        }

        
    }

    /// <summary>
    /// 根据ID查找节点
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private RootNode FindNodeByID(string id)
    {
        return graphViewBase.nodes.ToList().Cast<RootNode>().Where(node => node.ID == id).ToList().FirstOrDefault();
    }

    /// <summary>
    /// 清空图谱
    /// </summary>
    public void ClearNode()
    {
        graphViewBase.DeleteElements(graphViewBase.nodes.ToList());
        graphViewBase.DeleteElements(graphViewBase.edges.ToList());
    }

    
    public void SaveAsset(Object asset, string path)
    {
        // 检查指定路径是否存在现有文件
        if (AssetDatabase.LoadAssetAtPath<Object>(path) != null)
        {
            // 存在现有文件，删除它
            AssetDatabase.DeleteAsset(path);
        }

        // 创建新的 Asset 并保存到指定路径
        AssetDatabase.CreateAsset(asset, path);

        // 保存 Asset 数据库
        AssetDatabase.SaveAssets();
    }
}