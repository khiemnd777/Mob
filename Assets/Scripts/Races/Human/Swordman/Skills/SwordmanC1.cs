using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mob
{
	public class SwordmanC1: SkillAffect, IPhysicalAttackingEventHandler
	{
		public override float gainPoint {
			get {
				return 12f;
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

			Destroy(args.affect.gameObject, 3f);
		}

		public System.Collections.IEnumerator OnPhysicalMiss (PhysicalAttackingEventArgs args)
		{
			throw new NotImplementedException ();
		}

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 80f + .5f * stat.physicalAttack;
			}
		}

		#endregion
	}

	public class SwordmanC1Skill: Skill
	{
		public override int level {
			get {
				return 8;
			}
		}

		public override float energy {
			get {
				var e = 8f;
				foreach (var target in own.targets) {
					if (Affect.HasAffect<StunAffect> (target)) {
						e = 2f;
						break;
					}	
				}
				return e;
			}
		}

		public override int cooldown {
			get {
				return 2;
			}
		}

		bool subEnable;

		void Update(){
			if (own.isEndTurn) {
				subEnable = false;	
			}
			if (!own.isTurn && subEnable) {
				return;
			}
			foreach (var target in own.targets) {
				if (Affect.HasAffect<StunAffect> (target)) {
					usedTurn = 0;
					subEnable = true;
					break;
				}	
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<SwordmanC1> (own, targets);
			return true;
		}
	}

	public class SwordmanC1BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			who.GetModule<SkillModule> (x => x.Add<SwordmanC1Skill> (quantity));
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}
}

