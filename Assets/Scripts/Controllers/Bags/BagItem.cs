using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class BagItem : MonoHandler 
	{
		public Image icon;
		public Text quantity;
		public Button useBtn;
		public Item item;

		Race _player;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			useBtn.onClick.AddListener (() => {
				_player.GetModule<BagModule>(x => {
					x.Use(item, new Race[0]);
				});
			});
		}

		void Update(){
			Prepare ();
		}

		void Prepare(){
			if (item == null)
				return;
			useBtn.interactable = item.EnoughEnergy () && item.EnoughLevel () && item.EnoughCooldown ();
			quantity.text = "x" + item.quantity;
			icon.sprite = item.GetIcon("default") ?? item.GetIcon("none");
		}
	}	
}