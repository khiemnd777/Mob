using UnityEngine;

namespace Mob
{
	public class HeartOfHestia : Affect, ICriticalHandler, IAttackableAffect
	{
		void Start(){
//			HasAffect<HeartOfHestia> (own, () => {
//				Destroy(gameObject);
//			});
//			own.GetModule<StatModule>(s => s.damage += 10f);
		}

		#region IAttackableAffect implementation

		public void AssignAttackableAffect (Race target)
		{
			Affect.CreatePrimitive<BurnAffect> (own, new Race[]{ target }, b => {
				b.subtractHp = 10f;
			});
		}

		#endregion

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			return damage * 2.5f;
		}

		#endregion
	}

	public class HeartOfHestiaItem: Item
	{
		public override string title {
			get {
				return "Heart of Hestia";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<HeartOfHestia> (own, targets);
			return true;
		}
	}

	public class HeartOfHestiaBoughtItem: BoughtItem 
	{
		public override string title {
			get {
				return "Heart of Hestia";
			}
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<HeartOfHestiaItem> (who, price, quantity);
		}
	}
}

