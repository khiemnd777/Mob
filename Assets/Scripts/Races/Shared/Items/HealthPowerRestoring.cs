using UnityEngine;

namespace Mob
{
	public class HealthPowerRestoring : Affect
	{
		public float extraHp;

		void Start(){
			own.GetModule<HealthPowerModule>(hp => {
				var _ = float.MinValue;
				own.GetModule<AffectModule>(am => {
					am.GetSubAffects<IRestorableHealthPower>(a => {
						_ = Mathf.Max(_, a.RestoreHealthPower(extraHp));
					});
				});
				extraHp = Mathf.Max (_, extraHp);
				hp.AddHp(extraHp);
			});

			Destroy (gameObject);
		}
	}

	public class HealthPowerRestoringItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets);
		}
	}
}

