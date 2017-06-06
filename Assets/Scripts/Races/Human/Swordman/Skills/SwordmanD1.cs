using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mob
{
	public class SwordmanD1 : SkillAffect, ICriticalHandler, IPhysicalAttackingEventHandler
	{
		public float storedEnergy;
	
		public override float gainPoint {
			get {
				return 18f;
			}
		}

		Text targetHpLabel;

		void Start(){
			targetHpLabel = GetMonoComponent<Text> (Constants.TARGET_HP_LABEL);
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, Race target)
		{
			own.GetModule<EnergyModule> (x => x.AddEnergy(storedEnergy / 2));
			return damage;
		}

		#endregion

		#region IPhysicalAttackingEventHandler implementation

		public System.Collections.IEnumerator OnPhysicalHit (PhysicalAttackingEventArgs args)
		{
			yield return OnSetTimeout (() => {
				args.target.GetModule<HealthPowerModule> (x => x.SubtractHp (args.outputDamage));
			});

			JumpEffect (targetHpLabel.transform, Vector3.one);

			ShowSubLabel (Constants.DECREASE_LABEL, targetHpLabel.transform, args.outputDamage);

			Destroy(args.affect.gameObject, 3f);
		}

		public System.Collections.IEnumerator OnPhysicalMiss (PhysicalAttackingEventArgs args)
		{
			throw new NotImplementedException ();
		}

		public float bonusDamage {
			get {
				var stat = own.GetModule<StatModule>();
				return 20f + 10f * storedEnergy + .4f * stat.magicAttack;
			}
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