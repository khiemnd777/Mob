using System;

namespace Mob
{
	public class TabContent : MonoHandler
	{
		public string key;

		void Start(){
			EventManager.StartListening("fire-tab-content", new Action<string>(OnVisible));
		}

		void OnVisible(string key){
			gameObject.SetActive(this.key == key);
		}
	}
}

