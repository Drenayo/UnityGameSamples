using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z2_1
{
    public class Item : MonoBehaviour
    {
        // 父物体 X轴 偏移位置
        // Entity XZ直径缩放 Y高度缩放
        // TextParent 位置随着父物体偏移、随着柱子缩放偏移、缩放随着柱子等比缩放

        public void SetItemPosX(float x)
        {
            transform.position = new Vector3(x, 0, 0);
        }

        public void SetEntityScale(float diameterX, float heightY, float diameterZ)
        {
            transform.Find("Entity").localScale = new Vector3(diameterX, heightY, diameterZ);
        }

        public void SetTextUIPos(float x, float y, float z)
        {
            transform.Find("TextParent").position = new Vector3(x, y, z);
        }

        public void SetTextUIScale(float x, float y, float z)
        {
            transform.Find("TextParent").localScale = new Vector3(x, y, z);
        }

        public void SetColor(Color color)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = color;
        }

        public void SetText(string value)
        {
            gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = value;
        }
    }
}
