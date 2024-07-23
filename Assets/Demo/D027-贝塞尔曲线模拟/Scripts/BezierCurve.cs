using UnityEngine;

namespace D027
{
    [RequireComponent(typeof(LineRenderer))]
    public class BezierCurve : MonoBehaviour
    {
        [Header("控制点组")]
        public Transform[] controlPoints;
        private LineRenderer lineRenderer;
        private int numPoints = 50; // 分段数

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = numPoints;
        }

        private void Update()
        {
            DrawBezierCurve();
        }

        void DrawBezierCurve()
        {
            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (float)(numPoints - 1);
                Vector3 point = CalculateBezierPoint(t, controlPoints);
                lineRenderer.SetPosition(i, point);
            }
        }

        Vector3 CalculateBezierPoint(float t, Transform[] points)
        {
            if (points.Length == 3)
            {
                return Mathf.Pow(1 - t, 2) * points[0].position +
                       2 * (1 - t) * t * points[1].position +
                       Mathf.Pow(t, 2) * points[2].position;
            }
            else if (points.Length == 4)
            {
                return Mathf.Pow(1 - t, 3) * points[0].position +
                       3 * Mathf.Pow(1 - t, 2) * t * points[1].position +
                       3 * (1 - t) * Mathf.Pow(t, 2) * points[2].position +
                       Mathf.Pow(t, 3) * points[3].position;
            }
            return Vector3.zero;
        }
    }

}
