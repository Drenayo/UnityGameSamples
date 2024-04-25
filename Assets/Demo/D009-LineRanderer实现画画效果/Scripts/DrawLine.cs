using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D009
{
    public class DrawLine : MonoBehaviour
    {
        public Color lineColor;
        public float lineWidth;
        public Material lineMaterial;

        private LineRenderer line;
        private List<Vector3> linePointList = new List<Vector3>();
        
        void Start()
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = lineMaterial;
            SetWidth();
            SetColor();
        }

        public void SetWidth()
        {
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
        }
        public void SetColor()
        {
            line.startColor = lineColor;
            line.endColor = lineColor;
        }


        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && !LineManager.IsPointerOverGameObject(Input.mousePosition))
            {
                // 2D写法
                Vector3 mousePoint = Input.mousePosition;
                //linePointList.Add(mousePoint);
                //line.positionCount = linePointList.Count;
                //line.SetPositions(linePointList.ToArray());

                // 3D写法 就是Canvas的类型是WorldSpace的写法
                mousePoint.z = 10;
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);
                linePointList.Add(worldPoint);
                line.positionCount = linePointList.Count;
                line.SetPositions(linePointList.ToArray());
            }
        }
    }
}
