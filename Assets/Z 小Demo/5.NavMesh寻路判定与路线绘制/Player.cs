using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Z_5
{
    public class Player : MonoBehaviour
    {
        public LineRenderer line;
        private NavMeshAgent nav;
        public Transform target;

        void Start()
        {
            nav = GetComponent<NavMeshAgent>();

            // 如果是固定点，可以只设置一次，如果是动态目标，则需要Update
            nav.SetDestination(target.position);

            // 如果只需要规划路线，玩家自行走动 设置为true
            nav.isStopped = false;
        }


        void Update()
        {


            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                Debug.Log("抵达");
                nav.ResetPath();
            } 
            
            if (nav.hasPath)
                DrawLine();
        }




        // 绘制出路径点
        private void DrawLine()
        {
            // 获取路径
            Vector3[] navPath = nav.path.corners;
            // 把路径赋予LineRenderer
            line.positionCount = navPath.Length;
            line.SetPositions(navPath);
        }
    }
}
