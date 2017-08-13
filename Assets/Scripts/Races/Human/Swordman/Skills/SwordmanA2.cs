using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class SwordmanA2 : SkillAffect, ICriticalHandler, IPhysicalAttackingEventHandler
	{
		public override void Init(){
			timeToDestroy = 0f;
			gainPoint = 8f;
			AddPlugin (Effect.CreatePrimitive<SwordmanA2Effect>(this, own, targets));
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			return damage * 2f;
		}

		#endregion

		#region IPhysicalAttackingEventHandler implementation

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 2.3f * stat.physicalAttack;
			}
		}

		public void HandleAttack(Race target){

		}

		#endregion
	}

	public class SwordmanA2Effect: Effect {

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

	public class SwordmanA2Skill: Skill
	{
		public override void Init ()
		{
			title = "A1";
			brief = "Increasing 130% physical damage to opponent and x2 damage when it be occurred an critical attack";
			cooldown = 0;
			level = 1;
			gainPoint = 8f;
			energy = 4f;
			icon.prefabs.Add ("none", "Sprites/icon");
			icon.prefabs.Add ("default", "Sprites/swordman-skills => swordman-skills-a1");
		}

		public override string GetSyncIcon ()
		{
			return icon.prefabs.ContainsKey ("default")? icon.prefabs ["default"] : icon.prefabs ["none"];
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<SwordmanA2> (own, targets, t => t.gainPoint = gainPoint);
			return true;
		}
	}
}

