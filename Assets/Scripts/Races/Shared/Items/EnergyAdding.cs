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
		public override string title {
			get {
				return "+" + extraEnergy + " energy";
			}
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
		public override string title {
			get {
				return "+" + extraEnergy + " energy";
			}
		}

		public float extraEnergy;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<EnergyAddingItem> (who, price, quantity, e => e.extraEnergy = extraEnergy);
		}
	}
}

