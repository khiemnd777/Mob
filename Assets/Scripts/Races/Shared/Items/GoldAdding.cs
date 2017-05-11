using UnityEngine;

namespace Mob
{
	public class GoldAdding : Affect
	{
		public float extraGold;

		void Start(){
			own.GetModule<GoldModule>(g => g.AddGold(extraGold));	
			Destroy (gameObject);
		}
	}

	public class GoldAddingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<GoldAdding> (own, targets);
		}
	}
}