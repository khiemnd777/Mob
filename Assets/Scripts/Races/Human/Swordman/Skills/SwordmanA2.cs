﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class SwordmanA2 : SkillAffect, ICriticalHandler, IPhysicalAttackingEventHandler
	{
		
		Text targetHpLabel;

		public override void Init(){
			gainPoint = 8f;
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			return damage * 2f;
		}

		#endregion

		#region IPhysicalAttackingEventHandler implementation

		public System.Collections.IEnumerator OnPhysicalHit (PhysicalAttackingEventArgs args)
		{
			yield return OnSetTimeout (() => {
				var slashLine = InstantiateFromMonoResource<SlashLine>(Constants.EFFECT_SLASH_LINE);
				slashLine.target = targetHpLabel.transform;
			}, 0.05f);

			yield return OnSetTimeout (() => {
				args.target.GetModule<HealthPowerModule> (x => x.SubtractHp (args.outputDamage));
			});

			JumpEffect (targetHpLabel.transform, Vector3.one);

			ShowSubLabel (Constants.DECREASE_LABEL, targetHpLabel.transform, args.outputDamage);

			Destroy(args.affect.gameObject, Constants.WAIT_FOR_DESTROY);
		}

		public System.Collections.IEnumerator OnPhysicalMiss (PhysicalAttackingEventArgs args)
		{
			throw new NotImplementedException ();
		}

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 2.3f * stat.physicalAttack;
			}
		}

		#endregion
	}

	public class SwordmanA2Skill: Skill
	{
		public override void Init ()
		{
			level = 1;
			energy = 4f;
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<SwordmanA2> (own, targets);
			return true;
		}
	}
}

