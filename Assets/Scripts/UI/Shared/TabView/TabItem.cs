using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class TabItem : MonoHandler
	{
		public string key;
		public Button actionBtn;

		void Start(){
			actionBtn.onClick.AddListener (Select);
		}

		void Select(){
			Debug.Log ("call " + key);
			EventManager.TriggerEvent ("fire-tab-content");
		}
	}
}

