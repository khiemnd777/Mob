﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class SwordmanB2 : SkillAffect, IPhysicalAttackingEventHandler
	{

		public override void Init ()
		{
			timeToDestroy = 0f;
			gainPoint = 8f;
			AddPlugin (Effect.CreatePrimitive<SwordmanB2Effect> (this, own, targets));
		}

		#region IPhysicalAttackingEventHandler implementation

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 40f + 1.5f * stat.physicalAttack;
			}
		}

		public void HandleAttack(Race target){
			OnSetTimeout (() => {
				Affect.CreatePrimitiveAndUse<StunAffect> (own, new Race[]{ target }, x => {
					x.turnNumber = 1;
				});
			}, 3f);
		}

		#endregion
	}

	public class SwordmanB2Effect: Effect {

		Text targetHpLabel;
		public override void InitPlugin ()
		{
			use = true;
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		public override IEnumerator Define (Dictionary<string, object> effectValues)
		{
			if ((bool)effectValues ["isHit"]) {
				var damage = (float)effectValues["damage"];
				var target = (Race)effectValues ["target"];
				if (targetHpLabel == null) {
					target.GetModule<HealthPowerModule> (x => x.SubtractHpEffect ());
					Destroy (((Affect)host).gameObject, Constants.WAIT_FOR_DESTROY);
				} else {
					yield return OnSetTimeout (() => {
						var slashLine = InstantiateFromMonoResource<SlashLine>(Constants.EFFECT_SLASH_LINE);
						slashLine.target = targetHpLabel.transform;
					}, 0.05f);

					target.GetModule<HealthPowerModule> (x => x.SubtractHpEffect ());

					JumpEffect (targetHpLabel.transform, Vector3.one);

					ShowSubLabel (Constants.DECREASE_LABEL, targetHpLabel.transform, damage);
				}
			}
		}
	}

	public class SwordmanB2Skill: Skill
	{
		public override void Init ()
		{
			level = 4;
			energy = 6f;
			cooldown = 2;
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<SwordmanB2> (own, targets);
			return true;
		}
	}

	public class SwordmanB2BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			who.GetModule<SkillModule> (x => x.Add<SwordmanB2Skill> (quantity));
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}
}

