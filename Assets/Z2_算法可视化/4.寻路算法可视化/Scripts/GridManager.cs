using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z2_4
{
    public class GridManager : MonoBehaviour
    {
        public int row; // 行
        public int col; // 列
        public float gridWidth; // 宽度

        public Transform gridParent; // 格子的父节点
        public GameObject itemPrefab; // 格子预制体

        public Color red;
        public Color yellow;
        public Color blue;

        public int[,] currPathArray;

        private void Start()
        {
            GenerateGrid();
        }

        // 生成Grid
        public void GenerateGrid()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject grid = Instantiate(itemPrefab, gridParent);
                    grid.transform.position = new Vector3(j * gridWidth, 0, i * gridWidth * -1)
                        + gridParent.position
                        + new Vector3(gridWidth / 2, 0, gridWidth / 2 * -1)
                        - new Vector3(row * gridWidth / 2, 0, col * gridWidth / 2 * -1);
                    grid.name = $"{i},{j}";
                }
            }
            RefColor();
        }

        // 刷新颜色
        public void RefColor()
        {
            foreach (Transform g in gridParent)
            {
                MeshRenderer meshR = g.GetComponent<MeshRenderer>();
                // 分割字符串
                string[] str = g.name.Split(",");
                int frontNumber = System.Convert.ToInt32(str[0]);
                int backNumber = System.Convert.ToInt32(str[1]);


                if (frontNumber % 2 == 0 && backNumber % 2 == 0) // 前偶后偶 红色
                    meshR.material.color = red;
                else if (frontNumber % 2 != 0 && backNumber % 2 == 0) // 前奇后偶 蓝色
                    meshR.material.color = blue;
                else if (frontNumber % 2 == 0 && backNumber % 2 != 0) // 前偶后奇 蓝色
                    meshR.material.color = blue;
                else if (frontNumber % 2 != 0 && backNumber % 2 != 0) // 前奇后奇 红色
                    meshR.material.color = red;
            }
        }
    }
}
