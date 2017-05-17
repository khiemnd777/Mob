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
		public override string title {
			get {
				return "Dissolve all negative affects";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<NegativeAffectDissolving> (own, targets);
			return true;
		}
	}

	public class NegativeAffectDissolvingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "Dissolve all negative affects";
			}
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<NegativeAffectDissolvingItem> (who, price, quantity);
		}
	}
}

