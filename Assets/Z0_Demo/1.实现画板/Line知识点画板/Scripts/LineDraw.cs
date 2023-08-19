using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_0
{
    public class LineDraw : MonoBehaviour
    {
        public Slider slider; // 画笔宽度控制

        private LineRenderer line;
        //private Material mat; // 画笔颜色材质
        private int num = 0;//总共画画点数
        public Color c;
        
        void Start()
        {
            slider.value = 0.1f;
        }
        
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (c == null)
                    {
                        return;
                    }
                    GameObject obj = new GameObject();
                    line = obj.AddComponent<LineRenderer>();
                    line.material.color = c;
                    line.widthMultiplier = slider.value;//宽度
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
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(ChangeColor());
                }
            }
        }

        IEnumerator ChangeColor()
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture2D.Apply();
            c = texture2D.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);
        }
    }
}
