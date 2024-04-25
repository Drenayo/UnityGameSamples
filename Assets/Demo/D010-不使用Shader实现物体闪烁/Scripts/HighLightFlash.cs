using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D010
{
    public class HighLightFlash : MonoBehaviour
    {
        public float flashSpeed;
        public float intensity;
        public Color startColor;
        public Color endColor;
        public Material mat;
        private Material matInstance;

        private void OnEnable()
        {
            // 创建材质实例
            matInstance = new Material(mat);
            matInstance.EnableKeyword("_EMISSION");
            // 将新的材质实例应用到对象上
            GetComponent<Renderer>().material = matInstance;
        }

        private void Update()
        {
            //在材质（mat）上设置发光颜色（"_EmissionColor"），通过在两种颜色（startColor 和 endColor）之间进行线性插值（Lerp），并使用 PingPong 函数在这两种颜色之间循环变化。
            matInstance.SetColor("_EmissionColor", Color.Lerp(startColor * intensity, endColor * intensity, Mathf.PingPong(Time.time, flashSpeed)));
        }

        private void OnDisable()
        {
            Destroy(matInstance);
        }
    }
}
