using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace D024
{
    [System.Serializable]
    public class GameNode : MonoBehaviour
    {
        public void SetRadius(float r)
        {
            transform.localScale = new Vector3(r * 2, r * 2, r * 2);
        }

        public void SetText(string str)
        {
            transform.Find("TextValue").GetComponent<TMP_Text>().text = str;
        }

        public void SetTextIndex(int index)
        {
            transform.Find("TextIndex").GetComponent<TMP_Text>().text = $"[{index.ToString()}]";
        }

        public void SetPosition(float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        public void SetColor(Color c)
        {
            GetComponent<MeshRenderer>().material.color = c;
        }

        // 设置指向
    }
}
