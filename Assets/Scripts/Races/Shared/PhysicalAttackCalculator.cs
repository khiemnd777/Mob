using UnityEngine;
using System;

namespace Mob
{
	public class PhysicalAttackCalculator
	{
		public static void Calculate(float bonusDamage, Race attacker, Race target) {
			var stat = attacker.GetModule<StatModule> ();
			var targetStat = target.GetModule<StatModule> ();
			var isHit = AccuracyCalculator.IsHit (stat.attackRating, targetStat.attackRating);
			isHit = !isHit ? AccuracyCalculator.MakeSureHit(attacker) : isHit;
			if (isHit) {
				var isCritHit = AccuracyCalculator.IsCriticalHit (stat.criticalRating);
				isCritHit = !isCritHit ? AccuracyCalculator.MakeSureCritical (attacker) : isCritHit;
				// optional Damage
				var outputDamage = AttackPowerCalculator.TakeDamage(bonusDamage, targetStat.physicalDefend, isCritHit);
				AccuracyCalculator.HandleCriticalDamage (ref outputDamage, attacker, target);
				AttackPowerCalculator.HandleDamage(ref outputDamage, attacker, target);
				target.GetModule<HealthPowerModule> (x => x.SubtractHp (outputDamage));
			} else {
				var isCritHit = AccuracyCalculator.IsCriticalHit (stat.criticalRating);
				var damage = AttackPowerCalculator.TakeDamage(bonusDamage, targetStat.physicalDefend, isCritHit);
				Affect.GetSubAffects<IMissingHandler>(target, handler => handler.HandleMissing(damage, attacker));
			}
		}

		public static void Calculate(float bonusDamage, Race attacker, Race[] targets) {
			foreach (var target in targets) {
				Calculate (bonusDamage, attacker, target);
			}
		}
	}
}

