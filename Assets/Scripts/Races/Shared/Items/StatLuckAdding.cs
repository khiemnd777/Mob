using UnityEngine;

namespace Mob
{
	public class StatLuckAdding : Affect
	{
		public float extraLuck;

		void Start(){
			own.GetModule<StatModule>(s => s.luck+=extraLuck);
			Destroy (gameObject);
		}
	}

	public class StatLuckAddingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatLuckAdding> (own, targets);
		}	
	}
}

