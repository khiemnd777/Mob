using UnityEngine;
using System;

namespace Mob
{
	public class SwordmanD1 : SkillAffect, ICriticalHandler
	{
		public float storedEnergy;
		
		void Start() {
			var stat = own.GetModule<StatModule>();
			MagicAttackCalculator.Calculate (20f + 10f * storedEnergy + .4f * stat.magicAttack, own, targets);
			AddGainPoint(18f);

			Destroy (gameObject);
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			own.GetModule<EnergyModule> (x => x.AddEnergy(storedEnergy / 2));
			return damage;
		}

		#endregion
	}

	public class SwordmanD1Skill : Skill
	{
		public override float energy {
			get {
				return Mathf.Max(6f, own.GetModule<EnergyModule> ().energy);
			}
		}

		public override int level {
			get {
				return 12;
			}
		}

		public override int cooldown {
			get {
				return 3;
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<SwordmanB2> (own, targets);
			SubtractEnergy();
			return true;
		}
	}

	public class SwordmanD1BoughtSkill : BoughtItem
	{
		public override void Pick (Race who, int quantity)
		{
			var skillModule = who.GetModule<SkillModule> ();
			if (skillModule.evolvedSkillPoint <= 0)
				return;
			who.GetModule<SkillModule> (x => x.Add<SwordmanD1Skill> (quantity));
			--skillModule.evolvedSkillPoint;
			enabled = false;
		}
	}
}