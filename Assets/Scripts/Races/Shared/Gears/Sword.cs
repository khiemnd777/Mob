using UnityEngine;
using System.Collections;

namespace Mob
{
	public class Sword : Affect
	{
		public float damage = 20f;

		public override void Init ()
		{
			gainPoint = 6f;
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.physicalAttack += damage);
		}

		public override void Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			damage *= 1.2f;
			Execute ();
			AddGainPoint ();
		}
	}

	public class SwordItem: Item, ISelfUsable {

		public override void Init ()
		{
			upgradePrice = 50f;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Sword> (own))
				return false;

			Affect.CreatePrimitive<Sword> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override void Upgrade (float price = 0)
		{
			++upgradeCount;
			if (EnoughGold (own, upgradePrice)) {
				Affect.HasAffect<Sword> (own, (a) => {
					a.Upgrade();
					SubtractGold (own, upgradePrice);
					upgradePrice *= 1.2f;
				});
			}
		}
	}

	public class SwordBoughtItem: BoughtItem {

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<SwordItem> (who, new Race[]{ who }, price);
		}
	}
}

