//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class GearController : MonoBehaviour 
//	{
//		public ScrollableList list;
//		public GearListBuyItem listBuyItemResource;
//		public GearListUpgradeItem listUpgradeResource;
//
//		public Image helm;
//		public Image armor;
//		public Image weapon;
//		public Image cloth;
//		public Image ring;
//		public Image artifact;
//
//		public GearBoughtItem[] availableItems;
//
//		public Race player;
//		GearModule _gearModule;
//		Sprite _none;
//
//		void Start(){
//			list.ClearAll ();
//			player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
//			_gearModule = player.GetModule<GearModule> ();
//			_none = Resources.Load<Sprite> ("Sprites/none");
//			availableItems = GearAvailableItems.list.ToArray ();
//		}
//
//		public void WearHelm(){
//			helm.sprite = _gearModule.helm != null ? _gearModule.helm.GetIcon () : Resources.Load<Sprite> ("Sprites/buy-helm");
//		}
//
//		public void WearArmor(){
//			armor.sprite = _gearModule.armor != null ? _gearModule.armor.GetIcon () : Resources.Load<Sprite> ("Sprites/buy-armor");
//		}
//
//		public void WearWeapon(){
//			weapon.sprite = _gearModule.weapon != null ? _gearModule.weapon.GetIcon () : Resources.Load<Sprite> ("Sprites/buy-weapon");
//		}
//
//		public void WearCloth(){
//			cloth.sprite = _gearModule.cloth != null ? _gearModule.cloth.GetIcon () : Resources.Load<Sprite> ("Sprites/buy-cloth");
//		}
//
//		public void WearRing(){
//			ring.sprite = _gearModule.ring != null ? _gearModule.ring.GetIcon () : Resources.Load<Sprite> ("Sprites/buy-ring");
//		}
//
//		public void WearArtifact(){
//			artifact.sprite = _gearModule.artifact != null ? _gearModule.artifact.GetIcon () : _none;
//		}
//
//		public GearBoughtItem[] GetAvailableGearBoughtItemsByType(GearType gearType){
//			return availableItems
//				.Where (x => x.gearType == gearType && x.inStoreState == InStoreState.Available)
//				.ToArray ();
//		}
//
//		public GearItem[] GetOwnItemsByType(params GearType[] gearTypes){
//			var ownList = new List<GearItem> ();
//			foreach (var gearType in gearTypes) {
//				switch (gearType) {
//				case GearType.Armor:
//					ownList.Add (_gearModule.armor);
//					break;
//				case GearType.Artifact:
//					ownList.Add (_gearModule.artifact);
//					break;
//				case GearType.Cloth:
//					ownList.Add (_gearModule.cloth);
//					break;
//				case GearType.Ring:
//					ownList.Add (_gearModule.ring);
//					break;
//				case GearType.Helm:
//					ownList.Add (_gearModule.helm);
//					break;
//				case GearType.Weapon:
//					ownList.Add (_gearModule.weapon);
//					break;
//				default:
//					break;
//				}
//			}
//			return ownList.Where(x => x != null).ToArray();
//		}
//
//		public object[] GetGearItemsByType(GearType gearType){
//			var boughtItems = GetAvailableGearBoughtItemsByType (gearType);
//			var ownItems = GetOwnItemsByType (gearType);
//			var list = new List<object> ();
//			list.AddRange (ownItems);
//			list.AddRange (boughtItems);
//			return list.ToArray ();
//		}
//
//		public void FilterItemsByType(GearType gearType){
//			list.ClearAll ();
//			var filteredItems = GetGearItemsByType (gearType);
//			foreach (var item in filteredItems) {
//				if (typeof(GearBoughtItem).IsAssignableFrom(item.GetType ())) {
//					var gearSLI = Instantiate<GearListBuyItem> (listBuyItemResource, list.transform);
//					gearSLI.boughtItem = (GearBoughtItem)item;
//					gearSLI.gearType = gearType;
//					gearSLI.gearController = this;
//				} else if (typeof(GearItem).IsAssignableFrom(item.GetType ())) {
//					var gearSLI = Instantiate<GearListUpgradeItem> (listUpgradeResource, list.transform);
//					gearSLI.item = (GearItem)item;
//					gearSLI.gearType = gearType;
//					gearSLI.gearController = this;
//				}
//			}
//			list.Refresh ();
//		}
//
//		public void FilterUpgradedItemByType(GearType gearType){
//			
//		}
//
//		void Update(){
//			WearHelm ();
//			WearArmor ();
//			WearCloth ();
//			WearWeapon ();
//			WearRing ();
//			WearArtifact ();
//		}
//	}	
//}