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
					var accuracy = HasAffect<HolyKnight>(own) ? 1f : AccuracyCalculator.ToPercent (stat.technique, targetStat.technique);
					if(HasAffect<BurstStrength>(own)){
						var burstStrength = GetAffects<BurstStrength>(own)[0];
						burstStrength.use = true;
						accuracy = 1f;
					}
					if (accuracy > 0f) {
						var dogde = 0f;
						if(HasAffect<Speedy>(target)){
							var speedy = GetAffects<Speedy>(target)[0];
							speedy.use = true;
							dogde = targetStat.resistance * .75f;
						}
						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.resistance + dogde);
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