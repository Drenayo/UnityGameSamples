using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M001_2
{
    public class GameManager : MonoBehaviour
    {
        public GameObject dialogGo;
        void Start()
        {
            
        }
        public void OnClick_Dialogue()
        {
            dialogGo.SetActive(true);
            DialogueManager.Instance.SetDialogID(8);
        }
    }
}
