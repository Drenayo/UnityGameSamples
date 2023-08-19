using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_23
{
    public class HighlightFlicker : MonoBehaviour
    {
        [Header("变换材质")]
        public Material mat;
        public float speed;
        public Color startColor;
        public Color endColor;
        public bool isFlicker;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isFlicker = true;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                isFlicker = false;

            if (isFlicker)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, speed)));
            }
            else
            {
                mat.DisableKeyword("_EMISSION");
            }
        }
    }
}
