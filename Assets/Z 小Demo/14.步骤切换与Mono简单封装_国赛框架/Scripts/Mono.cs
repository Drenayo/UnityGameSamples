using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Z_14
{
    public class Mono : MonoBehaviour
    {
        protected EventManager eventMgr;
        [HideInInspector]
        protected NodeManager nodeMgr;
        [HideInInspector]
        protected StepManager stepMgr;
        [HideInInspector]
        protected TimerManager timerMgr;

        protected void Init()
        {
            eventMgr = EventManager.GetInstance();
            nodeMgr = NodeManager.Instance;
            stepMgr = StepManager.Instance;
            timerMgr = TimerManager.Instance;
        }
        
        protected void SetActive(GameObject gameObj, bool isActive,float delayTime = 0)
        {
            StartCoroutine(SetActiveDealy(gameObj, isActive, delayTime));
        } 
        IEnumerator SetActiveDealy(GameObject gameObj, bool isActive, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            gameObj.SetActive(isActive);
        }

        // 根据父物体，向下全局查找子物体，包括已经隐藏的，若null则全局查找
        protected GameObject FindGameObject(string objName,Transform parent= null)
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
    }
}
