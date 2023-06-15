using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_13
{
    public class Player : MonoBehaviour
    {
        public float speed;
        RaycastHit hit;

        void Update()
        {
            Move();
            RayDetection();
        }

        // 射线检测
        public void RayDetection()
        {
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
            {
                if (hit.collider.gameObject.CompareTag("Item"))
                {
                    GameObject item = hit.collider.gameObject;
                    int rowIndex = (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
                    int colIndex = (int)char.GetNumericValue(hit.collider.gameObject.name[2]);
                    if (PathManager.Instance.currPathArray[rowIndex, colIndex] == 1) // 路线正确
                    {
                        Debug.Log($"路线正确 [{rowIndex}:{colIndex}]");
                        item.GetComponent<MeshRenderer>().material = PathManager.Instance.material_Yellow;// 更换颜色
                        item.tag = "Finish"; // 更换tag标志
                        if (PathManager.Instance.IsPathDone())
                        {
                            Debug.Log("路线全程通过！");
                        }
                    }
                    else // 路线错误
                    {
                        Debug.Log("重置");
                        transform.position = PathManager.Instance.startPos.position;
                        PathManager.Instance.ResetPath();
                    }
                }
            }
        }

        // 移动
        public void Move()
        {
            float moveSpeed = speed * Time.deltaTime;
            float x = Input.GetAxis("Horizontal") * moveSpeed;
            float y = Input.GetAxis("Vertical") * moveSpeed;

            transform.Translate(x, 0, y);
        }
    }
}
