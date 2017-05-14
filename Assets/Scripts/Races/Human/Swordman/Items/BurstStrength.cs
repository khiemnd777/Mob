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
		
		public override string title {
			get {
				return "Burst strength";
			}
		}

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<BurstStrength> (own, targets);
		}
	}

	public class BurstStrengthBoughtItem: BoughtItem 
	{
		public override void Buy (Race who, float price, int quantity)
		{
			Buy<BurstStrengthItem> (who, price, quantity);
		}
	}
}

