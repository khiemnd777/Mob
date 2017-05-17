using UnityEngine;
using System;

namespace Mob
{
	public abstract class BoughtItem : MonoHandler
	{
		public int quantity = 1;

		public virtual string title { get { return name; } }

		public virtual string brief { get; }
		
		public abstract void Buy(Race who, float price, int quantity);

		public void SubtractGold(Race who, float price, int quantity){
			who.GetModule<GoldModule> ((g) => {
				g.SubtractGold(price * this.quantity == 0 ? quantity : this.quantity);
			});
		}

		public bool EnoughGold(Race who, float price, int quantity, Action predicate = null){
			var enough = false;
			who.GetModule<GoldModule> ((g) => {
				enough = g.gold >= price * (this.quantity == 0 ? quantity : this.quantity);
			});
			if (enough && predicate != null) {
				predicate.Invoke ();
			}
			return enough;
		}

		public void Buy<T>(Race who, float price, int quantity, Action<T> predicate = null) where T: Item{
			EnoughGold (who, price, quantity, () => {
				who.GetModule<BagModule>(i => i.Add<T>(quantity, predicate));
				SubtractGold(who, price, quantity);
			});
		}

		public void BuyAndUseImmediately<T>(Race who, Race[] targets, float price) where T: Item{
			EnoughGold (who, price, 1, () => {
				Item.CreatePrimitive<T>(who, 1, predicate: x => {
					x.Use(targets);
					Destroy(x.gameObject);
				});
				SubtractGold(who, price, 1);
			});
		}

		public static T CreatePrimitive<T>(Action<T> predicate = null) where T: BoughtItem {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			if (predicate != null) {
				predicate.Invoke (a);
			}
			return a;
		}
	}
}

