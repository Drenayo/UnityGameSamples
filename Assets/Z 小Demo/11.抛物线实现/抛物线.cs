using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_11
{
    public class 抛物线 : MonoBehaviour
    {
        public LineRenderer line;
        public Vector3[] posList = new Vector3[50];
        public Transform firePos;
        public float force;
        public float subNumber;
        public GameObject prefabBullet;

        public ForceMode forceMode;

        void Update()
        {
            for (int i = 0; i < posList.Length; i++)
            {
                posList[i] = firePos.position + firePos.forward * force * i * subNumber + Physics.gravity * ((i * subNumber) * (i * subNumber) * .5f);
            }
            line.positionCount = posList.Length;
            line.SetPositions(posList);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(prefabBullet, firePos.position, firePos.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(firePos.forward * force, forceMode);
            }
        }

    }
}
