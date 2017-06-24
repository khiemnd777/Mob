using System;

namespace Mob
{
	public class GreatPotion : Affect
	{
		public override void Init ()
		{
			gainPoint = 8f;
		}

		void Start(){
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, predicate: hp => hp.extraHp = 150f);
			Destroy(gameObject, Constants.WAIT_FOR_DESTROY);
		}
	}

	// Item
	public class GreatPotionItem: Item, ISelfUsable
	{
		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<GreatPotion> (own, targets);
			return true;
		}
	}

	public class GreatPotionBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Great potion";
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<GreatPotionItem> (who, price, quantity);
		}
	}
}

