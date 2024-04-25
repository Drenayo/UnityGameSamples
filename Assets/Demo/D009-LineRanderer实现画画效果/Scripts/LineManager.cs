using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && !IsPointerOverGameObject(Input.mousePosition))
            {
                DrawLine line = new GameObject("Line_" + lineList.Count.ToString()).AddComponent<DrawLine>();
                line.gameObject.layer = 6; // 设置层级
                line.lineWidth = lineWidthSlider.value;
                line.lineColor = lineColorImage.color;
                line.lineMaterial = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

                lineList.Add(line);

                // 获取上一个元素，然后禁用掉它的画线脚本
                if (lineList.IndexOf(line) > 0)
                {
                    lineList[lineList.IndexOf(line) - 1].enabled = false;
                }
            }

        }

        // 清空画板
        public void Btn_Clear()
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                Destroy(lineList[i].gameObject);
            }
            lineList.Clear();
        }

        // 撤销这一笔
        public void Btn_Pre()
        {
            Destroy(lineList[lineList.Count - 1].gameObject);
            lineList.RemoveAt(lineList.Count - 1);
        }


        /// <summary>
        /// 检测是否点击UI
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public static bool IsPointerOverGameObject(Vector2 mousePosition)
        {
            //创建一个点击事件
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            //向点击位置发射一条射线，检测是否点击UI
            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
