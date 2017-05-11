using UnityEngine;

namespace Mob
{
	public class EnergyAdding : Affect
	{
		public float extraEnergy;

		void Start(){
			own.GetModule<EnergyModule>(e => e.AddEnergy(extraEnergy));
			Destroy (gameObject);
		}
	}

	public class EnergyAddingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<EnergyAdding> (own, targets);
		}	
	}
}

