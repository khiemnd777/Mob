using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class InventoryModule : RaceModule
	{
		public List<Item> items;

		void Start(){
			items = new List<Item>();
		}

		public void Add<T>(int quantity, float cooldown = .0f) where T: Item{
			if (!items.Any (x => x.GetType().IsEqual<T> ())) {
				items.Add (Item.CreatePrimitive<T> (_race, quantity, cooldown));
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
	}
}