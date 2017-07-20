using System;

namespace Mob
{
	public class GearItem : Item
	{
		public override bool Use (Race[] targets)
		{
			return false;
		}
		
		public BoughtItem GetRandomItem(){
			switch (upgradeCount) {
			case 4:
				var item = GearUpgradedItems.GetIn (GearUpgradedItems.Case1 ());
				item.BuyAndUseImmediately (own, new[] { own });
				return item;
			case 9:
			case 10:
				item = GearUpgradedItems.GetIn (GearUpgradedItems.Case2 ());
				item.BuyAndUseImmediately (own, new[] { own });
				return item;
			default:
				break;
			}
			return null;
		}
	}
}

