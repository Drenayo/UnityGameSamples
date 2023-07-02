using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Z_14
{
    public class NodeManager : Mono
    {
        public static NodeManager Instance;
        protected void Awake()
        {
            Instance = this;
            Init();
        }

        public Transform pos1;
        public Transform pos2;
        public Transform pos3;

        // 切换位置
        public void SwitchPosition(Transform originTran, Vector3 targetPos, bool isWorld, float delayTimes = 0)
        {
            StartCoroutine(Dealy_Pos(originTran, targetPos, isWorld, delayTimes));
        }
        IEnumerator Dealy_Pos(Transform originTran, Vector3 targetPos, bool isWorld, float delayTimes)
        {
            yield return new WaitForSeconds(delayTimes);
            if (isWorld)
                originTran.position = targetPos;
            else
                originTran.localPosition = targetPos;
        }

        public void SwitchRotation(Transform originTran, Quaternion targetQua, bool isWorld, float delayTimes = 0)
        {
            StartCoroutine(Dealy_Rot(originTran, targetQua, isWorld, delayTimes));
        }
        IEnumerator Dealy_Rot(Transform originTran, Quaternion targetRot, bool isWorld, float delayTimes)
        {
            yield return new WaitForSeconds(delayTimes);
            if (isWorld)
                originTran.rotation = targetRot;
            else
                originTran.localRotation = targetRot;
        }
    }
}
