//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//namespace Mob
//{
//	public class ShopController : MobBehaviour
//	{
//		public ScrollableList list;
//		public ShopItem shopItemResource;
//
//		void Start(){
//			list.ClearAll ();
//			InitItems ();
//		}
//
//		void InitItems(){
//			list.ClearAll ();
//			foreach (var item in ShopAvailableItems.list) {
//				PrepareItems (item);
//			}
//			list.Refresh ();
//		}
//
//		void PrepareItems(BoughtItem boughtItem){
//			var ui = Instantiate<ShopItem> (shopItemResource, list.transform);
//			ui.boughtItem = boughtItem;
//		}
//	}	
//}