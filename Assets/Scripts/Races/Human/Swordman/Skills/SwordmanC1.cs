using UnityEngine;
using System;

namespace Mob
{
	public class SwordmanC1: SkillAffect
	{
		void Start(){
			var stat = own.GetModule<StatModule>();
			PhysicalAttackCalculator.Calculate (80f + .5f * stat.physicalAttack, own, targets);
			AddGainPoint (12f);
			Destroy (gameObject);
		}
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
				return 8f;
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
			var energy = this.energy;
			foreach (var target in targets) {
				energy = Affect.HasAffect<StunAffect>(target) ? 2f : this.energy;
			}
			SubtractEnergy(energy);
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

