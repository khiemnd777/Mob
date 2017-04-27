using UnityEngine;

namespace Mob
{
	public class EnergyModule : RaceModule
	{
		public float maxEnergy;

		float _energy;

		public float energy {
			get{
				return _energy;
			}
		}

		public void AddEnergy (float e)
		{
			_energy = Mathf.Min (_energy + e, maxEnergy);
		}

		public void SubtractEnergy (float e)
		{
			_energy = Mathf.Max (_energy - e, 0f);
		}
	}
}

