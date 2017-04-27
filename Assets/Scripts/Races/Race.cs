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

		#region Affect functions

		protected List<Affect> _affects = new List<Affect>();

		public Affect[] affects{
			get{
				return _affects.ToArray();
			}
		}

		public void AddAffect (Affect affect)
		{
			if (_affects == null) {
				_affects = new List<Affect> ();
			}
			_affects.Add (affect);
		}

		protected void RefreshAffect ()
		{
			if (_affects == null)
				return;
			_affects.RemoveAll (x => x == null);
		}

		protected void StartRefreshAffect(){
			StartCoroutine (RefreshingAffect());
		}

		IEnumerator RefreshingAffect(){
			if (gameObject == null)
				yield return null;
			while (true) {
				RefreshAffect ();
				yield return null;
			}
			yield return null;
		}

		public bool HasAffect<T>() where T : Affect{
			return _affects.Any (x => x.GetType ().IsEqual<T> ());
		}

		#endregion


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

//		public float gainPoint{
//			get {
//				return _gainPoint;
//			}
//		}

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

		public abstract void Attack();

		public abstract void BuyItem ();

		public abstract void Upgrade ();
	}
}

