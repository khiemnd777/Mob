using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class Slash : Affect 
	{
		int _level = 1;
		float _energy = 4f;
		float _gainPoint = 5f;
		float _attackDamage = 110f;

		void Start(){
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
						var targetHp = target.GetModule<HealthPowerModule> ();
						targetHp.SubtractHp (damage);	
						// Add gain point when affect hit
						AddGainPoint(_gainPoint);
					}
					// accuracy is zero is miss
				}

				// Subtract own energy
				var energy = own.HasAffect<Distract>() ? 2f : _energy;
				SubtractEnergy(energy);

				Destroy (gameObject);	
			});
		}
	}
}