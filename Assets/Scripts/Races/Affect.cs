using System;
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
			var energyModule = own.GetModule<EnergyModule> ();
			energyModule.SubtractEnergy (energy);
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
				target.AddAffect (a);
			}
		}
	}
}

