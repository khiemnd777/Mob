using System;

namespace Mob
{
	public class Ring : Affect
	{
		public override void Init ()
		{
			gainPoint = 6f;
		}

		float point = 1f;

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => {
				x.strength += point;
				x.dexterity += point;
				x.intelligent += point;
				x.vitality += point;
				x.luck += point;
			});
		}

		public override void Upgrade ()
		{
			++upgradeCount;
			if (upgradeCount == 1) {
				gainPoint = 8f;
				point = 3f;
			}
			if (upgradeCount == 2) {
				gainPoint = 12f;
				point = 6f;
			}
			Execute ();
			AddGainPoint ();
		}
	}

	public class RingItem: Item, ISelfUsable {
		
		public override void Init ()
		{
			upgradePrice = 50f;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Ring> (own))
				return false;

			Affect.CreatePrimitive<Ring> (own, targets);
			upgradePrice = 80f;
			return true;
		}

		public override void Upgrade (float price = 0)
		{
			++upgradeCount;
			if (EnoughGold (own, upgradePrice)) {
				Affect.HasAffect<Ring> (own, (a) => {
					a.Upgrade();
					SubtractGold (own, upgradePrice);
					if(upgradeCount == 1){
						upgradePrice = 120f;
					}
				});
			}
		}
	}
}

