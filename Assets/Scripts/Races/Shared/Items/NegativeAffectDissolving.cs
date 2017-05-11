using UnityEngine;

namespace Mob
{
	public class NegativeAffectDissolving : Affect
	{
		void Start(){
			own.GetModule<AffectModule>((a) => {
				a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
			});

			Destroy (gameObject);
		}
	}

	public class NegativeAffectDissolvingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<NegativeAffectDissolving> (own, targets);
		}
	}
}

