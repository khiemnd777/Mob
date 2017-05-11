using UnityEngine;

namespace Mob
{
	public class StatDamageAdding : Affect
	{
		public float extraDamage;

		void Start(){
			own.GetModule<StatModule>(s => s.damage+=extraDamage);
			Destroy (gameObject);
		}
	}

	public class StatDamageAddingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StatDamageAdding> (own, targets);
		}	
	}
}

