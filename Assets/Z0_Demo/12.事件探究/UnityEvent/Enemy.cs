using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Z_12_1
{
    public class Enemy : MonoBehaviour
    {
        public UnityEvent<float> onHrutEvent;
        public float hp;
        public float HP { get { return hp; } set { hp = value; } }

        private void Start()
        {
            onHrutEvent.AddListener(ShowHP);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                hp -= 1;
                onHrutEvent.Invoke(hp);
            }
        }

        public void ShowHP(float currHP)
        {
            Debug.Log($"剩余血量:{currHP}");
        }
    }
}
