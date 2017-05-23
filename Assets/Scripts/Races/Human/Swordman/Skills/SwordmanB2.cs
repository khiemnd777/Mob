using UnityEngine;
using System;

namespace Mob
{
	public class SwordmanB2 : SkillAffect
	{
		void Start(){
			var stat = own.GetModule<StatModule>();
			PhysicalAttackCalculator.Calculate (40f + .5f * stat.physicalAttack, own, targets);	

			foreach(var target in targets){
				Affect.CreatePrimitive<StunAffect> (own, targets, x => {
					x.turnNumber = 1;
				});
			}

			AddGainPoint(8f);

			Destroy (gameObject);
		}
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
			SubtractEnergy();
			return true;
		}
	}
}

