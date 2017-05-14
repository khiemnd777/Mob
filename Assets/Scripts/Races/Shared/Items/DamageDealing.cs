using UnityEngine;

namespace Mob
{
	public class DamageDealing : Affect
	{
		public float damage;

		void Start(){
			foreach(var target in targets){
				target.GetModule<HealthPowerModule> (hp => hp.SubtractHp (damage));
			}
			Destroy (gameObject);
		}
	}

	public class DamageDealingItem: Item
	{
		public override string title {
			get {
				return "Deal " + damage + " damage";
			}
		}

		public float damage;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<DamageDealing> (own, targets, d => d.damage = damage);
		}
	}

	public class DamageDealingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "Deal " + damage + " damage";
			}
		}

		public float damage;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<DamageDealingItem> (who, price, quantity, x => x.damage = damage);
		}
	}
}

