using UnityEngine;

namespace Mob
{
	public class HeartOfHestia : Affect, ICriticalHandler, IAttackableAffect
	{
		void Start(){
			HasAffect<HeartOfHestia> (own, () => {
				Destroy(gameObject);
			});
			own.GetModule<StatModule>(s => s.damage += 10f);
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

		public float HandleCriticalDamage (float damage, float accuracy, Race target)
		{
			return damage * 2.5f;
		}

		#endregion
	}

	public class HeartOfHestiaItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<HeartOfHestia> (own, targets);
		}
	}
}

