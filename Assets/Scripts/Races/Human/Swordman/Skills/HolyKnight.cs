using UnityEngine;

namespace Mob
{
	public class HolyKnight : Affect, IAccurate
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
		
		#region IAccurate implementation
		public float HandleAccuracy (Race target)
		{
			return 1f;
		}
		#endregion
	}

	// Item
	public class HolyKnightSkill: Skill {
		public override string title {
			get {
				return "Holy knight";
			}
		}

		public override int level {
			get {
				return 16;
			}
		}

		public override float energy {
			get {
				return 12f;
			}
		}

		public override void Use (Race[] targets)
		{
			if (EnoughLevel () && EnoughEnergy ()) {
				Affect.CreatePrimitive<HolyKnight> (own, targets);
				SubtractEnergy();
			}
		}
	}
}

