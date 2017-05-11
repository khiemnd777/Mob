using UnityEngine;

namespace Mob
{
	public class BurnAffect : Affect
	{
		public float subtractHp = 15f;
		public int turnNumber = 3; 

		void Update(){
			foreach (var target in targets) {
				ExecuteInTurn(target, () => {
					target.GetModule<HealthPowerModule>(hp => hp.SubtractHp(subtractHp));
					if(turn == turnNumber){
						Destroy(gameObject);
					}
				});	
			}
		}
	}

	public class BurnAffectItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<BurnAffect> (own, targets);
		}
	}
}

