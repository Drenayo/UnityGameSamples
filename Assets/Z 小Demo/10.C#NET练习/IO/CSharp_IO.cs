using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Z_10
{
    public class CSharp_IO : MonoBehaviour
    {
        public TextAsset text;
        private void Start()
        {
            Debug.Log(text.text);
            
            AssetDatabase.Refresh();
        }
    }
}
