using UnityEngine;
using System.Collections;

namespace Mob
{
	public class Helm : Affect
	{
		public float hp = 50f;

		public override void Init ()
		{
			gainPoint = 5f;
		}

		public override void Execute ()
		{
			own.GetModule<HealthPowerModule>(x => x.AddHp(hp));
		}

		public override void Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			hp *= 1.2f;
			Execute ();
			AddGainPoint ();
		}
	}

	public class HelmItem: GearItem, ISelfUsable {
		
		public override void Init ()
		{
			upgradePrice = 40f;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Helm> (own))
				return false;
			
			Affect.CreatePrimitive<Helm> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override void Upgrade (float price = 0)
		{
			++upgradeCount;
			if (EnoughGold (own, upgradePrice)) {
				Affect.HasAffect<Helm> (own, (a) => {
					a.Upgrade ();
					SubtractGold (own, upgradePrice);
					upgradePrice *= 1.2f;
				});
				GetRandomItem ();
			}
		}
	}

	public class HelmBoughtItem: BoughtItem {
		
		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<HelmItem> (who, new Race[]{ who }, price);
		}
	}
}

