using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
	public abstract class Race : MonoHandler
	{
		public static Race[] FindWithPlayerId(string playerId){
			var go = GameObject.FindGameObjectsWithTag (playerId);
			if(go.Length == 0)
				return new Race[0];

			return go
				.Select (x => x.GetComponent<Race> ())
				.ToArray();
		}

		public static T Create<T>(string resource, Action<T> predicate = null) where T: Race{
			var go = InstantiateFromMonoResource<GameObject> (resource);
			if (go == null)
				return null;
			var race = go.GetComponent<T> ();
			if (predicate != null) {
				predicate.Invoke (race);
			}
			return race;
		}

		#region Turn base

		protected bool _isTurn;

		public bool isTurn {
			get {
				return _isTurn;
			}
		}

		public void AllowTurn (bool allow)
		{
			_isTurn = allow;
		}
		 
		#endregion

		public T GetModule<T>(Action<T> predicate = null) where T: RaceModule
		{
			var module = GetComponent<T> ();
			if (module == null)
				return null;
			if (predicate != null) {
				predicate.Invoke (module);
			}
			return module;
		}

		#region Gain point functions

		// Gain point
		public float gainPoint;

		public void AddGainPoint(float p){
			gainPoint += p;
		}

		public void SubtractGainPoint(float p){
			gainPoint = Mathf.Max (gainPoint - p, 0f);
		}

		#endregion

		public virtual void DefaultValue(){
			
		}

		bool _isAttack;
		public void AllowAttack(bool allow)
		{
			_isAttack = allow;
		}

		bool _isBuy;
		public void AllowBuy(bool allow)
		{
			_isBuy = allow;
		}

		bool _isUpgrade;
		public void AllowUpgrade(bool allow)
		{
			_isUpgrade = allow;
		}

		public abstract void Attack();

		public abstract void BuyItem ();

		public abstract void Upgrade ();
	}
}

