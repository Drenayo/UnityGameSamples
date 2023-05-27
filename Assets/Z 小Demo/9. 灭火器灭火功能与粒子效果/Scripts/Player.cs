using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_9
{
    public class Player : MonoBehaviour
    {
        public ParticleSystem particle;

        void Start()
        {

        }


        void Update()
        {
            // 按下鼠标左键
            if (Input.GetMouseButton(0))
            {
                particle.gameObject.SetActive(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                particle.gameObject.SetActive(false);
            }
        }
    }
}
