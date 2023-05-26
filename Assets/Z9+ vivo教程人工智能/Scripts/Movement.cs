using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z9
{
    public class Movement : MonoBehaviour
    {
        public Transform target;
        public float animPlayRangeTime;


        private Animator anim;

        private IEnumerator Start()
        {
            anim = GetComponent<Animator>();
            yield return new WaitForSeconds(Random.Range(0, animPlayRangeTime));
            anim.SetBool("Run",true);
        }

        private void Update()
        {
            transform.LookAt(target);
        }
    }
}
