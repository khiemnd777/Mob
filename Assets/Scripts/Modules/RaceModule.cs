using UnityEngine;

namespace Mob
{
	public abstract class RaceModule : MonoHandler
	{
		protected Race _class 
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

