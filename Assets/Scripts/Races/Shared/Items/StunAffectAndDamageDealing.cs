using UnityEngine;

namespace Mob
{
	public class StunAffectAndDamageDealing : Affect
	{
		public int turnNumber = 1;
		public float damage = 30f;

		void Update(){
			foreach(var target in targets){
				ExecuteInTurn (target, () => {
					if(turn <= turnNumber){
						Affect.CreatePrimitive<DamageDealing>(own, new Race[]{target}, d => d.damage = damage);
						target.AllowAttack(false);
					} else {
						target.AllowAttack(true);
						Destroy(gameObject);
					}
				});	
			}
		}
	}
}

