using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z11
{
    public class HPShowOrHide : MonoBehaviour
    {
        public GameObject hpParent;
        public Image hp;
        public float dealyHideTime;
        void Start()
        {

        }


        public void ShowHP(RoleData roleData)
        {
            hpParent.SetActive(true);
            hp.fillAmount = roleData.hp / roleData.maxhp;
            StartCoroutine(DealyHide());
        }

        IEnumerator DealyHide()
        {
            yield return new WaitForSeconds(dealyHideTime);
            hpParent.SetActive(false);
            //Debug.Log("隐藏血条");
        }
    }
}
