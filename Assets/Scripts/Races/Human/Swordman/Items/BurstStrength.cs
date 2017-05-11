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
			AddGainPoint (6f);
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

	// Item
	public class BurstStrengthItem: Item {

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<BurstStrength> (own, targets);
		}
	}
}

