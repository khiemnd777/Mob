using UnityEngine;

namespace Mob
{
	public class StatTechniqueAdding : Affect
	{
		public float extraTechnique;

		void Start(){
			own.GetModule<StatModule>(s => s.technique+=extraTechnique);
			Destroy (gameObject);
		}
	}

	public class StatTechniqueAddingItem: Item
	{
		public override string title {
			get {
				return "+" + extraTechnique + " stat technique";
			}
		}

		public float extraTechnique;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatTechniqueAdding> (own, targets, x => x.extraTechnique = extraTechnique);
		}	
	}

	public class StatTechniqueAddingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "+" + extraTechnique + " stat technique";
			}
		}

		public float extraTechnique;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<StatTechniqueAddingItem> (who, price, quantity, e => e.extraTechnique = extraTechnique);
		}
	}
}

