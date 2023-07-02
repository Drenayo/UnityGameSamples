using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_13
{
    public class PathManager : MonoBehaviour
    {
        public static PathManager Instance;
        public int row; // 行
        public int col; // 列
        public float gridWidth;

        public Transform gridParent;
        public Transform uiGridParent;
        public Transform startPos;
        public GameObject itemPrefab;
        public GameObject itemUIPrefab;

        public Material material_Red;
        public Material material_Yellow;
        public Material material_Blue;
        public Color ui_Red;
        public Color ui_Yellow;
        public Color ui_Blue;

        public int[,] currPathArray;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            ResetPath();
            GenerateGrid();
            GenerateUIGrid();
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
                if (g.name[0] % 2 == 0 && g.name[2] % 2 == 0) // 前偶后偶 红色
                    meshR.material = material_Red;
                else if (g.name[0] % 2 != 0 && g.name[2] % 2 == 0) // 前奇后偶 蓝色
                    meshR.material = material_Blue;
                else if (g.name[0] % 2 == 0 && g.name[2] % 2 != 0) // 前偶后奇 蓝色
                    meshR.material = material_Blue;
                else if (g.name[0] % 2 != 0 && g.name[2] % 2 != 0) // 前奇后奇 红色
                    meshR.material = material_Red;

                // 标志也重置一下
                g.tag = "Item";
            }
        }

        // 检查路径是否已经走完  当触发终点时则检测全程
        public bool IsPathDone()
        {
            // 遍历item的名字，根据名字判定二维数组下标
            foreach (Transform grid in gridParent)
            {
                int rowIndex = (int)char.GetNumericValue(grid.name[0]);
                int colIndex = (int)char.GetNumericValue(grid.name[2]);

                // 下标为1的地方，已经走过的节点没有标识finish，则视为未完成全程
                if (currPathArray[rowIndex, colIndex] == 1 && !grid.gameObject.CompareTag("Finish"))
                    return false;
            }
            return true;
        }

        // 显示路线按钮
        public void Btn_ShowTips()
        {
            if (uiGridParent.gameObject.activeSelf)
                uiGridParent.gameObject.SetActive(false);
            else
                uiGridParent.gameObject.SetActive(true);
        }

        // UI路径格子生成
        public void GenerateUIGrid()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    GameObject grid = Instantiate(itemUIPrefab, uiGridParent);
                    grid.name = $"{i},{j}";
                }
            }
            RefUIGridColor();
        }

        // 刷新一下UI格子的颜色，同时按照显示路径
        public void RefUIGridColor()
        {
            // 按照奇偶数规则，刷新成格子颜色
            foreach (Transform g in uiGridParent)
            {
                Image imageColor = g.GetComponent<Image>();
                if (g.name[0] % 2 == 0 && g.name[2] % 2 == 0) // 前偶后偶 红色
                    imageColor.color = ui_Red;
                else if (g.name[0] % 2 != 0 && g.name[2] % 2 == 0) // 前奇后偶 蓝色
                    imageColor.color = ui_Blue;
                else if (g.name[0] % 2 == 0 && g.name[2] % 2 != 0) // 前偶后奇 蓝色
                    imageColor.color = ui_Blue;
                else if (g.name[0] % 2 != 0 && g.name[2] % 2 != 0) // 前奇后奇 红色
                    imageColor.color = ui_Red;
            }

            // 然后把二维数组为1的地方的颜色变更一下即可
            foreach (Transform uiGrid in uiGridParent)
            {
                int rowIndex = (int)char.GetNumericValue(uiGrid.name[0]);
                int colIndex = (int)char.GetNumericValue(uiGrid.name[2]);
                if (currPathArray[rowIndex, colIndex] == 1)
                    uiGrid.GetComponentInChildren<Image>().color = ui_Yellow;
                //Color.yellow + new Color(0, 0, 0, pathAlpha);

            }
        }

        // 重置路径  玩家走到路线外，触发此函数, 行列数 固定4*4
        public void ResetPath()
        {
            FalshPath();
            RefColor();
            RefUIGridColor();
        }

        // 伪随机路径
        private void FalshPath()
        {
            int[,] randomPathArray1 = {
                { 1,1,1,1 },
                { 0,0,0,1 },
                { 0,0,0,1 },
                { 0,0,0,1 }};

            int[,] randomPathArray2 = {
                { 1,0,0,0 },
                { 1,1,0,0 },
                { 0,1,1,1 },
                { 0,0,0,1 }};

            int[,] randomPathArray3 = {
                { 1,1,1,0 },
                { 0,0,1,0 },
                { 0,0,1,1 },
                { 0,0,0,1 }};

            // 重置路线
            if (Random.Range(0, 4) == 0)
                currPathArray = randomPathArray1;
            else if (Random.Range(0, 4) == 1)
                currPathArray = randomPathArray2;
            else if (Random.Range(0, 4) == 2)
                currPathArray = randomPathArray3;
        }

        // 深度优先算法 最短路径
        private void Path_DFS()
        {

        }

        // 广度优先算法 最短路径
        private void Path_BFS()
        {

        }

        // RRT算法 基于随机采样的路径规划
        private void Path_RRT(int start,int goal, int maxIterations,double stepSize)
        {

        }
    }
}
