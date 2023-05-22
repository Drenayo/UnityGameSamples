using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z4
{
    public class CameraCtrl : MonoBehaviour
    {
        private bool isDragging = false;
        private RaycastHit hit;

        void Start()
        {
            
        }

        private void Update()
        {
            if (isDragging)
            {
                // 将物体的位置设置为鼠标位置
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            }
            else
            {
                // 检测鼠标是否悬浮在物体上
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // 如果鼠标悬浮在物体上，则将 isDragging 设置为 true
                    isDragging = true;
                }
            }

            // 如果鼠标左键被释放，则将 isDragging 设置为 false
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }
}
