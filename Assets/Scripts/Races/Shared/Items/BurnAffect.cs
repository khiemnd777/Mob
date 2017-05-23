using UnityEngine;

namespace Mob
{
	public class BurnAffect : Affect, INegativeAffect
	{
		public float subtractHp = 15f;
		public int turnNumber = 3; 

		void Update(){
			foreach (var target in targets) {
				ExecuteInTurn(target, () => {
					PhysicalAttackCalculator.Calculate(subtractHp, own, targets);
					if(turn == turnNumber){
						Destroy(gameObject);
					}
				});	
			}
		}
	}

	public class BurnAffectItem: Item
	{
		public override string title {
			get {
				return "Burn affect";
			}
		}

		public float subtractHp = 15f;
		public int turnNumber = 3; 

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<BurnAffect> (own, targets, x => {
				x.subtractHp = subtractHp;
				x.turnNumber = turnNumber;
			});
			return true;
		}
	}

	public class BurnAffectBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "Burn affect";
			}
		}

		public float subtractHp = 15f;
		public int turnNumber = 3; 

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<BurnAffectItem> (who, price, quantity, x => {
				x.subtractHp = subtractHp;
				x.turnNumber = turnNumber;
			});
		}
	}
}