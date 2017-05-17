using System;

namespace Mob
{
	public class GreatPotion : Affect
	{
		void Start(){
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, predicate: hp => hp.extraHp = 150f);
			AddGainPoint (8f);
			Destroy(gameObject);
		}
	}

	// Item
	public class GreatPotionItem: Item {
		
		public override string title {
			get {
				return "Great potion";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<GreatPotion> (own, targets);
			return true;
		}
	}

	public class GreatPotionBoughtItem: BoughtItem 
	{
		public override void Buy (Race who, float price, int quantity)
		{
			Buy<GreatPotionItem> (who, price, quantity);
		}
	}
}

