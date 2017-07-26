using System;

namespace Mob
{
	public class SwordmanE1 : SkillAffect, IDamaged, IHittable, ICritical
	{

		public override void Init ()
		{
			gainPoint = 30f;
		}

		void Update(){
			ExecuteInTurn (own, () => {
				if(turn == 3){
					Destroy(gameObject);
				}
			});
		}

		#region IDamaged implementation

		public float HandleDamage (float damage, Race target)
		{
			var result = 0f;
			target.GetModule<AffectModule> (x => {
				if(x.HasSubAffect<INegativeAffect>()){
					result = damage + damage * 1.5f;
				}
			});
			return result;
		}

		#endregion
	}

	public class SwordmanE1Skill : Skill
	{
		public override void Init ()
		{
			title = "Holy Knight";
			energy = 12f;
			level = 16;
			cooldown = 6;
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<SwordmanE1> (own, targets);
			return true;
		}
	}

	public class SwordmanE1BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			who.GetModule<SkillModule> (x => x.Add<SwordmanE1Skill> (quantity));
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}

		LevelModule _level;
		SkillModule _skill;

		protected override bool Interact ()
		{
			var level = _level ?? (_level = own.GetModule<LevelModule> ());
			var skill = _skill ?? (_skill = own.GetModule<SkillModule> ());
			return level.level >= 16 && skill.evolvedSkillPoint > 0;
		}
	}
}

