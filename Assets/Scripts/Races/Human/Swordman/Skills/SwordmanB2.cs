using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mob
{
	public class SwordmanB2 : SkillAffect, IPhysicalAttackingEventHandler
	{
		public override float gainPoint {
			get {
				return 8f;
			}
		}

		Text targetHpLabel;

		void Start(){
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		#region IPhysicalAttackingEventHandler implementation

		public System.Collections.IEnumerator OnPhysicalHit (PhysicalAttackingEventArgs args)
		{
			yield return OnSetTimeout (() => {
				args.target.GetModule<HealthPowerModule> (x => x.SubtractHp (args.outputDamage));
			});

			JumpEffect (targetHpLabel.transform, Vector3.one);

			ShowSubLabel (Constants.DECREASE_LABEL, targetHpLabel.transform, args.outputDamage);

			Affect.CreatePrimitive<StunAffect> (own, new Race[]{args.target}, x => {
				x.turnNumber = 1;
			});

			Destroy(args.affect.gameObject, 3f);
		}

		public System.Collections.IEnumerator OnPhysicalMiss (PhysicalAttackingEventArgs args)
		{
			throw new NotImplementedException ();
		}

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 40f + .5f * stat.physicalAttack;
			}
		}

		#endregion
	}

	public class SwordmanB2Skill: Skill
	{
		public override int level {
			get {
				return 4;
			}
		}

		public override float energy {
			get {
				return 6f;
			}
		}

		public override int cooldown {
			get {
				return 2;
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<SwordmanB2> (own, targets);
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

