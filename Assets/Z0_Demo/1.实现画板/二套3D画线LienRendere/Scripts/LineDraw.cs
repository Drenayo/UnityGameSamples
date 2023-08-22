using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_0
{
    public class LineDraw : MonoBehaviour
    {
        public Color color;
        public LayerMask layerMask;
        public Transform rayTran;
        public float distance;
        private int num = 0;
        private LineRenderer line;
        public float width;
        public float speed;
        void Start()
        {

        }

        void Update()
        {
            transform.Translate(transform.forward * Time.deltaTime * speed);

            RaycastHit hit;

            if (Physics.Raycast(rayTran.position, rayTran.forward, out hit, distance, layerMask))
            {
                Debug.Log("触发");
                if (Input.GetMouseButtonDown(0))
                {
                    if (color == null)
                    {
                        return;
                    }
                    GameObject obj = new GameObject();
                    line = obj.AddComponent<LineRenderer>();
                    line.widthMultiplier = width;
                    line.material.color = color;
                    line.SetPosition(0, hit.point);
                    line.SetPosition(1, hit.point);
                    num = 0;
                }
                if (Input.GetMouseButton(0))
                {
                    num++;
                    line.positionCount = num;
                    line.SetPosition(num - 1, hit.point + Vector3.up * 0.2f);

                }
            }
        }
    }
}
