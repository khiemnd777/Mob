using UnityEngine;
using System;

namespace Mob
{
	public abstract class Item : MonoHandler
	{
		public int quantity;
		public Race own;
		public int usedTurn = 0;
		public int usedNumber = 0;
		public int upgradeCount;

		public float energy = 0f;
		public int level = 0;
		string _title;
		public string title { get { return _title ?? this.name; } set { _title = value; }}
		public string brief;
		public int cooldown = 0;
		public float upgradePrice = 0f;

		public virtual void Init(){
			
		}

		public abstract bool Use (Race[] targets);

		public virtual bool Disuse(){
			return false;	
		}

		public bool Use<T>(Race[] targets) where T: Affect {
			Affect.CreatePrimitive<T> (own, targets);
			SubtractEnergy ();
			return true;
		}

		public virtual void Upgrade(float price = 0f){
			
		}

		public void SubtractGold(Race who, float price = 0f, int quantity = 0){
			var p = price <= 0f ? this.upgradePrice : price;
			var q = quantity <= 0 ? this.quantity : quantity;
			who.GetModule<GoldModule> ((g) => {
				g.SubtractGold(p * q);
			});
		}

		public bool EnoughGold(Race who, float price = 0f, int quantity = 1, Action predicate = null){
			var enough = false;
			var p = price <= 0f ? this.upgradePrice : price;
			var q = quantity <= 0 ? this.quantity : quantity;

			who.GetModule<GoldModule> ((g) => {
				enough = g.gold >= p * q;
			});
			if (enough && predicate != null) {
				predicate.Invoke ();
			}
			return enough;
		}

		public bool EnoughLevel(Action predicate = null){
			var result = false;
			own.GetModule<LevelModule> (x => {
				result = x.level >= level;
			});
			if (result && predicate != null) {
				predicate.Invoke ();
			}
			return result;
		}

		public bool EnoughEnergy(Action predicate = null){
			var result = false;
			own.GetModule<EnergyModule> (x => {
				result = x.energy >= energy;
			});
			if (result && predicate != null) {
				predicate.Invoke ();
			}
			return result;
		}

		public bool EnoughCooldown(Action predicate = null){
			var result = cooldown == 0 || usedTurn == 0 || usedTurn + cooldown == own.turnNumber;
			if (result && predicate != null) {
				predicate.Invoke ();
			}
			return result;
		}

		public bool Enable(){
			return EnoughEnergy () && EnoughLevel () && EnoughCooldown ();
		}

		public void SubtractEnergy(float energy = 0f){
			own.GetModule<EnergyModule> ((e) => {
				e.SubtractEnergy (energy == 0f ? this.energy : energy);
			});
		}

		public static T Create<T>(string resource, Race own, int quantity, Action<T> predicate = null) where T : Item {
			var a = GetMonoResource<T> (resource);
			return Create<T> (a, own, quantity, predicate);
		}

		public static T Create<T>(T item, Race own, int quantity, Action<T> predicate = null) where T : Item {
			var a = Instantiate<T>(item);
			a.own = own;
			a.quantity = quantity;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			a.Init ();

			return a;
		}

		public static T CreatePrimitive<T>(Race own, int quantity, Action<T> predicate = null) where T: Item {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			a.own = own;
			a.quantity = quantity;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			a.Init ();

			return a;
		}
	}
}