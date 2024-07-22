using UnityEngine;

namespace D025
{
    public class Line : MonoBehaviour
    {
        [Header("线段顶点的数量")]
        public int numberOfPoints = 10;
        [Header("浮动高度")]
        public float amplitude = 1f;
        [Header("浮动频率")]
        public float frequency = 1f;
        [Header("浮动速度")]
        public float speed = 1f;

        private LineRenderer lineRenderer; // LineRenderer组件
        private Vector3[] points; // 存储线段顶点的数组

        void Start()
        {
            // 获取LineRenderer组件
            lineRenderer = GetComponent<LineRenderer>();
            // 设置线段顶点数量
            lineRenderer.positionCount = numberOfPoints;
            // 初始化顶点数组
            points = new Vector3[numberOfPoints];

            // 初始化每个顶点的位置
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = new Vector3(i, 0, 0);
            }
        }

        void Update()
        {
            // 动态调整每个顶点的位置
            for (int i = 0; i < numberOfPoints; i++)
            {
                // 使用正弦函数来模拟浮动效果
                float offset = Mathf.Sin(Time.time * speed + i * frequency) * amplitude;
                points[i] = new Vector3(i, offset, 0);
            }

            // 更新LineRenderer的顶点位置
            lineRenderer.SetPositions(points);
        }
    }
}
