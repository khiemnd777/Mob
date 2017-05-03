using UnityEngine;
using System.Linq;

namespace Mob
{
	public abstract class Gear : Affect
	{
		public bool use;

		public abstract bool Upgrade ();
	}
}

