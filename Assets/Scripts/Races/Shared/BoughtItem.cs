﻿using UnityEngine;
using System;

namespace Mob
{
	public abstract class BoughtItem : MonoHandler
	{
		public int quantity = 1;

		public float price = 0f;

		public string title;

		public string brief;

		public virtual void Init(){
			
		}
		
		public virtual void Buy(Race who, float price = 0f, int quantity = 0){
			
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

		public void Buy<T>(Race who, float price = 0f, int quantity = 0, Action<T> predicate = null) where T: Item{
			EnoughGold (who, price, quantity, () => {
				who.GetModule<BagModule>(i => i.Add<T>(quantity, predicate));
				SubtractGold(who, price, quantity);
			});
		}

		public void BuyAndUseImmediately<T>(Race who, Race[] targets, float price = 0f) where T: Item{
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
			a.Init ();
			return a;
		}
	}
}
