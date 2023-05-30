using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_12_2
{
    public class Player : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed;
        void Start()
        {
            
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bullet =  Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
                Destroy(bullet, 5f);
            }
        }
    }
}
