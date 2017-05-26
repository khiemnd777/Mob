using System;

namespace Mob
{
	public class SwordmanE1 : SkillAffect, IDamaged, IHittable, ICritical
	{
		void Start(){
			AddGainPoint(30f);	
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
					result = damage + damage * .5f;
				}
			});
			return result;
		}

		#endregion
	}

	public class SwordmanE1Skill : Skill
	{
		public override string title {
			get {
				return "Holy Knight";
			}
		}

		public override float energy {
			get {
				return 12f;
			}
		}

		public override int level {
			get {
				return 16;
			}
		}

		public override int cooldown {
			get {
				return 6;
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<SwordmanE1> (own, targets);
			SubtractEnergy ();
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
	}
}

