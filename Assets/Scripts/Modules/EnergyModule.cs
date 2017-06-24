using UnityEngine;
using UnityEngine.UI;

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

		public float energyLabel;

		public void AddEnergy (float e)
		{
			_energy = Mathf.Min (_energy + e, maxEnergy);
			While ((inc, step) => {
				energyLabel = Mathf.Min(energyLabel + inc, maxEnergy);
			}, e, 1f);
		}

		public void SubtractEnergy (float e)
		{
			_energy = Mathf.Max (_energy - e, 0f);
			While ((inc, step) => {
				energyLabel = Mathf.Max(energyLabel - inc, 0f);
			}, e, 1f);

			var energyValue = GetMonoComponent<Text> (Constants.ATTACKER_ENERGY_LABEL);
			if (energyValue != null) {
				JumpEffectAndShowSubLabel (energyValue.transform, Constants.DECREASE_LABEL, e);
			}
		}
	}
}

