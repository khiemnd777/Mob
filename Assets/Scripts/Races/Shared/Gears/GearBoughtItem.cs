using System;
using System.Linq;

namespace Mob
{
	public abstract class GearBoughtItem : BoughtItem
	{
		public GearType gearType;
		public InStoreState inStoreState;

		public void AlternateInStoreState(){
			var boughtItems = GearAvailableItems.list;
			foreach (var boughtItem in boughtItems) {
				if (boughtItem.gearType != gearType)
					continue;
				boughtItem.inStoreState = InStoreState.Available;
			}
			inStoreState = InStoreState.Bought;
		}
	}
}

