using UnityEngine;

namespace Mob
{
	public class StunAffectAndDamageDealing : Affect
	{
		public int turnNumber = 1;
		public float damage = 30f;

		void Update(){
			foreach(var target in targets){
				ExecuteInTurn (target, () => {
					if(turn <= turnNumber){
						Affect.CreatePrimitive<DamageDealing>(own, new Race[]{target}, d => d.damage = damage);
						target.AllowAttack(false);
					} else {
						target.AllowAttack(true);
						Destroy(gameObject);
					}
				});	
			}
		}
	}

	public class StunAffectAndDamageDealingItem: Item
	{
		public override string title {
			get {
				return "Stun in" + turnNumber + " turn and take " + damage + " damage";
			}
		}

		public int turnNumber = 1;
		public float damage = 30f;

		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<StunAffectAndDamageDealing> (own, targets, x => {
				x.turnNumber = turnNumber;
				x.damage = damage;
			});
		}
	}

	public class StunAffectAndDamageDealingBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "Stun in" + turnNumber + " turn and take " + damage + " damage";
			}
		}

		public int turnNumber = 1;
		public float damage = 30f;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<StunAffectAndDamageDealingItem> (who, price, quantity, e => {
				e.damage = damage;
				e.turnNumber = turnNumber;
			});
		}
	}
}

