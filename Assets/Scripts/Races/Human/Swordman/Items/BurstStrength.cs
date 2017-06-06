using System;

namespace Mob
{
	public class BurstStrength : Affect, IAccurate
	{
		public bool use;

		public override float gainPoint {
			get {
				return 6f;
			}
		}

		void Start(){
			if(HasAffect<BurstStrength>(own)){
				RemoveAffect (own, this);
				return;
			}
		}

		void Update(){
			if(use){
				use = false;
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
	public class BurstStrengthItem: Item, ISelfUsable
	{	
		public override string title {
			get {
				return "Burst strength";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<BurstStrength> (own, targets);
			return true;
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

