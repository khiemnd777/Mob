using UnityEngine;

namespace Mob
{
	public class StatResistanceAdding : Affect
	{
		public float extraResistance;

		void Start(){
			own.GetModule<StatModule>(s => s.resistance+=extraResistance);
			Destroy (gameObject);
		}
	}

	public class StatResistanceAddingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatResistanceAdding> (own, targets);
		}	
	}
}

