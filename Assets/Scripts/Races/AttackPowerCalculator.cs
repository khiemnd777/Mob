using UnityEngine;

namespace Mob
{
	public class AttackPowerCalculator
	{
		public static float TakeDamage(float attackerDamage, float targetResistance){
			return attackerDamage * attackerDamage / (attackerDamage + targetResistance);
		}

		public static void HandleDamage(ref float damage, Race own, Race target){
			var _ = float.MinValue;
			own.GetModule<AffectModule>(am => {
				am.GetSubAffects<IDamaged>(a => {
					_ = Mathf.Max(_, a.HandleDamage(target));
				});
			});
			damage = Mathf.Max (_, damage);
		}

		public static void ExtraAttackableAffect(Race own, Race target){
			own.GetModule<AffectModule>(am => {
				am.GetSubAffects<IAttackableAffect>(a => {
					a.AssignAttackableAffect(target);
				});
			});
		}

		public static void AssignDamage(ref float attackDamage, Race own){
			var _ = float.MinValue;
			own.GetModule<AffectModule>(am => {
				am.GetSubAffects<IAssignableDamage>(a => {
					_ = Mathf.Max(_, a.AssignDamage());
				});
			});

			attackDamage = Mathf.Max (_, attackDamage);
		}
	}
}

