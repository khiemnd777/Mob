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
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatTechniqueAdding> (own, targets);
		}	
	}
}

