using System;

namespace Mob
{
	public class Potion : Affect
	{
		public override void Init ()
		{
			gainPoint = 5f;
		}

		void Start(){
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, hp => hp.extraHp = 50f);
			Destroy(gameObject, Constants.WAIT_FOR_DESTROY);
		}
	}

	// Item
	public class PotionItem: Item, ISelfUsable {
		
		public override void Init ()
		{
			title = "Potion";
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Potion> (own, targets);
			return true;
		}
	}

	public class PotionBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Potion";
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<PotionItem> (who, price, quantity);
		}
	}
}

