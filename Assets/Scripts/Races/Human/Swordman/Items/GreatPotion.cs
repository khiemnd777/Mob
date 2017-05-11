using System;

namespace Mob
{
	public class GreatPotion : Affect
	{
		void Start(){
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, hp => hp.extraHp = 150f);
			AddGainPoint (8f);
			Destroy(gameObject);
		}
	}

	// Item
	public class GreatPotionItem: Item {

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<GreatPotion> (own, targets);
		}
	}
}

