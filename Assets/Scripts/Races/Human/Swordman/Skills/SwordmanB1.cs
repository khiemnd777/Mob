using UnityEngine;
using System;

namespace Mob
{
	public class SwordmanB1 : SkillAffect
	{
		void Start(){
			var stat = own.GetModule<StatModule>();
			PhysicalAttackCalculator.Calculate (40f + .3f * stat.physicalAttack + .2f * stat.magicAttack, own, targets);	

			foreach(var target in targets){
				Affect.CreatePrimitive<BurnAffect> (own, targets, x => {
					x.subtractHp = 6f;
					x.turnNumber = 3;
				});
			}

			AddGainPoint(8f);

			Destroy (gameObject);
		}
	}

	public class SwordmanB1Skill: Skill
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
			Affect.CreatePrimitive<SwordmanB1> (own, targets);
			SubtractEnergy();
			return true;
		}
	}

	public class SwordmanB1BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			who.GetModule<SkillModule> (x => x.Add<SwordmanB1Skill> (quantity));
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}	
}

