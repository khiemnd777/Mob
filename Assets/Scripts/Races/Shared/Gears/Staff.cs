using UnityEngine;
using System.Collections;

namespace Mob
{
	public class Staff : Affect
	{
		public float magicAttack = 20f;

		public override void Init ()
		{
			gainPoint = 6f;
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.magicAttack += magicAttack);
		}

		public override void Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			magicAttack *= 1.2f;
			Execute ();
			AddGainPoint ();
		}
	}

	public class StaffItem: GearItem, ISelfUsable {

		public override void Init ()
		{
			upgradePrice = 50f;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Staff> (own))
				return false;

			Affect.CreatePrimitive<Staff> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override void Upgrade (float price = 0)
		{
			++upgradeCount;
			if (EnoughGold (own, upgradePrice)) {
				Affect.HasAffect<Staff> (own, (a) => {
					a.Upgrade();
					SubtractGold (own, upgradePrice);
					upgradePrice *= 1.2f;
				});
				GetRandomItem ();
			}
		}
	}

	public class StaffBoughtItem: BoughtItem {

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<StaffItem> (who, new Race[]{ who }, price);
		}
	}
}

