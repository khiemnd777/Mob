using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class Ravage : Affect 
	{
		int _level = 4;
		float _energy = 6f;
		float _gainPoint = 10f;
		float _attackDamage = 130f;
		float _critTime = 3f;

		void Start ()
		{
			EnoughLevel (_level, () => {
				var stat = own.GetModule<StatModule> ();
				var ownDamage = stat.damage;
				var baseDamage = ownDamage * _attackDamage / 100f;

				foreach (var target in targets) {
					var targetStat = target.GetModule<StatModule> ();
					var accuracy = own.HasAffect<HolyKnight>() ? 100f : AccuracyCalculator.ToPercent (stat.technique, targetStat.technique);
					if (accuracy > 0f) {
						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.resistance);
						damage *= accuracy;
						if (Mathf.Clamp (accuracy, .7f, 1f) == accuracy) {
							damage *= _critTime;
						}
						var targetHp = target.GetModule<HealthPowerModule> ();
						targetHp.SubtractHp (damage);	
						// Add gain point when affect hit
						AddGainPoint(_gainPoint);
					}
				}

				// Subtract own energy
				SubtractEnergy(_energy);

				Destroy (gameObject);	
			});
		}
	}
}