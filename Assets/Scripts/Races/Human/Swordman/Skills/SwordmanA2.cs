using System;

namespace Mob
{
	public class SwordmanA2 : SkillAffect, ICriticalHandler
	{
		void Start(){
			var stat = own.GetModule<StatModule>();
			PhysicalAttackCalculator.Calculate (1.3f * stat.physicalAttack, own, targets);	
			AddGainPoint(8f);

			Destroy (gameObject);
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			return damage * 2f;
		}

		#endregion
	}

	public class SwordmanA2Skill: Skill
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
			Affect.CreatePrimitive<SwordmanA2> (own, targets);
			SubtractEnergy();
			return true;
		}
	}
}

