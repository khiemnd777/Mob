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
}

