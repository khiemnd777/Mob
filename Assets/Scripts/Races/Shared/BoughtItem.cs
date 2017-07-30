using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public abstract class BoughtItem : MonoHandler
	{
		public Race own;

		public Dictionary<string, Sprite> icons = new Dictionary<string, Sprite>();

		public int quantity = 1;

		public float price = 0f;

		string _title;

		public string title { get { return _title??this.name; } set { _title = value; } }

		public string brief;

		public virtual void Init(){
			
		}

		public virtual Sprite GetIcon(string key, Func<bool> predicate){
			return predicate != null && predicate.Invoke () ? icons [key] : null;
		}

		public virtual Sprite GetIcon(string key){
			return icons.ContainsKey(key) ? icons [key] : null;
		}

		public virtual Sprite GetIcon(){
			return icons.Count > 0 ? icons.FirstOrDefault().Value : null;
		}

		public virtual void Buy(Race who, float price = 0f, int quantity = 0){
			
		}

		public virtual void BuyAndUseImmediately(Race who, Race[] targets, float price = 0f){

		}

		public virtual void Pick(Race who, int quantity = 0){
			
		}

		public void SubtractGold(Race who, float price = 0f, int quantity = 0){
			var p = price <= 0f ? this.price : price;
			var q = quantity <= 0 ? this.quantity : quantity;
			who.GetModule<GoldModule> ((g) => {
				g.SubtractGold(p * q);
			});
		}

		public bool EnoughGold(Race who, float price = 0f, int quantity = 1, Action predicate = null){
			var enough = false;
			var p = price <= 0f ? this.price : price;
			var q = quantity <= 0 ? this.quantity : quantity;

			who.GetModule<GoldModule> ((g) => {
				enough = g.gold >= p * q;
			});
			if (enough && predicate != null) {
				predicate.Invoke ();
			}
			return enough;
		}

		public void Buy<T>(Race who, float price = 0f, int quantity = 0, Action<T> predicate = null, Action postBuying = null) where T: Item{
			EnoughGold (who, price, quantity, () => {
				who.GetModule<BagModule>(i => i.Add<T>(quantity, predicate));
				SubtractGold(who, price, quantity);
				if(postBuying != null)
					postBuying.Invoke();
			});
		}

		public void BuyAndUseImmediately<T>(Race who, Race[] targets, float price = 0f, Action<T> predicate = null, Action postBuying = null) where T: Item{
			EnoughGold (who, price, 1, () => {
				Item.CreatePrimitive<T>(who, 1, predicate: x => {
					if(predicate != null){
						predicate.Invoke(x);
					}
					x.Use(targets);
					x.FlushAll();
				});
				SubtractGold(who, price, 1);
				if(postBuying != null)
					postBuying.Invoke();
			});
			FlushAll ();
		}

		public static T CreatePrimitive<T>(Action<T> predicate = null) where T: BoughtItem {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			if (predicate != null) {
				predicate.Invoke (a);
			}
			a.Init ();
			a.StartCoroutine (a.Interacting (a.gameObject));

			return a;
		}

		public static T CreatePrimitiveWithOwn<T>(Race own, Action<T> predicate = null) where T: BoughtItem {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			a.own = own;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			a.Init ();
			a.StartCoroutine (a.Interacting (a.gameObject));

			return a;
		}
	}
}

