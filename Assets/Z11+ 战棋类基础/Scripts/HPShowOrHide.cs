using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z11
{
    public class HPShowOrHide : MonoBehaviour
    {
        public Image hp;
        public float dealyHideTime;
        void Start()
        {
            
        }


        public void ShowHP(RoleData roleData)
        {
            gameObject.SetActive(true);
            hp.fillAmount = roleData.hp / roleData.maxhp;

        }

        IEnumerator DealyHide()
        {
            yield return new WaitForSeconds(dealyHideTime);
            gameObject.SetActive(false);
        }
    }
}
