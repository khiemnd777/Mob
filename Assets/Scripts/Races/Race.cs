using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public abstract class Race : MonoHandler
	{
		public Race[] targets;

		public static Race[] FindWithPlayerId(params string[] playerId){
			var list = new List<GameObject> ();
			foreach (var pid in playerId) {
				var go = GameObject.FindGameObjectsWithTag (pid);
				if (go.Length > 0) {
					list.AddRange (go);
				}
			}

			return list
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

		public static T CreatePrimitive<T>(Action<T> predicate = null) where T: Race{
			var go = new GameObject (typeof(T).Name, typeof(T));
			var race = go.GetComponent<T> ();
			if (predicate != null) {
				predicate.Invoke (race);
			}
			return race;
		}

		#region Turn base

		protected bool _isTurn;
		protected int _turnNumber;

		public bool isTurn {
			get {
				return _isTurn;
			}
		}

		public int turnNumber{
			get{
				return _turnNumber;
			}
		}

		public void AllowTurn (bool allow)
		{
			_isTurn = allow;
		}
		 
		#endregion

		#region Gain point functions

		// Gain point
		public float gainPoint;
		public float gainPointLabel;

		public void AddGainPoint(float p){
			gainPoint += p;
			While ((inc, step) => {
				gainPointLabel += inc;
			}, p, 1f);
			var gainPointValue = GetMonoComponent<Text> (Constants.ATTACKER_GAIN_POINT_LABEL);
			if (p > 0) {
				if (gainPointValue != null) {
					JumpEffect (gainPointValue.transform, Vector3.one);
					ShowSubLabel (Constants.INCREASE_LABEL, gainPointValue.transform, p);
				}
			}
		}

		public void SubtractGainPoint(float p){
			gainPoint = Mathf.Max (gainPoint - p, 0f);
		}

		#endregion

		public virtual void DefaultValue(){
			
		}

		protected bool _enableAttack;
		public void AllowAttack(bool allow)
		{
			_enableAttack = allow;
		}

		protected bool _enableBuy;
		public void AllowBuy(bool allow)
		{
			_enableBuy = allow;
		}

		protected bool _enableUpgrade;
		public void AllowUpgrade(bool allow)
		{
			_enableUpgrade = allow;
		}

		public Skill[] GetAvailableSkills(){
			var skills = new Skill[0];
			GetModule<SkillModule> (x => {
				skills = x.skills.ToArray();
			});
			return skills;
		}

		public virtual void Attack(Race[] targets, Skill skill){
			if (!_enableAttack)
				return;
			skill.Use (targets);
		}

		public virtual void Attack<T>(Race[] targets){
			if (!_enableAttack)
				return;
			GetModule<SkillModule> (x => x.Use<T> (targets));
		}

		public virtual void BuyItem (){
			
		}

		public virtual void Upgrade (){
			
		}

		public virtual void OpenSkillTree(){
			
		}

		public virtual void StartTurn(){
			_isTurn = true;
			_isEndTurn = false;
			_turnNumber++;
			GetModule<StatModule> (s => {
				GetModule<HealthPowerModule>(hp => {
					Affect.CreatePrimitiveAndUse<HealthPowerRestoring> (this, new Race[]{this}, hpr => hpr.extraHp = s.regenerateHp * hp.hp);
				});
			});
		}

		protected bool _isEndTurn;
		public bool isEndTurn{
			get{
				return _isEndTurn;
			}
		}
		public virtual void EndTurn(){
			_isTurn = false;
			_isEndTurn = true;
		}
	}
}

