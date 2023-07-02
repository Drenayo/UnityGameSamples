using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_3
{
    public class Player : MonoBehaviour
    {
        public LayerMask layerMask;
        public RaycastHit hit;
        public GameObject outLine;

        void Update()
        {
            // 向下射线检测
            if (Physics.Raycast(transform.position, transform.up * -1, out hit, 1, layerMask))
            {
                OutLineShow(hit.collider.transform);
            }

            //demo中离开地块了，悬浮标识并没有消失，而是赚到了地面本身的根坐标处，地面使用了Terrain，所以根坐标不正确
            
        }

        // 显示选中线并且转移到当前地块位置
        public void OutLineShow(Transform tran)
        {
            outLine.SetActive(true);
            outLine.transform.position = new Vector3(tran.position.x, outLine.transform.position.y, tran.position.z);
        }
    }
}
