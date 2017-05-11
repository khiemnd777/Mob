using UnityEngine;

namespace Mob
{
	public class DamageDealing : Affect
	{
		public float damage;

		void Start(){
			foreach(var target in targets){
				target.GetModule<HealthPowerModule> (hp => hp.SubtractHp (damage));
			}
			Destroy (gameObject);
		}
	}

	public class DamageDealingItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<DamageDealing> (own, targets);
		}
	}
}

