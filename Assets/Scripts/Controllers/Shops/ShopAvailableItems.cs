using System.Collections.Generic;

namespace Mob
{
	public class ShopAvailableItems
	{
		public static List<BoughtItem> list = new List<BoughtItem> ();

		public static void Init(){
			list.Add(BoughtItem.CreatePrimitive<PotionBoughtItem>(x => x.quantity = 99));
			list.Add(BoughtItem.CreatePrimitive<GreatPotionBoughtItem>(x => x.quantity = 99));
			list.Add(BoughtItem.CreatePrimitive<AntidoteBoughtItem>(x => x.quantity = 99));
			list.Add(BoughtItem.CreatePrimitive<BurstStrengthBoughtItem>(x => x.quantity = 99));
			list.Add(BoughtItem.CreatePrimitive<SpeedyBoughtItem>(x => x.quantity = 99));
		}
	}
}

