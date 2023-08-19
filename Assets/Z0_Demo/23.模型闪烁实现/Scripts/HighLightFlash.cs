using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_23
{
    public class HighLightFlash : MonoBehaviour
    {
        public Material mat;
        public float flashSpeed;
        public float intensity;
        public Color startColor;
        public Color endColor;
        
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
