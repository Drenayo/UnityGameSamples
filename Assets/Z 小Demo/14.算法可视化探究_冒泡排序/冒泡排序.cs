using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace Z_14
{
    public class 冒泡排序 : MonoBehaviour
    {
        [Header("步次速度")]
        public float stepSpeed;
        [Header("排序生成预制体")]
        public GameObject prefab;

        [Header("随机的数据量")]
        public int dataSize;
        [Header("随机数据最大值")]
        public int maxValue;

        [Header("实体柱子间隔")]
        public float interval;
        [Header("倍数关系（Y轴缩放与实际排序值的关系）")]
        public float multiples;
        [Header("柱子直径")]
        public float diameter;
        [Header("文字与柱子关系大小")]
        public float textSize;

        [Header("排序List")]
        public List<int> dataList;
        [Header("实体List")]
        public List<GameObject> entityList;

        void Start()
        {
            GenerateDataAndEntity();
        }

        // 生成数据与实体
        public void GenerateDataAndEntity()
        {
            // 先行删除所有数据
            dataList = new List<int>();
            entityList = new List<GameObject>();
            foreach (Transform tran in transform)
                Destroy(tran.gameObject);

            // 生成随机数据
            System.Random random = new System.Random();
            for (int i = 0; i < dataSize; i++)
            {
                dataList.Add(random.Next(1, maxValue));
            }

            // 按照数据匹配生成实体
            for (int i = 0; i < dataList.Count; i++)
            {
                GameObject obj = Instantiate(prefab, this.transform);
                // 位置改X轴  偏移
                obj.transform.position = new Vector3(i * (diameter + interval), 0, 0);
                // 缩放改Z轴  向上
                obj.transform.Find("Pos").localScale = new Vector3(diameter, dataList[i] * multiples, diameter);
                // 设置值
                obj.transform.Find("TextParent").position = new Vector3(obj.transform.position.x, diameter / 2, -(diameter / 2) + -(diameter / 5));
                obj.transform.Find("TextParent").localScale = new Vector3(diameter * textSize, diameter * textSize, 1);
                obj.transform.Find("TextParent").Find("TextUI").GetComponent<TMP_Text>().text = dataList[i].ToString();

                // 存入List
                entityList.Add(obj);
            }
        }

        // 设置可视化实体的参数
        public void SetSortEntity(GameObject entity, string value)
        {
            entity.transform.Find("TextParent").Find("TextUI").GetComponent<TMP_Text>().text = value;
        }
        public void SetSortEntity(GameObject entity, Color color)
        {
            entity.GetComponent<MeshRenderer>().material.color = color;
        }

        // 交换可视化实体的位置
        public void SwapEntityPos(Transform pos1, Transform pos2)
        {
            Vector3 temp = pos1.position;
            pos1.position = pos2.position;
            pos2.position = temp;
        }

        // 交换
        public void Swap(int s1, int s2)
        {
            int tempInt = dataList[s1];
            dataList[s1] = dataList[s2];
            dataList[s2] = tempInt;


            GameObject tempObj = entityList[s1];
            entityList[s1] = entityList[s2];
            entityList[s2] = tempObj;

            // 交换实际实体位置
            SwapEntityPos(entityList[s1].transform, entityList[s2].transform);
        }

        // 排序算法
        IEnumerator Sort_Y()
        {
            //for (int i = dataList.Count - 1; i >= 0; i--)
            //{
            //    for (int s = 1; s <= i; s++)
            //    {
            //        if (dataList[s - 1] > dataList[s])
            //        {
            //            yield return new WaitForSeconds(stepSpeed);
            //            Swap(s - 1, s);
            //            yield return new WaitForSeconds(stepSpeed);

            //        }
            //    }
            //}

            for (int i = 0; i < dataList.Count; i++)
            {
                int minValueIndex = i;
                for (int j = i + 1; j < dataList.Count; j++)
                {
                    minValueIndex = dataList[j] < dataList[minValueIndex] ? j : minValueIndex;
                }
                yield return new WaitForSeconds(stepSpeed);
                Swap(i, minValueIndex);
            }
        }

        Coroutine sortY;

        public void Btn_Start()
        {
            Debug.Log("开始模拟");
            sortY = StartCoroutine(Sort_Y());
        }
        public void Btn_Stop()
        {
            Debug.Log("暂停模拟");
            StopCoroutine(sortY);
        }
        public void Btn_Reste()
        {
            StopAllCoroutines();
            Debug.Log("重置模拟");
            GenerateDataAndEntity();
        }
    }
}
