//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class GearListBuyItem : MobBehaviour
//	{
//		public GearType gearType;
//		public Text titleText;
//		public Text briefText;
//		public Text priceText;
//		public Text statusText;
//		public Button buyBtn;
//		public GearBoughtItem boughtItem;
//		public GearController gearController;
//
//		Race _player;
//		GearModule _gearModule;
//
//		void Start(){
//			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
//			_gearModule = _player.GetModule<GearModule> ();
//
//			buyBtn.onClick.AddListener (() => {
//				boughtItem.Buy(Race.FindWithPlayerId(Constants.PLAYER1)[0], quantity: 1);
//				gearController.FilterItemsByType(gearType);
//			});
//		}
//
//		void Update(){
//			titleText.text = boughtItem.title;
//			briefText.text = boughtItem.brief;
//			priceText.text = Mathf.Floor(boughtItem.price) + "g";
//		}
//	}	
//}