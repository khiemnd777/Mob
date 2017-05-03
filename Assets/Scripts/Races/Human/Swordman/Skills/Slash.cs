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
				var attackDamage = HasAffect<Tier1>(own) 
					|| HasAffect<Tier2>(own) 
					|| HasAffect<Tier3>(own) ? 120f : _attackDamage;
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
						if(HasAffect<Aura1>(own) || HasAffect<Aura2>(own)){
							if (Mathf.Clamp (accuracy, .7f, 1f) == accuracy) {
								damage *= 2f;
							}
						}
						var targetHp = target.GetModule<HealthPowerModule> ();
						targetHp.SubtractHp (damage);	
						// Add gain point when affect hit
						AddGainPoint(_gainPoint);
					}
					// accuracy is zero is miss
				}

				// Subtract own energy
				var energy = HasAffect<Distract>(own) ? 2f : _energy;
				SubtractEnergy(energy);

				Destroy (gameObject);	
			});
		}
	}
}