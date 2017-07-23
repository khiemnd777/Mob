using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class MainTestModule : MonoHandler
	{
		public Text pointText;
		public Text goldText;

		Race _player;

		void Awake(){
			pointText.text = "";
			Race.CreatePrimitive<Swordman> ((p1) => {
				p1.tag = Constants.PLAYER1;
				p1.name = Constants.PLAYER1;
				p1.DefaultValue ();
				p1.GetModule<GoldModule>(x => x.AddGold(999999));
				p1.GetModule<StatModule>(x => x.point = 1000);
			});

			GearAvailableItems.Init ();
			GearUpgradedItems.Init ();
			ShopAvailableItems.Init ();
		}

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
		}

		void Update(){
			_player.GetModule<StatModule> (x => {
				pointText.text = "Point: " + Mathf.FloorToInt(x.point);
			});

			_player.GetModule<GoldModule> (x => {
				goldText.text = "Gold: " + Mathf.FloorToInt(x.gold) + "g";
			});
		}
	}
}

