using System;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class SwordmanC2 : SkillAffect
	{
		void Start(){
			timeToDestroy = 0f;
			own.GetModule<StatModule>(x => {
				x.physicalDefend += x.physicalDefend * .2f;
				x.magicResist += x.magicResist * .2f;
			});
		}
	}

	public class SwordmanC2Skill: Skill  
	{
		public override void Init ()
		{
			level = 8;
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<SwordmanC2> (own)) {
				return false;
			}
			Affect.CreatePrimitiveAndUse<SwordmanC2> (own, targets);
			return true;
		}
	}

	public class SwordmanC2BoughtSkill : SkillBoughtItem
	{
		public override void Init ()
		{
			title = "C2";
			brief = "+20% physcial defend and magical resistance";
			cooldown = 0;
			learnedLevel = 8;
			gainPoint = 0f;
			reducedEnergy = 0f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/swordman-skills").FirstOrDefault(x => x.name == "swordman-skills-c2"));	
		}

		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			BuyAndUseImmediately<SwordmanC2Skill> (who, new Race[]{ who }, 0f, t => 
				{
					t.level = learnedLevel;
					t.icons = icons;
					t.title = title;
					t.brief = brief;
					t.cooldown = cooldown;
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
			return level.level >= 8 && skill.evolvedSkillPoint > 0 && !skill.HasSkill<SwordmanC2Skill>();
		}
	}
}

