//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class TouchOnGear : MobBehaviour
//	{
//		public GearType gearType;
//		public GearController gearController;
//
//		Button _btn;
//
//		Race _player;
//		GearModule _gearModule;
//
//		void Start(){
//			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
//			_gearModule = _player.GetModule<GearModule> ();
//			_btn = GetComponent<Button> ();
//			_btn.onClick.AddListener (() => {
//				Act();	
//			});
//		}
//
//		void Act(){
//			gearController.FilterItemsByType (gearType);
//		}
//	}	
//}