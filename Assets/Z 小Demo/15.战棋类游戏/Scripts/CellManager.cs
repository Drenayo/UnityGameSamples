using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_15
{
    public class CellManager : MonoBehaviour
    {
        public GameObject cellPrefab;
        public Transform cellTran;
        public float xNumber;
        public float yNumber;
        public float cellSpacing;
        public float cellRadius;
        public float cellTall;

        void Start()
        {
            CreateCellMap();
        }


        void Update()
        {

        }

        // 生成格子
        public void CreateCellMap()
        {
            for (int i = 0; i < xNumber; i++)
            {
                for (int j = 0; j < yNumber; j++)
                {
                    GameObject cellObj = Instantiate(cellPrefab, cellTran);
                    cellObj.transform.localPosition = new Vector3(i * (cellRadius * 2) + i * cellSpacing, cellTall, j * (cellRadius * 2) + j * cellSpacing);
                }
            }
        }
    }
}
