using UnityEngine;

namespace Mob
{
	public class HolyKnight : Affect
	{
		int _level = 16;
		float _energy = 12f;
		float _gainPoint = 40f;

		void Update ()
		{
			EnoughLevel (_level, () => {
				ExecuteInTurn (own, () => {
					var stat = own.GetModule<StatModule> ();
					if (turn == 1) {
						// increase 5pt for all stats
						stat.damage += 5f;
						stat.resistance += 5f;
						stat.technique += 5f;
						stat.luck += 5f;
						// Add gain point
						AddGainPoint(_gainPoint);

						// Subtract own energy
						SubtractEnergy(_energy);

					} else if (turn > 3) {
						// decrease 5pt for all stats after using in 3 times
						stat.damage -= 5f;
						stat.resistance -= 5f;
						stat.technique -= 5f;
						stat.luck -= 5f;

						Destroy (gameObject);
						return;
					}
				});	
			});
		}
		
	}
}

