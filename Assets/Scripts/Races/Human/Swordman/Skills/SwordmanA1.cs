using System;

namespace Mob
{
	public class SwordmanA1 : SkillAffect
	{
		void Start(){
			var stat = own.GetModule<StatModule>();
			PhysicalAttackCalculator.Calculate (1.1f * stat.physicalAttack, own, targets);	
			AddGainPoint(5f);

			Destroy (gameObject);
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
			Affect.CreatePrimitive<SwordmanA1> (own, targets);
			SubtractEnergy();
			return true;
		}
	}
}

