using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Test
{
	
	public class OdinDemo : MonoBehaviour
	{

		public List<GameObject> list;

		[ChildGameObjectsOnly]
		public GameObject obj;

		[InlineEditor]
		public Transform tran;

		public bool b;


		
        private void Update()
        {
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
				if (hit.collider.TryGetComponent(out Father fa))
                {
					Debug.Log(fa.gameObject.name + "_");
					Debug.Log(fa is Son);
                }
				Debug.Log(hit.collider.gameObject.name);
            }
   //         if (obj.TryGetComponent(out Father fa))
   //         {
			//	Debug.Log(fa is Son2);
   //         }
			//else
   //         {
			//	Debug.Log("没有被获取");
   //         }
        }
    }
}