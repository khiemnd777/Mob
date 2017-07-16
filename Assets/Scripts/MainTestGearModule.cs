using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class MainTestGearModule : MonoHandler
	{
		public Text goldText;

		void Awake(){
			goldText.text = "";
			Race.CreatePrimitive<Swordman> ((p1) => {
				p1.tag = Constants.PLAYER1;
				p1.name = Constants.PLAYER1;
				p1.DefaultValue ();
				p1.GetModule<GoldModule>(x=>x.AddGold(9999));
			});

			AvailableBoughtItem.Init();
		}

		void Update(){
			Race.FindWithPlayerId (Constants.PLAYER1) [0].GetModule<GoldModule> (x => {
				goldText.text = Mathf.FloorToInt(x.gold) + "g";
			});
		}
	}
}

