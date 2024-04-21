using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage1;

namespace GameDemo
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            
        }


        public void Save()
        {
            StorageManager.Instance.Save();
        }

        public void Load()
        {
            StorageManager.Instance.Load();
        }
    }
}
