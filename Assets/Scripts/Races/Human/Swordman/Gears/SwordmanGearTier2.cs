using UnityEngine;

namespace Mob
{
	public class SwordmanGearTier2 : Gear, IMissingHandler, IAssignableDamage
	{
		void Start(){
			own.GetModule<StatModule> (s => {
				s.damage += 25f;
				s.resistance += 16f;
				s.technique += 12f;
				s.luck += 6f;
			});
			AddGainPoint(80f);
//			EnoughGold (160f, () => {
//				
//				SubtractGold(160f);
//			});
		}

		public override bool Upgrade(){
			return EnoughGold (280f, () => {
				InstantiateFromMonoResource<SwordmanGearTier1> ("Races/Human/Swordman/Gears/Tier3");
				Destroy(gameObject);
			});
		}

		#region IMissingHandler implementation

		public void HandleMissing (float accuracy, float damage, Race target)
		{
			this.own.GetModule<StatModule> (stat => stat.technique += 1f);
		}

		#endregion

		#region IAssignableDamage implementation

		public float AssignDamage ()
		{
			return 120f;
		}

		#endregion
	}
}

