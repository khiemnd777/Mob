﻿using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class SkillModule : RaceModule
	{
		public int evolvedSkillPoint;
		public RectTransform skillTreeUIPrefab;
		public SkillTreeUI skillTreeUI;
		public List<Skill> skills = new List<Skill>();
		public List<SkillBoughtItem> availableSkills = new List<SkillBoughtItem>();

		[SyncVar] 
		public SyncListString networkSkills = new SyncListString();

		[SyncVar] 
//		public SyncListString networkAvailableSkills = new SyncListString();
		public SyncListSkillBoughtItem networkAvailableSkills = new SyncListSkillBoughtItem();

		void Start(){
			if (skillTreeUIPrefab != null) {
				var instPfb = Instantiate (skillTreeUIPrefab, GetMonoComponent<Canvas> ("Canvas").transform);
				skillTreeUI = instPfb.GetComponent<SkillTreeUI> ();
				skillTreeUI.own = _race;
				skillTreeUI.gameObject.SetActive (false);
			}
		}

		void Update(){
			if (isServer) {
//				networkSkills.Clear ();
//
//				foreach(var str in skills.Select (x => x.title))
//					networkSkills.Add(str);
//				
//				RefreshSyncAvailableSkills();
			}
		}

		void RefreshSyncAvailableSkills(){
			var removedItems = new List<SyncSkillBoughtItem> ();
			foreach (var syncObj in networkAvailableSkills) {
				if (availableSkills.Any (x => x.GetInstanceID () == syncObj.id))
					continue;
				removedItems.Add (syncObj);
			}

			foreach (var syncObj in removedItems) {
				networkAvailableSkills.Remove (syncObj);
			}

			foreach (var item in availableSkills) {
				var syncItem = item.ToSyncSkillBoughtItem ();
				if(!networkAvailableSkills.Any(x => x.id == item.GetInstanceID())) {
					networkAvailableSkills.Add(syncItem);
					continue;
				}
				var syncObj = networkAvailableSkills.FirstOrDefault (x => x.id == item.GetInstanceID ());
				var syncObjIndex = networkAvailableSkills.IndexOf (syncObj);
				networkAvailableSkills[syncObjIndex] = syncItem;
			}
		}

		public void Add<T>(int quantity, Action<T> predicate = null) where T: Skill{
			if (!skills.Any (x => x.GetType().IsEqual<T> ())) {
				skills.Add (Skill.CreatePrimitive<T> (_race, quantity, predicate));
				return;
			}
		}

		public T GetSkill<T>() where T: Skill{
			if (!skills.Any (x => x.GetType ().IsEqual<T> ()))
				return null;
			var skill = skills.FirstOrDefault (x => x.GetType().IsEqual<T> ());
			return (T)skill;
		}

		public bool HasSkill<T>() where T: Skill{
			return skills.Any (x => x.GetType ().IsEqual<T> ());
		}

		public bool HasSkill(Skill skill){
			return skills.Any (x => x.GetType ().IsAssignableFrom(skill.GetType()));
		}

		public void AddAvailableSkill<T>(Action<T> predicate = null) where T: SkillBoughtItem{
			if (!availableSkills.Any (x => x.GetType().IsEqual<T> ())) {
				availableSkills.Add (SkillBoughtItem.CreatePrimitiveWithOwn<T> (_race, predicate));
				RefreshSyncAvailableSkills ();
				return;
			}
		}

		public void PickAvailableSkill<T>() where T: SkillBoughtItem{
			var skill = GetAvailableSkill<T> ();
			if (skill == null)
				return;
			PickAvailableSkill (skill);
		}

		public void PickAvailableSkill(SkillBoughtItem boughtItem){
			if (!availableSkills.Any (x => x.GetType ().IsAssignableFrom (boughtItem.GetType ())))
				return;
			boughtItem.Pick (_race, 1);
			boughtItem.learned = true;
			RefreshSyncAvailableSkills ();
		}

		[Command]
		public void CmdPickAvailableSkill(SyncSkillBoughtItem syncBoughtItem){
			var boughtItem = availableSkills.FirstOrDefault (x => x.GetInstanceID () == syncBoughtItem.id);
			if (boughtItem == null)
				return;
			PickAvailableSkill (boughtItem);
		}

		public T GetAvailableSkill<T>() where T: SkillBoughtItem{
			if(!availableSkills.Any(x => x.GetType().IsEqual<T>()))
				return null;
			var skill = availableSkills.FirstOrDefault (x => x.GetType ().IsEqual<T> ());
			return (T)skill;
		}

		public void Remove<T>() where T: Skill{
			if (!skills.Any (x => x.GetType ().IsEqual<T> ()))
				return;
			var skill = skills.FirstOrDefault (x => x.GetType().IsEqual<T> ());
			Destroy (skill.gameObject);
			skills.RemoveAll (x => x.GetType ().IsEqual<T> ());
		}

		public void Remove(Skill skill){
			skills.Remove (skill);
			Destroy (skill.gameObject);
		}

		public void Use<T>(Race[] targets){
			if (!skills.Any (x => x.GetType().IsEqual<T> ())) 
				return;
			var item = skills.FirstOrDefault (x => x.GetType().IsEqual<T> ());

			Use (item, targets);
		}

		public void Use(Skill skill, Race[] targets) {
			skill.Use (targets);
			skill.SubtractEnergy ();
			++skill.usedNumber;
			skill.usedTurn = _race.turnNumber;
			--skill.quantity;

			if (skill.quantity == 0) {
				skill.quantity = 1;
				return;
			}
		}

		public void OpenSkillTreeUI(){
			if (skillTreeUI == null)
				return;
			skillTreeUI.gameObject.SetActive (true);
		}
	}
}

