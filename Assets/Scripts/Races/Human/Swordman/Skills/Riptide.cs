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
//			EnoughLevel (_level, () => {
//				var stat = own.GetModule<StatModule> ();
//				var ownDamage = stat.damage;
//				var attackDamage = _attackDamage;
//
//				// Assign damage
//				AttackPowerCalculator.AssignDamage(ref attackDamage, own);
//
//				var baseDamage = ownDamage * attackDamage / 100f;
//
//				foreach (var target in targets) {
//					var targetStat = target.GetModule<StatModule> ();
//					var accuracy = AccuracyCalculator.ToPercent (stat.technique, targetStat.technique);
//
//					// Extra accuracy
//					AccuracyCalculator.HandleAccuracy(ref accuracy, own, target);
//
//					// Dodge chance
//					AccuracyCalculator.DodgeChance(ref accuracy, own, target);
//
//					if (accuracy > 0f) {
//						var extraResistance = 0f;
//
//						// Extra resistance
//						ResistanceCalculator.HandleResistance(ref extraResistance, own, target);
//
//						// Calculate damage affect on each target
//						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.resistance + extraResistance);
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
//						// Add stunt affect
//						Affect.CreatePrimitive<StunAffect>(own, new Race[] {target});
//
//						// Add gain point when affect hit
//						AddGainPoint(_gainPoint);
//
//						// Extra attackable affect
//						AttackPowerCalculator.ExtraAttackableAffect(own, target);
//					}
//					// accuracy is zero is miss
//					else{
//						// Accuracy is zero is miss
//						var damage = AttackPowerCalculator.TakeDamage(baseDamage, targetStat.resistance);
//						GetSubAffects<IMissingHandler>(target, handler => handler.HandleMissing(accuracy, damage, own));
//					}
//				}
//
//				Destroy (gameObject);
//			});
		}
	}

	// Item
	public class RiptideSkill: Skill {
		
		public override string title {
			get {
				return "Riptide";
			}
		}

		public override int level {
			get {
				return 12;
			}
		}

		public override float energy {
			get {
				return 6f;
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Riptide> (own, targets);
			SubtractEnergy();
			return true;
		}
	}
}

