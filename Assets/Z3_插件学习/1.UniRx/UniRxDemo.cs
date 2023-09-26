using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Test
{
	public class UniRxDemo : MonoBehaviour
	{
		public GameObject box;
		public ReactiveProperty<int> index = new ReactiveProperty<int>(1);
		private void Start()
		{
			// Observable.Timer(TimeSpan.FromSeconds(2f))
			// 	.Subscribe(_ => Debug.Log("2秒"))
			// 	.AddTo(this);
			//
			// Observable.EveryUpdate()
			// 	.Subscribe(_ => Debug.Log("213"))
			// 	.AddTo(this);
			// box.OnMouseDownAsObservable()
			//  	.Subscribe(_ => Debug.Log("被点击了"));
			//
			// index
			// 	.Subscribe(_ => Debug.Log($"值变更{index.Value}"))
			// 	.AddTo(this);

			//	Observable.EveryUpdate().Subscribe(_=>index.Value++);
			this.UpdateAsObservable()
				.Sample(TimeSpan.FromSeconds(1f))
				.Subscribe(unit => Debug.Log("每秒输出"));
		}

		private void Update()
		{
			
		}
	}
}