using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace D007
{
    public class AreaSelection : MonoBehaviour
    {
        public BoxCollider box;// 用于显示选中区域的Box Collider
        public LineRenderer line;
        [Header("射线检测层级")]
        public LayerMask layerMask;
        [Header("矩形区域在Z轴深度")]
        public int deepZ;
        [Header("主相机")]
        public Camera cam;
        [Header("显示列表Text组件")]
        public Text text;




        private Collider[] array;// 存储选中区域内的所有碰撞体
        private List<GameObject> list = new List<GameObject>();// 存储选中区域内的所有游戏对象
        private Vector3 leftUpPoint;
        private Vector3 leftDownPoint;
        private Vector3 rightUpPoint;
        private Vector3 rightDownPoint;
        private Vector3 beginPoint = Vector3.zero;

        void Update()
        {
            DrawRecangle();// 绘制矩形区域边框
            SelectCheck();// 检查并选择矩形区域内的对象


            // 将每个选中对象的名称显示出来
            string outStr = string.Empty;
            foreach (GameObject i in list)
            {
                outStr += i.gameObject.name + "\n";
            }
            text.text = outStr;
            outStr = string.Empty;
        }

        public void SelectCheck()
        {
            RaycastHit hit = new RaycastHit();

            // 鼠标按下时
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask))
                {
                    beginPoint = hit.point;
                }
            }

            // 鼠标抬起时
            if (Input.GetMouseButtonUp(0))
            {
                list.Clear();

                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask))
                {
                    Debug.Log($"开始点：{beginPoint}  结束点{hit.point}");
                    // 计算选中区域的中心点
                    Vector3 center = new Vector3((beginPoint.x + hit.point.x) / 2, 1, (beginPoint.z + hit.point.z) / 2);
                    // 计算选中区域的半尺寸
                    Vector3 half = new Vector3(Mathf.Abs(hit.point.x - beginPoint.x) / 2, 1, Mathf.Abs(hit.point.z - beginPoint.z) / 2);
                    // 获取选中区域内的所有碰撞体
                    array = Physics.OverlapBox(center, half);

                    // 更新Box Collider的中心点
                    box.center = center;
                    // 更新Box Collider的尺寸
                    box.size = new Vector3(Mathf.Abs(hit.point.x - beginPoint.x), 1, Mathf.Abs(hit.point.z - beginPoint.z));

                    // 将选中区域内的碰撞体所附加的游戏对象添加到列表中
                    for (int i = 0; i < array.Length; i++)
                    {
                        // 排除地面
                        if (array[i].gameObject.name != "Plane")
                            list.Add(array[i].gameObject);
                    }
                }
            }
        }

        public void DrawRecangle()
        {
            // 鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                leftUpPoint = Input.mousePosition;
            }

            // 鼠标持续按下
            else if (Input.GetMouseButton(0))
            {
                rightDownPoint = Input.mousePosition;
                leftUpPoint.z = deepZ;
                rightDownPoint.z = deepZ;

                rightUpPoint = new Vector3(rightDownPoint.x, leftUpPoint.y, deepZ);
                leftDownPoint = new Vector3(leftUpPoint.x, rightDownPoint.y, deepZ);

                line.positionCount = 4;
                line.SetPosition(0, cam.ScreenToWorldPoint(leftUpPoint));
                line.SetPosition(1, cam.ScreenToWorldPoint(rightUpPoint));
                line.SetPosition(2, cam.ScreenToWorldPoint(rightDownPoint));
                line.SetPosition(3, cam.ScreenToWorldPoint(leftDownPoint));
            }

            // 鼠标抬起
            else if (Input.GetMouseButtonUp(0))
            {
                line.positionCount = 0;
            }
        }

        private void OnDrawGizmos()
        {
            // 绘制Box Collider的Gizmos
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(box.center, box.size);
        }
    }
}
