using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_20
{
    public class Ads : MonoBehaviour
    {
        public List<Transform> originTranList;
        public List<Transform> currentTranList;
        
        void Start()
        {
            
        }


        void Update()
        {
            // 需要通过一个标识来判定两者是否为同一个，可以使用Tag，物体名字，或者挂载一个信息脚本，填写不一样的信息（这个适用有大量信息要处理的时候）
            // 使用Tag

            //if (Vector3.Distance(originalTran.position, transform.position) < 0.3f)
            //{
            //    transform.position = originalTran.position;
            //}
        }

        
    }
}
