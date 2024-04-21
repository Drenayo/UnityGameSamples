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

        private void Start()
        {
            // 实例化材质
            //mat = new Material(Shader.Find("Standard"));
            //GetComponentInChildren<Renderer>().material = mat;
        }

        private void OnEnable()
        {
            mat.EnableKeyword("_EMISSION");
        }

        private void Update()
        {
            mat.SetColor("_EmissionColor", Color.Lerp(startColor * intensity, endColor * intensity, Mathf.PingPong(Time.time, flashSpeed)));
        }

        private void OnDisable()
        {
            mat.DisableKeyword("_EMISSION");
        }
    }
}
