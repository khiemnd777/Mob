using System;

namespace Mob
{
	public class Potion : Affect
	{
		void Start(){
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, hp => hp.extraHp = 50f);
			AddGainPoint (5f);
			Destroy(gameObject);
		}
	}

	// Item
	public class PotionItem: Item {
		public override string title {
			get {
				return "Potion";
			}
		}

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<Potion> (own, targets);
		}
	}

	public class PotionBoughtItem: BoughtItem 
	{
		public override void Buy (Race who, float price, int quantity)
		{
			Buy<PotionItem> (who, price, quantity);
		}
	}
}

