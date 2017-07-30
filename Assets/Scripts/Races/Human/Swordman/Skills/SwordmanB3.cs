using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class SwordmanB3 : SkillAffect, IMissingHandler
	{
		public override void Init ()
		{
			timeToDestroy = 0f;
		}
		#region IMissingHandler implementation
		public void HandleMissing (float damage, Race target)
		{
			own.GetModule<StatModule> (s => s.AddPoint(StatType.Dexterity, 1));
		}
		#endregion
	}

	public class SwordmanB3Skill: Skill
	{
		public override void Init ()
		{
			level = 4;
		}
		
		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<SwordmanB3> (own)) {
				return false;
			}
			Affect.CreatePrimitiveAndUse<SwordmanB3> (own, targets);
			return true;
		}
	}

	public class SwordmanB3BoughtSkill : SkillBoughtItem
	{
		public override void Init ()
		{
			title = "B3";
			brief = "+1 dexterity when you obviously avoid of opponent's.";
			cooldown = 0;
			learnedLevel = 4;
			gainPoint = 0f;
			reducedEnergy = 0f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/swordman-skills").FirstOrDefault(x => x.name == "swordman-skills-b3"));	
		}

		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			BuyAndUseImmediately<SwordmanB1Skill> (who, new Race[]{ who }, 0f, t => {
				t.icons = icons;
				t.title = title;
				t.brief = brief;
				t.cooldown = cooldown;
				t.level = learnedLevel;
				t.gainPoint = gainPoint;
				t.energy = reducedEnergy;
			});
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}

		LevelModule _level;
		SkillModule _skill;

		protected override bool Interact ()
		{
			var level = _level ?? (_level = own.GetModule<LevelModule> ());
			var skill = _skill ?? (_skill = own.GetModule<SkillModule> ());
			return level.level >= 4 && skill.evolvedSkillPoint > 0 && !skill.HasSkill<SwordmanB3Skill>();
		}
	}
}

