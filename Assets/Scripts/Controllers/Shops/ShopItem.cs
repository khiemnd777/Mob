//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class ShopItem : MobBehaviour 
//	{
//		public Text title;
//		public Text brief;
//		public Text price;
//		public Image icon;
//		public Button buyBtn;
//
//		public BoughtItem boughtItem;
//
//		Race _player;
//		BagModule _bagModule;
//
//		void Start(){
//			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
//
//			buyBtn.onClick.AddListener (() => {
//				boughtItem.Buy(_player, boughtItem.price, 1);
//				EventManager.TriggerEvent(Constants.EVENT_ITEM_BOUGHT_FIRED, new {t = "test"});
//			});
//		}
//
//		void Update(){
//			title.text = boughtItem.title;
//			brief.text = boughtItem.brief;
//			price.text = Mathf.Floor(boughtItem.price) + "p";
//			icon.sprite = boughtItem.GetIcon("default") ?? boughtItem.GetIcon("none");
//		}
//	}	
//}