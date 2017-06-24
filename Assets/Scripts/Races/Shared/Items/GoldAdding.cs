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
		public override void Init ()
		{
			title = "+" + extraGold + " gold";
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
		public override void Init ()
		{
			title = "+" + extraGold + " gold";
		}

		public float extraGold;

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<GoldAddingItem> (who, price, quantity, e => e.extraGold = extraGold);
		}
	}
}