using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z1_1
{
    public class LineManager : MonoBehaviour
    {
        public Material lineMaterial;
        public float lineWidth;
        public Color lineColor;
        public GameObject linePrefab;
        public List<DrawLine> lineList = new List<DrawLine>();
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DrawLine line = Instantiate(linePrefab, transform).GetComponent<DrawLine>();
                line.lineWidth = lineWidth;
                line.lineColor = lineColor;
                line.lineMaterial = lineMaterial;

                // 保存之前画的每一笔画，同时把之前画上的Update的画笔程序禁用掉，保证新一笔画下时，其他所有笔画不在跟着画
                lineList.Add(line);

                // 获取上一个元素，然后禁用掉它的画线脚本
                if (lineList.IndexOf(line) > 0)
                {
                    lineList[lineList.IndexOf(line) - 1].enabled = false;
                }
            }

        }
    }
}
