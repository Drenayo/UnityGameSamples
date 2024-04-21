using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace D007
{
    public class AreaSelection : MonoBehaviour
    {
        public Collider[] array;
        public List<GameObject> list = new List<GameObject>();
        public BoxCollider box;
        public Vector3 leftUpPoint;
        public Vector3 leftDownPoint;
        public Vector3 rightUpPoint;
        public Vector3 rightDownPoint;
        public LineRenderer line;
        public LayerMask layerMask;
        public int deepZ;
        public Camera cam;
        public Text text;
        private Vector3 beginPoint = Vector3.zero;

        void Update()
        {
            DrawRecangle();
            SelectCheck();

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


            // 鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask))
                {
                    beginPoint = hit.point;
                }
            }

            // 鼠标抬起
            if (Input.GetMouseButtonUp(0))
            {
                list.Clear();

                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100f, layerMask))
                {
                    Debug.Log($"开始点：{beginPoint}  结束点{hit.point}");
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
