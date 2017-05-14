using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class InventoryModule : RaceModule
	{
		public List<Item> items = new List<Item>();

		public void Add<T>(int quantity, Action<T> predicate = null) where T: Item{
			if (!items.Any (x => x.GetType().IsEqual<T> ())) {
				var item = Item.CreatePrimitive<T> (_race, quantity, predicate: predicate);
				items.Add (item);
				return;
			}
			items.FirstOrDefault (x => x.GetType().IsEqual<T> ()).quantity += quantity;
		}

		public void Use<T>(Race[] targets){
			if (!items.Any (x => x.GetType().IsEqual<T> ())) 
				return;
			var item = items.FirstOrDefault (x => x.GetType().IsEqual<T> ());

			item.Use(targets);
			--item.quantity;

			if (item.quantity == 0) {
				items.Remove(item);
				Destroy (item.gameObject);
			}
		}

		public void Use(Item item, Race[] targets){
			item.Use(targets);
			--item.quantity;

			if (item.quantity == 0) {
				items.Remove(item);
				Destroy (item.gameObject);
			}
		}
	}
}