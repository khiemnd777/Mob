using UnityEngine;

namespace Mob
{
	public abstract class RaceModule : MonoHandler
	{
		protected Race _race 
		{
			get 
			{
				return GetComponent<Race> ();
			}
		}

		public T GetModule<T>() where T : RaceModule
		{
			return GetComponent<T> ();	
		}
	}
}

