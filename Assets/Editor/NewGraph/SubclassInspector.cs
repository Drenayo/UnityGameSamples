//using System.Collections;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Experimental.GraphView;
//using UnityEngine.UIElements;
//using System.Collections.Generic;
//using System;

//public class SubclassInspector : EditorWindow
//{
//    [MenuItem("GraphTool/Open SubclassInspector")]
//    public static void Open()
//    {
//        var win = GetWindow<SubclassInspector>();
//        win.titleContent = new GUIContent("子类关系查看工具");
//    }

//    public string checkType = "Type";
//    public int depth = 2;
//    public int heightSpacing = 60;
//    public int widthSpacing = 250;

//    /// <summary>
//    /// 维护一个当前层级的子节点数量 层级|子节点数量  根是0层，根子节点是2层
//    /// </summary>
//    private Dictionary<int, int> layerNodeNumDic;

//    public void Execute()
//    {

//        Type type = TypeTool.FindTypeByName(checkType);
//        if (type == null)
//        {
//            Debug.Log("Type为空");
//            return;
//        }
//       // Clera();

//        // 初始化层级的子节点数量
//        layerNodeNumDic = new Dictionary<int, int>();
//        for (int i = 0; i < depth; i++)
//            layerNodeNumDic.Add(i + 1, 0);

//        // 获取继承指定类型的所有子类
//        IEnumerable<Type> derivedTypes = TypeTool.GetDerivedTypes(type);

//        // 拿到状态机
//        AnimatorStateMachine machine = controller.layers[0].stateMachine;

//        // 计算根节点位置
//        Vector3 rootPos = machine.entryPosition + Vector3.right * widthSpacing;

//        // 添加主状态（根节点）
//        AnimatorState mainState = machine.AddState(type.Name, rootPos);

//        // 生成子类节点并添加连线
//        GenerateNodesAndLinks(machine, mainState, rootPos, derivedTypes, depth);
//    }

//    //[Button("清除"), HorizontalGroup("B")]
//    //public void Clera()
//    //{
//    //    // 拿到状态机
//    //    AnimatorStateMachine machine = controller.layers[0].stateMachine;
//    //    foreach (var item in machine.states)
//    //    {
//    //        machine.RemoveState(item.state);
//    //    }
//    //}

//    /// <summary>
//    /// 生成节点并链接
//    /// </summary>
//    /// <param name="machine">状态机</param>
//    /// <param name="parentState">父状态(节点)</param>
//    /// <param name="parentPos">父状态位置</param>
//    /// <param name="derivedTypes">子类型列表</param>
//    /// <param name="remainingDepth">遍历深度</param>
//    private void GenerateNodesAndLinks(AnimatorStateMachine machine, AnimatorState parentState, Vector3 parentPos, IEnumerable<Type> derivedTypes, int remainingDepth)
//    {
//        if (remainingDepth <= 0 || derivedTypes == null)
//            return;

//        // 初始化父节点的位置偏移量  | 纵向Y轴：当前层的子节点数量*高度间距
//        Vector3 parentPositionOffset = parentPos + Vector3.up * heightSpacing * layerNodeNumDic[depth - remainingDepth + 1];

//        // 循环处理每个子类
//        foreach (Type derivedType in derivedTypes)
//        {
//            // 添加子节点状态（节点）  | 位置计算：父节点基准 + 宽度间隔*当前遍历深度
//            AnimatorState state = machine.AddState(derivedType.Name, parentPositionOffset + Vector3.right * widthSpacing);

//            // 添加状态之间的连线关系
//            if (parentState != null)
//            {
//                parentState.AddTransition(state, false);
//            }

//            // 更新下一个子节点的位置
//            parentPositionOffset.y += heightSpacing;

//            // 递归生成子节点的子节点和连线
//            GenerateNodesAndLinks(machine, state, parentPos + Vector3.right * widthSpacing, TypeTool.GetDerivedTypes(derivedType), remainingDepth - 1);
//        }

//        // 更新层级的子节点数量
//        layerNodeNumDic[depth - remainingDepth + 1] += derivedTypes.Count();
//    }

//    private void OnEnable()
//    {
//        var graphView = new SampleGraphView()
//        {
//            style = { flexGrow = 1 }
//        };
//        rootVisualElement.Add(graphView);
//    }
//}

//public class SampleGraphView : GraphView
//{
//    public SampleNode root;

//    public SampleGraphView() : base()
//    {

//        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

//        // 插入一个背景
//        Insert(0, new GridBackground());

//        root = new SampleNode();
//        AddElement(root);


//        this.AddManipulator(new SelectionDragger());

        
//    }

//    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
//    {
//        var compatiblePorts = new List<Port>();
//        foreach (var port in ports.ToList())
//        {
//            if (startAnchor.node == port.node || startAnchor.direction == port.direction || startAnchor.portType != port.portType)
//            {
//                continue;
//            }
//            compatiblePorts.Add(port);
//        }
//        return compatiblePorts;
//    }
//}

//public class SampleNode : Node
//{
//    public SampleNode()
//    {
//        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
//        inputContainer.Add(inputPort);

//        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
//        outputContainer.Add(outputPort);
//    }

//}



//public static class TypeTool
//{
//    // 获取指定类型的继承链
//    public static IEnumerable<Type> GetInheritanceChain(Type type)
//    {
//        // 创建一个栈来保存继承链
//        var inheritanceChain = new Stack<Type>();

//        // 从指定类型开始，一直向上查找父类，直到 Object 类型
//        while (type != null)
//        {
//            inheritanceChain.Push(type);
//            type = type.BaseType;
//        }

//        // 返回继承链
//        return inheritanceChain;
//    }

//    // 获取继承指定类型的所有子类
//    public static IEnumerable<Type> GetDerivedTypes(Type parent)
//    {
//        // 获取当前加载的所有程序集
//        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

//        // 遍历所有程序集中的类型
//        foreach (var assembly in assemblies)
//        {
//            foreach (var type in assembly.GetTypes())
//            {
//                // 检查当前类型是否继承自指定类型
//                if (type.BaseType == parent)
//                {
//                    yield return type;
//                }
//            }
//        }
//    }

//    // 获取继承指定类型的所有子类名称
//    public static IEnumerable<string> GetDerivedTypeNames(Type parent)
//    {
//        // 获取当前加载的所有程序集
//        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

//        // 遍历所有程序集中的类型
//        foreach (var assembly in assemblies)
//        {
//            foreach (var type in assembly.GetTypes())
//            {
//                // 检查当前类型是否继承自指定类型
//                if (type.BaseType == parent)
//                {
//                    yield return type.Name;
//                }
//            }
//        }
//    }

//    // 根据类型名查找类型
//    public static Type FindTypeByName(string typeName)
//    {
//        // 获取当前应用程序域中所有已加载的程序集
//        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

//        // 在每个程序集中查找具有指定名称的类型
//        foreach (var assembly in assemblies)
//        {
//            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
//            if (type != null)
//            {
//                return type;
//            }
//        }
//        return null;
//    }
//}

