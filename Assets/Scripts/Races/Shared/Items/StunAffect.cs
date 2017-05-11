using System;

namespace Mob
{
	public class StunAffect : Affect, INegativeAffect
	{
		public int turnNumber = 1;

		void Update(){
			foreach(var target in targets){
				ExecuteInTurn (target, () => {
					if(turn <= turnNumber){
						target.AllowAttack(false);
					} else {
						target.AllowAttack(true);
						Destroy(gameObject);
					}
				});	
			}
		}
	}

	public class StunAffectItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StunAffect> (own, targets);
		}
	}
}