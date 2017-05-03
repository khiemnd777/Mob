using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class AffectModule : RaceModule
	{
		#region Affect functions

		List<Affect> _affects = new List<Affect>();

		public Affect[] affects{
			get {
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

		public int RemoveAffect(Predicate<Affect> match){
			return _affects.RemoveAll(match);
		}

		public bool HasAffect<T>() where T : Affect{
			return _affects.Any (x => x.GetType ().IsEqual<T> ());
		}

		public T[] GetAffects<T>() where T: Affect{
			T[] result = _affects.Where(x => x.GetType().IsEqual<T>()).Cast<T>().ToArray();
			return result;
		}

		public void RefreshAffect ()
		{
			if (_affects == null)
				return;
			_affects.RemoveAll (x => x == null);
		}

		public Affect[] GetNegativeAffects(){
			if (_affects == null)
				return null;
			return _affects
				.Where (x => typeof(INegativeAffect).IsAssignableFrom (x.GetType ()))
				.ToArray();
		}

		IEnumerator RefreshingAffect(){
			if (gameObject == null)
				yield return null;
			while (true) {
				RefreshAffect ();
				yield return null;
			}
		}

		void Start(){
			StartCoroutine (RefreshingAffect());
		}

		#endregion
	}
}

