using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z1
{
    public class GizmosTest : MonoBehaviour
    {
        public Transform 实心盒体;
        public Transform 实心球体;
        public Transform 线框盒体;
        public Transform 线框球体;
        public Mesh 网格;
        public Mesh 线框网格;

        // 每帧绘制 不需要运行，直接在Scene上绘制
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(实心盒体.position, new Vector3(1, 1, 1));
            Gizmos.DrawSphere(实心球体.position, .5f);

            Gizmos.DrawWireCube(线框盒体.position, new Vector3(1, 1, 1));
            Gizmos.DrawWireSphere(线框球体.position, .5f);

            Gizmos.DrawMesh(网格);
            Gizmos.DrawWireMesh(线框网格);

        }

        // 对象被选中时绘制
        private void OnDrawGizmosSelected()
        {

        }
    }
}


