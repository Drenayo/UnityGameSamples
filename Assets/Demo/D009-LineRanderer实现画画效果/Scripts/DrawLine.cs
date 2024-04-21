using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D009
{
    public class DrawLine : MonoBehaviour
    {
        public Material lineMaterial;
        public Color lineColor;
        public float lineWidth;

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
            // 按住不放时，画线，因为是从鼠标位置转到世界位置的，所以都是XY轴生效，所以是面向屏幕的
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 mousePoint = Input.mousePosition;
                mousePoint.z = 10;
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);

                linePointList.Add(worldPoint);
                line.positionCount = linePointList.Count;
                line.SetPositions(linePointList.ToArray());
            }
        }
    }
}
