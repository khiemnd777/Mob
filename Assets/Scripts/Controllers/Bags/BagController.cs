using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class BagController : MonoHandler
	{
		public ScalableGridView gridView;
		public BagItem bagItemResource;

		Race _player;
		BagModule _bagModule;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_bagModule = _player.GetModule<BagModule> ();

			gridView.ClearAll ();

			EventManager.StartListening (Constants.EVENT_ITEM_BOUGHT_FIRED, new Action<string>(Prepare));
			EventManager.StartListening (Constants.EVENT_ITEM_OVER_IN_BAG, new Action<string>(Prepare));
		}

		void Prepare(string t){
			Debug.Log (t);
			var itemUIs = gridView.GetItems ().Select (x => x.GetComponent<BagItem> ()).ToArray();
			foreach (var item in itemUIs) {
				if (!_bagModule.items.Any (x => item.item.Equals (x))) {
					DestroyImmediate (item.gameObject);
				}
			}
			foreach (var item in _bagModule.items) {
				if(!itemUIs.Any(x => item.Equals(x.item))){
					var ui = Instantiate<BagItem> (bagItemResource, gridView.transform);
					ui.item = item;	
					continue;
				}
			}
		}
	}
}