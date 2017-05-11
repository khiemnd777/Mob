using UnityEngine;

namespace Mob
{
	public class SwordmanGearTier1 : Gear, IAssignableDamage
	{
		void Start(){
			own.GetModule<StatModule> (s => {
				s.damage += 15f;
				s.resistance += 10f;
				s.technique += 8f;
				s.luck += 4f;
			});
			AddGainPoint(40f);
//			EnoughGold (80f, () => {
//				
//				SubtractGold(80f);
//			});
		}

		public override bool Upgrade(){
			return EnoughGold (160f, () => {
				InstantiateFromMonoResource<SwordmanGearTier1> ("Races/Human/Swordman/Gears/Tier2");
				Destroy(gameObject);
			});
		}

		#region IAssignableDamage implementation

		public float AssignDamage ()
		{
			return 120f;
		}

		#endregion
	}
}

