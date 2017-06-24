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
		public override void Init ()
		{
			title = "+" + extraEnergy + " energy";
		}

		public float extraEnergy;

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<EnergyAdding> (own, targets, e => e.extraEnergy = extraEnergy);
			return true;
		}	
	}

	public class EnergyAddingBoughtItem: BoughtItem
	{
		public override void Init ()
		{
			title = "+" + extraEnergy + " energy";
		}

		public float extraEnergy;

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<EnergyAddingItem> (who, price, quantity, e => e.extraEnergy = extraEnergy);
		}
	}
}

