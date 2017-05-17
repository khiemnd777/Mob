﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class SkillModule : RaceModule
	{
		public List<Skill> skills = new List<Skill>();

		public void Add<T>(int quantity) where T: Skill{
			if (!skills.Any (x => x.GetType().IsEqual<T> ())) {
				skills.Add (Skill.CreatePrimitive<T> (_race, quantity));
				return;
			}
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
			if (skill.EnoughEnergy () && skill.EnoughLevel () && skill.EnoughCooldown ()) {
				skill.Use (targets);
				skill.usedTurn = _race.turnNumber;
				--skill.quantity;

				if (skill.quantity == 0) {
					skill.quantity = 1;
					return;
				}
			}
		}
	}
}

