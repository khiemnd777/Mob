//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class GearListUpgradeItem : MobBehaviour
//	{
//		public GearType gearType;
//		public Text titleText;
//		public Text briefText;
//		public Text priceText;
//		public Text statusText;
//		public Button upgradeBtn;
//		public GearItem item;
//		public GearController gearController;
//
//		void Start(){
//			upgradeBtn.onClick.AddListener (() => {
//				item.Upgrade();
//				//gearController.FilterItemsByType(gearType);
//			});
//		}
//
//		void Update(){
//			titleText.text = item.title;
//			briefText.text = item.brief;
//			priceText.text = Mathf.Floor(item.upgradePrice) + "g";
//		}
//	}	
//}