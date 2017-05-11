using UnityEngine;

namespace Mob
{
	public class BlessingOfAmphitrite: Affect, IMissingHandler
	{
		void Start(){
			HasAffect<BlessingOfAmphitrite> (own, () => {
				Destroy(gameObject);
			});
			own.GetModule<StatModule>(s => s.technique += 10f);
		}

		#region IMissingHandler implementation

		public void HandleMissing (float accuracy, float damage, Race target)
		{
			target.GetModule<HealthPowerModule>(hp => hp.SubtractHp(damage * .5f));
		}

		#endregion
	}

	public class BlessingOfAmphitriteItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<BlessingOfAmphitrite> (own, targets);
		}
	}
}