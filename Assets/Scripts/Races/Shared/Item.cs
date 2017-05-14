using UnityEngine;
using System;

namespace Mob
{
	public abstract class Item : MonoHandler
	{
		public int quantity;
		public Race own;
		public float cooldown;
		public bool active;

		public virtual float energy { get; }
		public virtual int level { get; }
		public virtual string title{ get { return this.name; } }
		public virtual string brief{ get; }

		public void Use<T>(Race[] targets) where T: Affect {
			Affect.CreatePrimitive<T> (own, targets);
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

		protected void SubtractEnergy(float energy = 0f){
			own.GetModule<EnergyModule> ((e) => {
				e.SubtractEnergy (energy == 0f ? this.energy : energy);
			});
		}

		public abstract void Use (Race[] targets);

		public static T Create<T>(string resource, Race own, int quantity, float cooldown = 0f, Action<T> predicate = null) where T : Item {
			var a = GetMonoResource<T> (resource);
			return Create<T> (a, own, quantity, cooldown, predicate);
		}

		public static T Create<T>(T item, Race own, int quantity, float cooldown = 0f, Action<T> predicate = null) where T : Item {
			var a = Instantiate<T>(item);
			a.own = own;
			a.quantity = quantity;
			a.cooldown = cooldown;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			return a;
		}

		public static T CreatePrimitive<T>(Race own, int quantity, float cooldown = 0f, Action<T> predicate = null) where T: Item {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			a.own = own;
			a.quantity = quantity;
			a.cooldown = cooldown;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			return a;
		}
	}
}

