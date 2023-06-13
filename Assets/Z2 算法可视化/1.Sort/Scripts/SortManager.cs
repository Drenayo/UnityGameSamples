using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace Z2_1
{
    public enum SortType
    {
        冒泡,
        归并,
        选择,
        插入,
        快速
    }

    public class SortManager : MonoBehaviour
    {
        public List<int> dataList;
        public List<GameObject> entityList;
        public SortSetting sortSetting;

        void Start()
        {
            GenerateDataAndEntity();
        }

        // 开始
        public void Btn_Start()
        {

            switch (sortSetting.sortType)
            {
                case SortType.冒泡:
                    StartCoroutine(BubbleSort_Y());
                    break;
                case SortType.归并:
                    break;
                case SortType.选择:
                    StartCoroutine(SelectSort_Y());
                    break;
                case SortType.插入:
                    StartCoroutine(InsertSort_Y());
                    break;
                case SortType.快速:
                    break;
                default:
                    break;
            }
        }

        // 暂停
        public void Btn_Stop()
        {
            if (Time.timeScale == 0)
            {
                GameObject.Find("Btn_Stop").GetComponentInChildren<Text>().text = "暂停模拟";
                Time.timeScale = 1;
            }
            else
            {
                GameObject.Find("Btn_Stop").GetComponentInChildren<Text>().text = "继续模拟";
                Time.timeScale = 0;
            }
        }

        // 重置
        public void Btn_Reste()
        {
            StopAllCoroutines();
            GenerateDataAndEntity();
            Time.timeScale = 1;
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
            for (int i = 0; i < sortSetting.dataSize; i++)
            {
                dataList.Add(random.Next(1, sortSetting.maxValue));
            }

            // 按照数据匹配生成实体
            for (int i = 0; i < dataList.Count; i++)
            {
                GameObject obj = Instantiate(sortSetting.prefab, this.transform);
                Item objScripts = obj.GetComponent<Item>();

                // 位置改X轴  偏移
                objScripts.SetItemPosX(i * (sortSetting.diameter + sortSetting.interval));

                // 缩放改Z轴  向上
                objScripts.SetEntityScale(sortSetting.diameter, dataList[i] * sortSetting.multiples * .2f, sortSetting.diameter);

                // 设置UI
                objScripts.SetTextUIPos(obj.transform.position.x, sortSetting.diameter / 1.5f, -(sortSetting.diameter / 2) + -0.01f);
                objScripts.SetTextUIScale(sortSetting.diameter * sortSetting.textSize + (.5f * sortSetting.diameter), sortSetting.diameter * sortSetting.textSize + (.5f * sortSetting.diameter), 1);
                objScripts.SetText(dataList[i].ToString());

                // 存入List
                entityList.Add(obj);
            }
        }

        // 交换数据、实体的位置
        private void Swap(int s1, int s2)
        {
            // 交换数据
            int tempInt = dataList[s1];
            dataList[s1] = dataList[s2];
            dataList[s2] = tempInt;

            // 交换实体在List中位置
            GameObject tempObj = entityList[s1];
            entityList[s1] = entityList[s2];
            entityList[s2] = tempObj;

            // 交换实际实体位置
            Vector3 temp = entityList[s1].transform.position;
            entityList[s1].transform.position = entityList[s2].transform.position;
            entityList[s2].transform.position = temp;
        }

        // 冒泡排序
        IEnumerator BubbleSort_Y()
        {
            for (int i = dataList.Count - 1; i >= 0; i--)
            {
                for (int s = 1; s <= i; s++)
                {
                    if (dataList[s - 1] > dataList[s])
                    {
                        // 当前正在对比的两个数据项 进行着色
                        entityList[s - 1].GetComponent<Item>().SetColor(sortSetting.currColor);
                        entityList[s].GetComponent<Item>().SetColor(sortSetting.currColor);
                        yield return new WaitForSeconds(sortSetting.SortSpeed);

                        Swap(s - 1, s); //交换  前一个比后一个大 两者交换

                        // 交换完毕，把已经交换的项设为默认色
                        entityList[s - 1].GetComponent<Item>().SetColor(sortSetting.defaultColor);
                        yield return new WaitForSeconds(sortSetting.SortSpeed * .5f);
                    }
                    else // 如果没有后一个大，则把之前用来比较的项设为默认色，将后一个比较大的设为当前色，开始比较
                    {
                        entityList[s].GetComponent<Item>().SetColor(sortSetting.currColor);
                        yield return new WaitForSeconds(sortSetting.SortSpeed);
                        entityList[s - 1].GetComponent<Item>().SetColor(sortSetting.defaultColor);
                    }

                    // 如果到了最后一个，则设颜色为已完成
                    if (s == i)
                    {
                        entityList[s].GetComponent<Item>().SetColor(sortSetting.doneColor);
                    }

                }

                // 指针走到最开始处，把最开始设颜色为已完成
                if (i == 0)
                {
                    entityList[i].GetComponent<Item>().SetColor(sortSetting.doneColor);
                }
            }
        }

        // 选择排序
        IEnumerator SelectSort_Y()
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                int minValueIndex = i;
                for (int j = i + 1; j < dataList.Count; j++)
                {
                    // 当前正在对比的 进行着色
                    entityList[j].GetComponent<Item>().SetColor(sortSetting.currColor);
                    entityList[minValueIndex].GetComponent<Item>().SetColor(sortSetting.currColor);
                    yield return new WaitForSeconds(sortSetting.SortSpeed);

                    if (dataList[j] < dataList[minValueIndex])
                    {
                        entityList[minValueIndex].GetComponent<Item>().SetColor(sortSetting.defaultColor);
                        minValueIndex = j; //最小值易位
                        entityList[minValueIndex].GetComponent<Item>().SetColor(sortSetting.currColor);
                        yield return new WaitForSeconds(sortSetting.SortSpeed);
                    }
                    else
                    {
                        entityList[j].GetComponent<Item>().SetColor(sortSetting.defaultColor);
                        yield return new WaitForSeconds(sortSetting.SortSpeed);
                    }
                }

                Swap(i, minValueIndex);
                entityList[i].GetComponent<Item>().SetColor(sortSetting.doneColor);
                yield return new WaitForSeconds(sortSetting.SortSpeed);
            }
        }

        // 插入排序 **********************
        IEnumerator InsertSort_Y()
        {
            for (int end = 1; end < dataList.Count; end++)
            {
                int newNumIndex = end;

                entityList[newNumIndex - 1].GetComponent<Item>().SetColor(sortSetting.currColor);
                entityList[newNumIndex].GetComponent<Item>().SetColor(sortSetting.currColor);
                yield return new WaitForSeconds(sortSetting.SortSpeed);

                while (newNumIndex - 1 >= 0 && dataList[newNumIndex - 1] > dataList[newNumIndex])
                {
                    Swap(newNumIndex - 1, newNumIndex);
                    entityList[newNumIndex].GetComponent<Item>().SetColor(sortSetting.defaultColor);
                    yield return new WaitForSeconds(sortSetting.SortSpeed);
                    newNumIndex--;
                }
            }
        }


    }
}
