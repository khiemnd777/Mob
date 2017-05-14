using UnityEngine;

namespace Mob
{
	public class Distract : Affect
	{
		int _level = 8;
		float _energy = 3f;
		float _gainPoint = 5f;

		void Update(){
			EnoughLevel (_level, () => {
				ExecuteInTurn(own, () => {
					if(turn == 1){
						AddGainPoint(_gainPoint);

						own.AllowAttack (false);
					} else if( turn > 4) {
						Destroy(gameObject);
					}
				});
			});
		}
	}

	// Item
	public class DistractSkill: Skill {

		public override int level {
			get {
				return 8;
			}
		}

		public override float energy {
			get {
				return 3f;
			}
		}

		public override string title {
			get {
				return "Distract";
			}
		}

		public override void Use (Race[] targets)
		{
			if (EnoughLevel () && EnoughEnergy ()) {
				Affect.CreatePrimitive<Distract> (own, targets);
				SubtractEnergy();
			}
		}
	}
}

