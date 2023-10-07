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

	}
}