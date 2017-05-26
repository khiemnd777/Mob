using System;
using UnityEngine;

namespace Mob
{
	public class SwordmanB3 : SkillAffect, IMissingHandler
	{
		#region IMissingHandler implementation
		public void HandleMissing (float damage, Race target)
		{
			own.GetModule<StatModule> (s => s.AddPoint(StatType.Dexterity, 1));
		}
		#endregion
	}

	public class SwordmanB3Skill: Skill
	{
		public override int level {
			get {
				return 4;
			}
		}
		
		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<SwordmanB3> (own)) {
				return false;
			}
			Affect.CreatePrimitive<SwordmanB3> (own, targets);
			return true;
		}
	}

	public class SwordmanB3BoughtSkill : BoughtItem
	{

		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			BuyAndUseImmediately<SwordmanB1Skill> (who, new Race[]{ who }, 0f);
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}
}

