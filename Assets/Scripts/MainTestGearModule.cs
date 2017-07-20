using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class MainTestGearModule : MonoHandler
	{
		public Text pointText;

		void Awake(){
			pointText.text = "";
			Race.CreatePrimitive<Swordman> ((p1) => {
				p1.tag = Constants.PLAYER1;
				p1.name = Constants.PLAYER1;
				p1.DefaultValue ();
				p1.GetModule<GoldModule>(x => x.AddGold(99999999999));
				p1.GetModule<StatModule>(x => x.point = 1000);
			});

			AvailableBoughtItem.Init();
			GearUpgradedItems.Init ();
		}

		void Update(){
			Race.FindWithPlayerId (Constants.PLAYER1) [0].GetModule<StatModule> (x => {
				pointText.text = "Point " + Mathf.FloorToInt(x.point);
			});
		}
	}
}

