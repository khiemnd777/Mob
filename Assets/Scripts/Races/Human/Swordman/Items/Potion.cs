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

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<Potion> (own, targets);
		}
	}
}

