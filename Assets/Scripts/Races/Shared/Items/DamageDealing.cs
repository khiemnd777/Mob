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
		public override void Init ()
		{
			title = "Deal " + damage + " damage";
		}

		public float damage;

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<DamageDealing> (own, targets, d => d.damage = damage);
			return true;
		}
	}

	public class DamageDealingBoughtItem: BoughtItem
	{
		public override void Init ()
		{
			title = "Deal " + damage + " damage";
		}

		public float damage;

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<DamageDealingItem> (who, price, quantity, x => x.damage = damage);
		}
	}
}

