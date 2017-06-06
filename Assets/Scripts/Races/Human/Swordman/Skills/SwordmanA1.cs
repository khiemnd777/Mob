using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Mob
{
	public class SwordmanA1 : SkillAffect, IPhysicalAttackingEventHandler
	{
		public override float gainPoint {
			get {
				return 5f;
			}
		}

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 1.1f * stat.physicalAttack;
			}
		}

		Text targetHpLabel;

		public override void Init(){
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		public IEnumerator OnPhysicalHit (PhysicalAttackingEventArgs args)
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

		public IEnumerator OnPhysicalMiss (PhysicalAttackingEventArgs args)
		{
			Destroy(args.affect.gameObject);
			yield return null;
		}
	}

	public class SwordmanA1Skill : Skill
	{
		public override int level {
			get {
				return 1;
			}
		}

		public override float energy {
			get {
				return 4f;
			}
		}

		public override bool Use (Race[] targets)
		{
			SkillAffect.CreatePrimitive<SwordmanA1> (own, targets);
			return true;
		}

		void Update(){
			if(usedNumber == 10){
				own.GetModule<SkillModule> (s => {
					s.Add<SwordmanA2Skill> (1);
					s.Remove(this);
				});
			}
		}
	}

	public class SwordmanA1BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			who.GetModule<SkillModule> (x => x.Add<SwordmanA1Skill> (quantity));
		}
	}
}

