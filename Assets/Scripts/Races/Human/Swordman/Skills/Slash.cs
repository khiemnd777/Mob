using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class Slash : Affect
	{
		int _level = 1;
		float _gainPoint = 5f;
		float _attackDamage = 110f;

		void Start(){
//			EnoughLevel (_level, () => {
//				var stat = own.GetModule<StatModule> ();
//				var ownDamage = stat.physicalAttack;
//				var attackDamage = _attackDamage;
//
//				// Assign damage
//				AttackPowerCalculator.AssignDamage(ref attackDamage, own);
//
//				var baseDamage = ownDamage * attackDamage / 100f;
//
//				foreach (var target in targets) {
//					var targetStat = target.GetModule<StatModule> ();
//					var accuracy = AccuracyCalculator.ToPercent (stat.attackRating, targetStat.attackRating);
//
//					// Extra accuracy
//					AccuracyCalculator.HandleAccuracy(ref accuracy, own, target);
//
//					// Dodge chance
//					AccuracyCalculator.DodgeChance(ref accuracy, own, target);
//
//					var accuracyProbability = Probability.Initialize(new float[] {100f - (accuracy * 100f), accuracy * 100f});
//					var accuracyResult = Probability.GetValueInProbability(accuracyProbability);
//
//					if(accuracyResult == 1){
//						// hit
//						// Extra resistance
//						var extraResistance = 0f;
//						ResistanceCalculator.HandleResistance(ref extraResistance, own, target);
//						// Calculate damage affect on each target
//						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.physicalDefend + extraResistance);
//
//					} else {
//						// miss
//					}
//
//					if (accuracy > 0f) {
//						var extraResistance = 0f;
//
//						// Extra resistance
//						ResistanceCalculator.HandleResistance(ref extraResistance, own, target);
//
//						// Calculate damage affect on each target
//						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.strength + extraResistance);
//						damage *= accuracy;
//
//						// Handle damage from other
//						AttackPowerCalculator.HandleDamage(ref damage, own, target);
//
//						// Critical damage
//						if(Mathf.Clamp(accuracy, .7f, 1f) == accuracy){
//							GetSubAffects<ICriticalHandler>(own, handler => {
//								damage = Mathf.Max(damage, handler.HandleCriticalDamage(damage, accuracy, target));
//							});
//						}
//
//						// Subtract HP
//						target.GetModule<HealthPowerModule> (hp => {
//							hp.SubtractHp (damage);	
//						});
//
//						// Add gain point when affect hit
//						AddGainPoint(_gainPoint);
//
//						// Extra attackable affect
//						AttackPowerCalculator.ExtraAttackableAffect(own, target);
//					} else {
//						// Accuracy is zero is miss
//						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.strength);
//						GetSubAffects<IMissingHandler>(target, handler => handler.HandleMissing(accuracy, damage, own));
//					}
//				}
//
//				Destroy (gameObject);	
//			});
		}
	}

	// Item
	public class SlashSkill: Skill {
		
		public override string title {
			get {
				return "Slash";
			}
		}

		public override int level {
			get {
				return 1;
			}
		}

		public override float energy {
			get {
				return 4f;
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Slash> (own, targets);
			var energy = Affect.HasAffect<Distract>(own) ? 2f : this.energy;
			SubtractEnergy(energy);
			return true;
		}
	}
}