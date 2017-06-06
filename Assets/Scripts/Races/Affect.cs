using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public abstract class Affect : MonoHandler
	{
		public virtual string title { get { return this.name; } }
		public virtual float gainPoint { get { return 0f; } }

		protected int turn = 1;
		protected bool isExecuted;

		public Race own;
		public Race[] targets;

		public virtual void Init(){
			
		}

		public virtual void Execute(Race target){
			
		}

		protected void AddGainPoint(float gainPoint = 0f){
			gainPoint = gainPoint > 0f ? gainPoint : this.gainPoint;
			own.AddGainPoint (gainPoint);
		}

		protected void SubtractEnergy(float energy){
			own.GetModule<EnergyModule> ((e) => {
				e.SubtractEnergy (energy);
			});
		}

		protected void SubtractGold(float gold){
			own.GetModule<GoldModule> ((g) => {
				g.SubtractGold(gold);
			});
		}

		protected bool EnoughGold(float gold, Action predicate){
			var enough = false;
			own.GetModule<GoldModule> ((g) => {
				enough = g.gold >= gold;
			});
			return enough;
		}

		protected void ExecuteInTurn(Race who, Action predicate){
			if (who.isTurn) {
				if (!isExecuted) {
					if (predicate != null) {
						predicate.Invoke ();
					}
					isExecuted = true;		
				}
			} else {
				if (isExecuted) {
					turn++;
					isExecuted = false;
				}
			}
		}

		protected bool EnoughLevel(int level, Action predicate){
			var levelModule = own.GetModule<LevelModule> ();
			if (levelModule.level < level) {
				Destroy (gameObject);	
				return false;
			}
			if (predicate != null) {
				predicate.Invoke ();
			}
			return true;
		}

		public static bool HasAffect<T>(Race who, Action predicate = null) where T: Affect{
			var affectModule = who.GetModule<AffectModule> ();
			var result = affectModule != null && affectModule.HasAffect<T> ();
			if (result && predicate != null) {
				predicate.Invoke ();
			}
			return result;
		}

		public static void RemoveAffect<T>(Race who) where T: Affect {
			var affectModule = who.GetModule<AffectModule> ();
			affectModule.GetAffects<T> (x => Destroy(x.gameObject));
			affectModule.RefreshAffect ();
		}

		public static void RemoveAffect(Race who, Affect affect){
			var affectModule = who.GetModule<AffectModule> ();
			Destroy(affect.gameObject);
			affectModule.RefreshAffect ();
		}

		public static T[] GetAffects<T>(Race who, Action<T> predicate = null) where T: Affect{
			T[] result = new T[0];
			who.GetModule<AffectModule> ((a) => {
				result = a.GetAffects<T>(predicate);
			});
			return result;
		}

		public static T[] GetSubAffects<T>(Race who, Action<T> predicate = null) {
			T[] result = new T[0];
			who.GetModule<AffectModule> ((a) => {
				result = a.GetSubAffects<T>(predicate);
			});
			return result;
		}

		public static void Create<T>(string resource, Race own, Race target, Action<T> predicate = null) where T : Affect {
			Create<T> (resource, own, new Race[]{ target }, predicate);
		}

		public static void Create<T>(string resource, Race own, Race[] targets, Action<T> predicate = null) where T : Affect {
			var a = GetMonoResource<T> (resource);
			Create<T> (a, own, targets, predicate);
		}

		public static void Create<T>(T affect, Race own, Race[] targets, Action<T> predicate = null) where T : Affect {
			var a = Instantiate<T>(affect);
			a.Init ();
			a.own = own;
			a.targets = targets;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			foreach (var target in a.targets) {
				target.GetModule<AffectModule> ((am) => {
					am.AddAffect(a);
				});
			}
		}

		public static void CreatePrimitive<T>(Race own, Race[] targets, Action<T> predicate = null) where T: Affect {
			var go = new GameObject (typeof(T).Name, typeof(T));
			var a = go.GetComponent<T> ();
			a.Init ();
			a.own = own;
			a.targets = targets;
			if (predicate != null) {
				predicate.Invoke (a);
			}
			foreach (var target in a.targets) {
				target.GetModule<AffectModule> ((am) => {
					am.AddAffect(a);
				});
				if (typeof(IPhysicalAttackingEventHandler).IsAssignableFrom (a.GetType ())) {
					var physicalAttacking = ((IPhysicalAttackingEventHandler)a);
					var stat = own.GetModule<StatModule> ();
					var targetStat = target.GetModule<StatModule> ();
					var isHit = AccuracyCalculator.IsHit (stat.attackRating, targetStat.attackRating);
					isHit = !isHit ? AccuracyCalculator.MakeSureHit(own) : isHit;
//					if (isHit) {
//						var isCritHit = AccuracyCalculator.IsCriticalHit (stat.criticalRating);
//						isCritHit = !isCritHit ? AccuracyCalculator.MakeSureCritical (own) : isCritHit;
//						// optional Damage
//						var outputDamage = AttackPowerCalculator.TakeDamage(physicalAttacking.bonusDamage, targetStat.physicalDefend, isCritHit);
//						AccuracyCalculator.HandleCriticalDamage (ref outputDamage, own, target);
//						AttackPowerCalculator.HandleDamage(ref outputDamage, own, target);
//						a.StartCoroutine (physicalAttacking.OnPhysicalHit (new PhysicalAttackingEventArgs{
//							affect = a,
//							target = target,
//							outputDamage = outputDamage,
//							isCritHit = isCritHit,
//						}));
//					} else {
//						var isCritHit = AccuracyCalculator.IsCriticalHit (stat.criticalRating);
//						var damage = AttackPowerCalculator.TakeDamage(physicalAttacking.bonusDamage, targetStat.physicalDefend, isCritHit);
//						a.StartCoroutine (physicalAttacking.OnPhysicalMiss (new PhysicalAttackingEventArgs{
//							affect = a,
//							target = target,
//							outputDamage = damage,
//							isCritHit = isCritHit,
//						}));
//					}
					var isCritHit = AccuracyCalculator.IsCriticalHit (stat.criticalRating);
					isCritHit = !isCritHit ? AccuracyCalculator.MakeSureCritical (own) : isCritHit;
					// optional Damage
					var outputDamage = AttackPowerCalculator.TakeDamage(physicalAttacking.bonusDamage, targetStat.physicalDefend, isCritHit);
					AccuracyCalculator.HandleCriticalDamage (ref outputDamage, own, target);
					AttackPowerCalculator.HandleDamage(ref outputDamage, own, target);
					a.StartCoroutine (physicalAttacking.OnPhysicalHit (new PhysicalAttackingEventArgs{
						affect = a,
						target = target,
						outputDamage = outputDamage,
						isCritHit = isCritHit,
					}));
				} else if(typeof(IMagicalAttacking).IsAssignableFrom(a.GetType())){

				} else {
					a.Execute (target);
				}
			}
			a.AddGainPoint ();
		}
	}
}

