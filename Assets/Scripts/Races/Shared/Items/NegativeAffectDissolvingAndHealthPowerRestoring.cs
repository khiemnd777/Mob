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
		public override string title {
			get {
				return "Dissolve negative affects and restore " + extraHp + " HP";
			}
		}

		public float extraHp = 50f;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<NegativeAffectDissolvingAndHealthPowerRestoring> (own, targets, n => n.extraHp = extraHp);
		}
	}

	public class NegativeAffectDissolvingAndHealthPowerRestoringBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "Dissolve negative affects and restore " + extraHp + " HP";
			}
		}

		public float extraHp = 50f;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<NegativeAffectDissolvingAndHealthPowerRestoringItem> (who, price, quantity, e => e.extraHp = extraHp);
		}
	}
}

