using UnityEngine;

namespace Mob
{
	public class AttackPowerCalculator
	{
		public static float TakeDamage(float attackerDamage, float targetResistance){
			return attackerDamage * attackerDamage / (attackerDamage + targetResistance);
		}
	}
}

