using System;

namespace Mob
{
	public class Speedy : Affect, IDodgeableChance
	{
		public bool use;

		public override float gainPoint {
			get {
				return 6f;
			}
		}

		void Start(){
			if(HasAffect<Speedy>(own)){
				RemoveAffect (own, this);
				return;
			}
		}

		void Update(){
			if (use) {
				use = false;
				Destroy (gameObject);
			}
		}

		#region IDodgeableChance implementation

		public float DodgeChance (float accuracy)
		{
			use = true;
			return AccuracyCalculator.GetAccuracyWithProbability (100f, accuracy);
		}

		#endregion
	}

	// Item
	public class SpeedyItem: Item, ISelfUsable
	{	
		public override string title {
			get {
				return "Speedy";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Speedy> (own, targets);
			return true;
		}
	}

	public class SpeedyBoughtItem: BoughtItem 
	{
		public override void Buy (Race who, float price, int quantity)
		{
			Buy<SpeedyItem> (who, price, quantity);
		}
	}
}

