using UnityEngine;

namespace Mob
{
	public class SwordmanGearTier0 : Gear
	{
		void Start(){
			own.GetModule<StatModule> (s => {
				s.damage += 7f;
				s.resistance += 5f;
				s.technique += 5f;
				s.luck += 2f;
			});
		}

		public override bool Upgrade(){
			return EnoughGold (80f, () => {
				InstantiateFromMonoResource<SwordmanGearTier1> ("Races/Human/Swordman/Gears/Tier1");
				Destroy(gameObject);
			});
		}
	}
}

