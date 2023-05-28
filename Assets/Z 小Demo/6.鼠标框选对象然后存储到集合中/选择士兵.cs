using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 框选士兵有两种解决方案
// 1.选择框贴紧地面
// 2.选择框显示在屏幕上（选择这个）
namespace Z_6
{
    public class 选择士兵 : MonoBehaviour
    {
        public Collider[] array;
        public List<GameObject> list = new List<GameObject>();
        public BoxCollider box;
        public Vector3 leftUpPoint;
        public Vector3 leftDownPoint;
        public Vector3 rightUpPoint;
        public Vector3 rightDownPoint;
        public LineRenderer line;
        public int deepZ;
        public Camera cam;
        public Transform cube;
        public Transform cube2;

        void Update()
        {
            DrawRecangle();
            SelectCheck();
        }

        public void SelectCheck()
        {
            RaycastHit hit = new RaycastHit();
            Vector3 beginPoint = Vector3.zero;

            // 鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    beginPoint = hit.point;
                    cube.position = beginPoint;
                }
            }

            // 鼠标抬起
            if (Input.GetMouseButtonUp(0))
            {
                list.Clear();

                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    cube2.position = hit.point;
                    Vector3 center = new Vector3((beginPoint.x + hit.point.x) / 2, 1, (beginPoint.z + hit.point.z) / 2);
                    Vector3 half = new Vector3(Mathf.Abs(hit.point.x - beginPoint.x) / 2, 1, Mathf.Abs(hit.point.z - beginPoint.z) / 2);
                    array = Physics.OverlapBox(center, half);

                    box.center = center;
                    box.size = new Vector3(Mathf.Abs(hit.point.x - beginPoint.x), 1, Mathf.Abs(hit.point.z - beginPoint.z));

                    for (int i = 0; i < array.Length; i++)
                    {
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
    }
}
