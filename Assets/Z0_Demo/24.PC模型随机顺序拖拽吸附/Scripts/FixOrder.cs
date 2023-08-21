using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_24
{
    public class FixOrder : MonoBehaviour
    {
        public static FixOrder Instance;
        public int fixNumber = 0;
        public List<string> orderList;

        public Transform SoltParent;
        public Transform DragParent;

        void Awake()
        {
            Instance = this;
        }


        void Update()
        {
            
        }

        // 所有物体归于原位
        public void RestetFix()
        {
            foreach (Transform tran in DragParent)
            {
                tran.GetComponent<DragItem>().SetStartPos();
            }

            foreach (Transform tran in SoltParent)
            {
                tran.GetComponent<Solt>().soltItem = null;
            }
        }
    }
}
