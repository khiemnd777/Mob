using UnityEngine;

namespace Mob
{
	public class Tier2 : Gear
	{
		void Start(){
			EnoughGold (160f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 25f;
					s.resistance += 16f;
					s.technique += 12f;
					s.luck += 6f;
				});
				AddGainPoint(80f);
				SubtractGold(160f);
			});
		}

		public override bool Upgrade(){
			return EnoughGold (280f, () => {
				InstantiateFromMonoResource<Tier3> ("Races/Human/Swordman/Gears/Tier3");
				Destroy(gameObject);
			});
		}
	}
}

