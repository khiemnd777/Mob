using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class SkillModule : RaceModule
	{
		public int evolvedSkillPoint;
		public RectTransform skillTreeUIPrefab;
		public SkillTreeUI skillTreeUI;

		public List<Skill> skills = new List<Skill>();
		public List<BoughtItem> availableSkills = new List<BoughtItem>();

		void Start(){
			if (skillTreeUIPrefab != null) {
				var instPfb = Instantiate (skillTreeUIPrefab, GetMonoComponent<Canvas> ("Canvas").transform);
				skillTreeUI = instPfb.GetComponent<SkillTreeUI> ();
				skillTreeUI.own = _race;
				skillTreeUI.gameObject.SetActive (false);
			}
		}

		public void Add<T>(int quantity) where T: Skill{
			if (!skills.Any (x => x.GetType().IsEqual<T> ())) {
				skills.Add (Skill.CreatePrimitive<T> (_race, quantity));
				return;
			}
		}

		public T GetSkill<T>() where T: Skill{
			if (!skills.Any (x => x.GetType ().IsEqual<T> ()))
				return null;
			var skill = skills.FirstOrDefault (x => x.GetType().IsEqual<T> ());
			return (T)skill;
		}

		public void AddAvailableSkill<T>(Action<T> predicate = null) where T: BoughtItem{
			if (!availableSkills.Any (x => x.GetType().IsEqual<T> ())) {
				availableSkills.Add (BoughtItem.CreatePrimitive<T> (predicate));
				return;
			}
		}

		public void PickAvailableSkill<T>() where T: BoughtItem{
			var skill = GetAvailableSkill<T> ();
			if (skill == null)
				return;
			skill.Pick (_race, 1);
		}

		public T GetAvailableSkill<T>() where T: BoughtItem{
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
			
			item.Use(targets);
			--item.quantity;

			if (item.quantity == 0) {
				item.quantity = 1;
				return;
			}
		}

		public void Use(Skill skill, Race[] targets){
			skill.Use (targets);
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

