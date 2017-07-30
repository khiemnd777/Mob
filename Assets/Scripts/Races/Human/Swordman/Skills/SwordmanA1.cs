using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class SwordmanA1 : SkillAffect, IPhysicalAttackingEventHandler
	{
		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 2.1f * stat.physicalAttack;
			}
		}

		public void HandleAttack(Race target){
			
		}

		public override void Init(){
			timeToDestroy = 0f;
			gainPoint = 5f;
			AddPlugin (Effect.CreatePrimitive<SwordmanA1Effect>(this, own, targets));
		}
	}

	public class SwordmanA1Effect: Effect {
		
		Text targetHpLabel;
		public override void InitPlugin ()
		{
			use = true;
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		public override IEnumerator Define (Dictionary<string, object> effectValues)
		{
			if ((bool)effectValues ["isHit"]) {
				var damage = (float)effectValues ["damage"];
				var target = (Race)effectValues ["target"];
				if (targetHpLabel == null) {
					target.GetModule<HealthPowerModule> (x => x.SubtractHpEffect ());
				} else {
					yield return OnSetTimeout (() => {
						var slashLine = InstantiateFromMonoResource<SlashLine> (Constants.EFFECT_SLASH_LINE);
						slashLine.target = targetHpLabel.transform;
					}, 0.05f);

					target.GetModule<HealthPowerModule> (x => x.SubtractHpEffect ());

					JumpEffect (targetHpLabel.transform, Vector3.one);

					ShowSubLabel (Constants.DECREASE_LABEL, targetHpLabel.transform, damage);
				}
			}
		}
	}

	public class SwordmanA1Skill : Skill
	{
		public override void Init ()
		{
			level = 1;
			energy = 4f;
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<SwordmanA1> (own, targets, t => {
				t.gainPoint = gainPoint;
			});
			return true;
		}

		void Update ()
		{
			if(usedNumber == 10){
				own.GetModule<SkillModule> (s => {
					s.Add<SwordmanA2Skill> (1);
					s.Remove(this);
				});
			}
		}
	}

	public class SwordmanA1BoughtSkill : SkillBoughtItem
	{
		public override void Init ()
		{
			title = "A1";
			brief = "Increasing 110% physical damage to opponent, when it's used 10 times will be self-upgrading to A2.";
			cooldown = 0;
			learnedLevel = 1;
			reducedEnergy = 4f;
			gainPoint = 5f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/swordman-skills").FirstOrDefault(x => x.name == "swordman-skills-a1"));	
		}

		public override void Pick (Race who, int quantity)
		{
			who.GetModule<SkillModule> (x => x.Add<SwordmanA1Skill> (quantity, t => {
				t.icons = icons;
				t.title = title;
				t.brief = brief;
				t.gainPoint = gainPoint;
				t.level = learnedLevel;
				t.cooldown = cooldown;
				t.energy = reducedEnergy;
			}));
		}

		LevelModule _level;
		SkillModule _skill;

		protected override bool Interact ()
		{
			var level = _level ?? (_level = own.GetModule<LevelModule> ());
			var skill = _skill ?? (_skill = own.GetModule<SkillModule> ());
			return level.level == 1 && !skill.HasSkill<SwordmanA1Skill>();
		}
	}
}

