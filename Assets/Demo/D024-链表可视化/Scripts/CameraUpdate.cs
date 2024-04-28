using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D024
{
    public class CameraUpdate : MonoBehaviour
    {
        public Camera came;
        public int count;
        public Transform parent;
        private void Update()
        {
            count = parent.childCount;
        }
        // 摄像机更新
        public void CameraPosUpdate()
        {
            // 20个节点 10size 最末尾38 摄像机X轴 末尾/2+0.5
            // 10个节点 5size 最末尾18 摄像机X轴 末尾/2+0.5
            // 8个节点 4size 最末尾14 摄像机X轴 末尾/2+0.5
            // 6个节点 3size 最末尾10 摄像机X轴 末尾/2+0.5


            float lastNodeXValue = parent.GetChild(count - 1).position.x;
            int size = Mathf.CeilToInt(count / 2f) + 1;
            float cameraXValue = lastNodeXValue / 2f + .5f;


            Debug.Log($"{count} / {2} = {Mathf.CeilToInt(count / 2f)}");


            came.orthographicSize = size;
            came.transform.localPosition = new Vector3(cameraXValue, came.transform.localPosition.y, came.transform.localPosition.z);
        }
    }
}
