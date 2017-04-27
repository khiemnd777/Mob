using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Mob
{
	public class Riptide : Affect
	{
		int _level = 12;
		float _energy = 6f;
		float _gainPoint = 15f;
		float _attackDamage = 60f;

		void Start ()
		{
			EnoughLevel (_level, () => {
				var stat = own.GetModule<StatModule> ();
				var ownDamage = stat.damage;
				var baseDamage = ownDamage + ownDamage * _attackDamage / 100f;

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
						var targetHp = target.GetModule<HealthPowerModule> ();
						targetHp.SubtractHp (damage);

						// Add stunt affect
						Affect.Create<StuntAffect>("Affects/StuntAffect", own, target);

						// Add gain point when affect hit
						AddGainPoint(_gainPoint);
					}
					// accuracy is zero is miss
				}

				// Subtract own energy
				SubtractEnergy(_energy);

				Destroy (gameObject);	
			});
		}
	}
}

