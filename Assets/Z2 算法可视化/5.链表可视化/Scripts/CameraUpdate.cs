using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2_5
{
    public class CameraUpdate : MonoBehaviour
    {
        private Camera came;
        void Start()
        {
            came = Camera.main;
        }


        void Update()
        {

        }

        // 摄像机更新
        public void CameraPosUpdate(Transform nodeParent,int nodeCount)
        {
            // 20个节点 10size 最末尾38 摄像机X轴 末尾/2+0.5
            // 10个节点 5size 最末尾18 摄像机X轴 末尾/2+0.5
            // 8个节点 4size 最末尾14 摄像机X轴 末尾/2+0.5
            // 6个节点 3size 最末尾10 摄像机X轴 末尾/2+0.5
            int nodeNumber = nodeCount;
            float lastNodeXValue = nodeParent.GetChild(nodeNumber - 1).position.x;
            int size = Mathf.CeilToInt(nodeNumber / 2f);
            float cameraXValue = lastNodeXValue / 2f + .5f;
            //Debug.Log($"节点：{nodeNumber} \n 最后值：{lastNodeXValue} \n Size:{size} \n came X: {cameraXValue}");
            came.orthographicSize = size;
            came.transform.localPosition = new Vector3(cameraXValue, came.transform.localPosition.y, came.transform.localPosition.z);
        }
    }
}
