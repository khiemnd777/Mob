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
		public override void Init ()
		{
			title = "Dissolve all negative affects";
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<NegativeAffectDissolving> (own, targets);
			return true;
		}
	}

	public class NegativeAffectDissolvingBoughtItem: BoughtItem
	{
		public override void Init ()
		{
			title = "Dissolve all negative affects";
		}

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<NegativeAffectDissolvingItem> (who, price, quantity);
		}
	}
}

