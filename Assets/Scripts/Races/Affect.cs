using System;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public abstract class Affect : MonoHandler
	{
		protected int turn = 1;
		protected bool isExecuted;

		public Race own;
		public Race[] targets;

		protected void AddGainPoint(float gainPoint){
			own.AddGainPoint (gainPoint);
		}

		protected void SubtractEnergy(float energy){
			var energyModule = own.GetModule<EnergyModule> ((e) => {
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

		public static bool HasAffect<T>(Race who) where T: Affect{
			var affectModule = who.GetModule<AffectModule> ();
			return affectModule != null && affectModule.HasAffect<T> ();
		}

		public static T[] GetAffects<T>(Race who) where T: Affect{
			T[] result;
			who.GetModule<AffectModule> ((a) => {
				result = a.GetAffects<T>();
			});
			return result;
		}

		public static void Create<T>(string resource, Race own, Race target) where T : Affect{
			Create<T> (resource, own, new Race[]{ target });
		}

		public static void Create<T>(string resource, Race own, Race[] targets) where T : Affect{
			var a = GetMonoResource<T> (resource);
			Create<T> (a, own, targets);
		}

		public static void Create<T>(T affect, Race own, Race[] targets) where T : Affect{
			var a = Instantiate<T>(affect);
			a.own = own;
			a.targets = targets;
			foreach (var target in a.targets) {
				target.GetModule<AffectModule> ((am) => {
					am.AddAffect(a);
				});
			}
		}
	}
}

