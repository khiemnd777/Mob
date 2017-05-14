using UnityEngine;

namespace Mob
{
	public class StatDamageAdding : Affect
	{
		public float extraDamage;

		void Start(){
			own.GetModule<StatModule>(s => s.damage+=extraDamage);
			Destroy (gameObject);
		}
	}

	public class StatDamageAddingItem: Item
	{
		public override string title {
			get {
				return "+" + extraDamage + " stat damage";
			}
		}

		public float extraDamage;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatDamageAdding> (own, targets, d => d.extraDamage = extraDamage);
		}	
	}

	public class StatDamageAddingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "+" + extraDamage + " stat damage";
			}
		}

		public float extraDamage;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<StatDamageAddingItem> (who, price, quantity, e => e.extraDamage = extraDamage);
		}
	}
}

