using UnityEngine;

namespace Mob
{
	public class GoldAdding : Affect
	{
		public float extraGold;

		void Start(){
			own.GetModule<GoldModule>(g => g.AddGold(extraGold));	
			Destroy (gameObject);
		}
	}

	public class GoldAddingItem: Item
	{
		public override string title {
			get {
				return "+" + extraGold + " gold";
			}
		}

		public float extraGold;

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<GoldAdding> (own, targets, g => g.extraGold = extraGold);
			return true;
		}
	}

	public class GoldAddingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "+" + extraGold + " gold";
			}
		}

		public float extraGold;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<GoldAddingItem> (who, price, quantity, e => e.extraGold = extraGold);
		}
	}
}