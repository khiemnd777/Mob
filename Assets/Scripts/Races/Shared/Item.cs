using UnityEngine;

namespace Mob
{
	public abstract class Item : MonoHandler
	{
		public int quantity;
		public Race own;
		public float cooldown;
		public bool active;

		public void Use<T>(Race[] targets) where T: Affect {
			Affect.CreatePrimitive<T> (own, targets);
		}

		public abstract void Use (Race[] targets);

		public static T Create<T>(string resource, Race own, int quantity, float cooldown) where T : Item {
			var a = GetMonoResource<T> (resource);
			return Create<T> (a, own, quantity, cooldown);
		}

		public static T Create<T>(T item, Race own, int quantity, float cooldown) where T : Item {
			var a = Instantiate<T>(item);
			a.own = own;
			a.quantity = quantity;
			a.cooldown = cooldown;
			return a;
		}

		public static T CreatePrimitive<T>(Race own, int quantity, float cooldown) where T: Item {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			a.own = own;
			a.quantity = quantity;
			a.cooldown = cooldown;
			return a;
		}
	}
}

