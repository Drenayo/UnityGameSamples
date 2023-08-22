using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GSFrame1;

namespace Z_25
{
    public class AnimationNotice : MonoBehaviour
    {
        public Animator animator;
        public UnityEvent doneEvent;
        public UnityAction donE;
        public MyCustomEvent eve;
        public bool isNotice;
        public UnityEvent<int> Unsada;
        void Start()
        {
            
        }

        public void TestTestZZZZ(int p1, int p2)
        {

        }

        void Update()
        {
            // 播放动画，播完回调
        }
    }

    public class MyCustomEvent : UnityEvent<int, int> 
    {

    }

}
