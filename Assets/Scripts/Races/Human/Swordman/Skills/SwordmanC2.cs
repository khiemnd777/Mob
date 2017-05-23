using System;

namespace Mob
{
	public class SwordmanC2 : SkillAffect
	{
		void Start(){
			own.GetModule<StatModule>(x => {
				x.physicalDefend += x.physicalDefend * .2f;
				x.magicResist += x.magicResist * .2f;
			});

			Destroy (gameObject);
		}
	}

	public class SwordmanC2Skill: Skill  
	{
		public override int level {
			get {
				return 8;
			}
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<SwordmanC2> (own)) {
				return false;
			}
			Affect.CreatePrimitive<SwordmanC2> (own, targets);
			return true;
		}
	}
}

