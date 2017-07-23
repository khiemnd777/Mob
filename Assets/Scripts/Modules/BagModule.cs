using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class BagModule : RaceModule
	{
		public List<Item> items = new List<Item>();

		public void Add<T>(int quantity, Action<T> predicate = null) where T: Item{
			if (!items.Any (x => x.GetType().IsEqual<T> ())) {
				var item = Item.CreatePrimitive<T> (_race, quantity, predicate);
				items.Add (item);
				return;
			}
			items.FirstOrDefault (x => x.GetType().IsEqual<T> ()).quantity += quantity;
		}

		public void Use<T>(Race[] targets){
			if (!items.Any (x => x.GetType().IsEqual<T> ())) 
				return;
			var item = items.FirstOrDefault (x => x.GetType().IsEqual<T> ());
			Use (item, targets);
		}

		public void Use(Item item, Race[] targets){
			var t = targets;
			if (typeof(ISelfUsable).IsAssignableFrom (item.GetType ())) {
				t = new Race[]{ item.own };
			} else if (typeof(ITargetUsable).IsAssignableFrom (item.GetType ())) {
				t = _race.targets;
			}
			item.Use (t);
			item.usedTurn = _race.turnNumber;
			--item.quantity;

			if (item.quantity == 0) {
				items.Remove (item);
				DestroyImmediate (item.gameObject);
				EventManager.TriggerEvent (Constants.EVENT_ITEM_OVER_IN_BAG);
			}
		}
	}
}