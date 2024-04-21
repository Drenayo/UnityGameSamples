using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace D006
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

            // 如果只需要规划寻路路径，玩家自行控制走过去 设置为true
            nav.isStopped = false;
        }


        void Update()
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                Debug.Log("抵达");
                nav.ResetPath();
            } 
            
            // 判断有无路径，有则绘制路径
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

        // 重新寻路
        public void ResetWayFinding()
        {
            // 随机重置目标点位置
            target.position = new Vector3(UnityEngine.Random.Range(-20, 20), target.position.y, UnityEngine.Random.Range(-20, 20));
            // 设置点
            nav.SetDestination(target.position);
        }
    }
}
