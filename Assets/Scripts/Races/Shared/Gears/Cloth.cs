using UnityEngine;
using System.Collections;

namespace Mob
{
	public class Cloth : Affect
	{
		public float magicResist = 15f;

		public override void Init ()
		{
			gainPoint = 5f;
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.magicResist += magicResist);
		}

		public override void Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			magicResist *= 1.2f;
			Execute ();
			AddGainPoint ();
		}
	}

	public class ClothItem: GearItem, ISelfUsable {

		public override void Init ()
		{
			upgradePrice = 40f;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Cloth> (own))
				return false;

			Affect.CreatePrimitive<Cloth> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override void Upgrade (float price = 0)
		{
			++upgradeCount;
			if (EnoughGold (own, upgradePrice)) {
				Affect.HasAffect<Cloth> (own, (a) => {
					a.Upgrade();
					SubtractGold (own, upgradePrice);
					upgradePrice *= 1.2f;
				});
				GetRandomItem ();
			}
		}
	}

	public class ClothBoughtItem: BoughtItem {

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<ClothItem> (who, new Race[]{ who }, price);
		}
	}
}

