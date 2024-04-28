using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D023
{
    public class SortSetting : ScriptableObject
    {

        // 控制
        [Header("排序速度")]
        [Range(1, 10)]
        public float sortspeed = 3;
        public float SortSpeed { get { return 1f / Mathf.Pow(2, sortspeed - 1); } }// 计算速度，1-10，1=1，2=0.5，3=0.25 等待速度依次递减，相反排序速度依次递增
        public SortType sortType;

        [Space(10)]
        // 生成数据
        [Header("随机的数据量")]
        public int dataSize = 20;
        [Header("随机数据最大值")]
        public int maxValue = 100;
        [Header("排序生成预制体")]
        public GameObject prefab;

        [Space(10)]
        // 排序显示
        [Header("实体柱子间隔")]
        [Range(.1f, 2f)]
        public float interval = 0.5f;
        [Header("柱子高度缩放倍数")]
        [Range(.1f, 1f)]
        public float multiples = .5f;
        [Header("柱子直径")]
        [Range(.1f, 2f)]
        public float diameter = 0.5f;
        [Header("UI数字大小")]
        [Range(.1f, 1f)]
        public float textSize = 1f;

        [Space(10)]
        // 颜色显示
        [Header("初始颜色")]
        public Color defaultColor = Color.white;
        [Header("已完成排序颜色")]
        public Color doneColor = Color.green;
        [Header("正在交换颜色")]
        public Color currColor = Color.cyan;
        [Header("锚点颜色")]
        public Color anchorColor = Color.black;
    }
}
