using System;

namespace Mob
{
	public class Speedy : Affect, IDodgeableChance
	{
		public bool use;

		void Start(){
			if(HasAffect<Speedy>(own)){
				Destroy (gameObject);
				return;
			}
			AddGainPoint (6f);
		}

		void Update(){
			if (use) {
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
	public class SpeedyItem: Item {
		
		public override string title {
			get {
				return "Speedy";
			}
		}

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<Speedy> (own, targets);
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

