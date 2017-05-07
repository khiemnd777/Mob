using UnityEngine;

namespace Mob
{
	public class SwordmanAura2 : Gear, ICriticalHandler
	{
		void Start(){
			EnoughGold (180f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 15f;
					s.resistance += 15f;
					s.technique += 15f;
					s.luck += 15f;
				});
				AddGainPoint(80f);
				SubtractGold(180f);
			});
		}

		public override bool Upgrade ()
		{
			return false;
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, float accuracy, Race target)
		{
			own.GetModule<StatModule> (stat => stat.damage += 1f);
			return damage * 2f;
		}

		#endregion
	}
}

