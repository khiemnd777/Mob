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

	public class SwordmanC2BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			BuyAndUseImmediately<SwordmanC2Skill> (who, new Race[]{ who }, 0f);
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}
}

