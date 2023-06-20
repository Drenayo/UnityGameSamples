using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Z_14
{
    /// <summary>
    /// 重写MonoBehaviour脚本
    /// </summary>
    public class Mono : MonoBehaviour
    {
        /// <summary>
        /// 启用和禁用物体
        /// </summary>
        /// <param name="gameObj"></param>
        /// <param name="isActive"></param>
        /// <param name="times"></param>
        public void SetActive(GameObject gameObj, bool isActive,float times = 0)
        {
            StartCoroutine(SetActiveDealy(gameObj, isActive, times));
        } 
        IEnumerator SetActiveDealy(GameObject gameObj, bool isActive, float times)
        {
            yield return new WaitForSeconds(times);
            gameObj.SetActive(isActive);
        }

        // 根据父物体，向下全局查找子物体，包括已经隐藏的，若null则全局查找
        public GameObject FindGameObject(string objName,Transform parent= null)
        {
            Queue<Transform> queue = new Queue<Transform>();

            if (parent == null)
            {
                for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
                {
                    queue.Enqueue(SceneManager.GetActiveScene().GetRootGameObjects()[i].transform);
                }
            }
            else
                queue.Enqueue(parent);

            while (queue.Count > 0)
            {
                Transform current = queue.Dequeue();

                Debug.Log(current.name);
                if (current.name.Equals(objName))
                    return current.gameObject;

                //如果当前节点有子节点，则将其加入队列，留待以后遍历
                for (int i = 0; i < current.childCount; i++)
                {
                    queue.Enqueue(current.GetChild(i));
                }
            }

            Debug.Log("没有找到" + objName + "节点");
            return null;
        }

        // 切换位置
        public void PositionSwitch(Transform originTran, Transform targetTran, bool isRot, float delayTimes = 0)
        {
            StartCoroutine(Dealy_Pos(originTran, targetTran, isRot, delayTimes));
        }
        IEnumerator Dealy_Pos(Transform originTran, Transform targetTran, bool isRot, float times)
        {
            yield return new WaitForSeconds(times);
            originTran.localPosition = targetTran.localPosition;
            if (isRot)
                originTran.localRotation = targetTran.localRotation;
        }
    }
}
