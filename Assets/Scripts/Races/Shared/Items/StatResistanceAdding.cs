using UnityEngine;

namespace Mob
{
	public class StatResistanceAdding : Affect
	{
		public float extraResistance;

		void Start(){
			own.GetModule<StatModule>(s => s.resistance+=extraResistance);
			Destroy (gameObject);
		}
	}

	public class StatResistanceAddingItem: Item
	{
		public override string title {
			get {
				return "+" + extraResistance + " stat resistance";
			}
		}

		public float extraResistance;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatResistanceAdding> (own, targets, x => x.extraResistance = extraResistance);
		}	
	}

	public class StatResistanceAddingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "+" + extraResistance + " stat resistance";
			}
		}

		public float extraResistance;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<StatResistanceAddingItem> (who, price, quantity, e => e.extraResistance = extraResistance);
		}
	}
}

