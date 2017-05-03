using UnityEngine;

namespace Mob
{
	public class Tier3 : Gear
	{
		void Start(){
			EnoughGold (280f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 50f;
					s.resistance += 24f;
					s.technique += 18f;
					s.luck += 8f;
				});
				AddGainPoint(140f);
				SubtractGold(280f);
			});
		}

		void Update(){
			ExecuteInTurn (own, () => {
				own.GetModule<HealthPowerModule>(hp => {
					hp.AddHp(10f);
				});
			});
		}

		public override bool Upgrade(){
			return false;
		}
	}
}

