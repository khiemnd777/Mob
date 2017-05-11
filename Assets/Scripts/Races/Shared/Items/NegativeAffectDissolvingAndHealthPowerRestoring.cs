using UnityEngine;

namespace Mob
{
	public class NegativeAffectDissolvingAndHealthPowerRestoring : Affect
	{
		public float extraHp = 50f;

		void Start(){
			own.GetModule<AffectModule>((a) => {
				a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
			});
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, hp => hp.extraHp = extraHp);
			Destroy (gameObject);
		}
	}

	public class NegativeAffectDissolvingAndHealthPowerRestoringItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<NegativeAffectDissolvingAndHealthPowerRestoring> (own, targets);
		}
	}
}

