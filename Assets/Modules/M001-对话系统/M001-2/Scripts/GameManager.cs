using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M001_2
{
    public class GameManager : MonoBehaviour
    {
        public GameObject dialogGo;
        [Header("想要触发的对话ID编号")]
        public int dialogID;
        void Start()
        {
            
        }
        public void OnClick_Dialogue()
        {
            dialogGo.SetActive(true);
            DialogueManager.Instance.SetDialogID(dialogID);
        }
    }
}
