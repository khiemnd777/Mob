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

						// Subtract own energy
						SubtractEnergy(_energy);

						own.AllowAttack (false);
					} else if( turn > 4){
						Destroy(gameObject);
					}
				});
			});
		}
	}
}

