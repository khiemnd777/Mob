using System;

namespace Mob
{
	public class BurstStrength : Affect, IAccurate
	{
		public bool use;

		void Start(){
			if(HasAffect<BurstStrength>(own)){
				Destroy (gameObject);
				return;
			}
			EnoughGold (40f, () => {
				AddGainPoint (6f);
				SubtractGold (40f);
			});
		}

		void Update(){
			if(use){
				Destroy(gameObject);
			}
		}

		#region IAccurate implementation

		public float HandleAccuracy (Race target)
		{
			use = true;
			return 1f;
		}

		#endregion
	}
}

