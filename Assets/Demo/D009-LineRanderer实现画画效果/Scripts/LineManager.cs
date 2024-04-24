using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D009
{
    public class LineManager : MonoBehaviour
    {
        public Slider lineWidthSlider;
        public Image lineColorImage;
        public List<DrawLine> lineList = new List<DrawLine>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DrawLine line = new GameObject("Line_" + lineList.Count.ToString()).AddComponent<DrawLine>();
                line.gameObject.layer = 6;
                line.lineWidth = lineWidthSlider.value;
                line.lineColor = lineColorImage.color;
                line.lineMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

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
