using UnityEngine;

namespace Mob
{
	public class SwordmanGearTier3 : Gear, IMissingHandler, IAssignableDamage
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

		#region IMissingHandler implementation

		public void HandleMissing (float accuracy, Race target)
		{
			own.GetModule<StatModule> (stat => stat.technique += 1f);
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

