using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z_2
{
    public class CheckCube : MonoBehaviour
    {
        public LayerMask layerMask;
        public RaycastHit hit;
        public Text textName;
        public Text textAge;
        public GameObject buttonPre;
        public GameObject panel;
        public Text HP;
        public int hp;

        private void Start()
        {
            HP.text = hp.ToString();
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.CompareTag("Z2/Cube") && Input.GetMouseButtonDown(1))
                {
                    ItemCube itemCube = hit.collider.gameObject.GetComponent<ItemCube>();
                    textName.text = itemCube.nameStr;
                    textAge.text = itemCube.age.ToString();


                    // 生成前删除
                    foreach (Transform tran in panel.transform)
                    {
                        Destroy(tran.gameObject);
                    }
                    // 技能列表 生成按钮列表
                    for (int i = 0; i < itemCube.skillList.Count; i++)
                    {
                        GameObject buttton = Instantiate(buttonPre, panel.transform);
                        buttton.transform.Find("Text").GetComponent<Text>().text = itemCube.skillList[i].skillName;
                        int num = itemCube.skillList[i].skillHurt;
                        buttton.GetComponent<Button>().onClick.AddListener(() => OnCLickHrut(num));
                        // Button.onClick.AddListener()
                    }

                }
            }
        }

        public void OnCLickHrut(int hurt)
        {
            hp -= hurt;
            HP.text = hp.ToString();
        }
    }
}
