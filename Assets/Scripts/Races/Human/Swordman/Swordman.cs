using UnityEngine;

namespace Mob
{
	public class Swordman : Human
	{
		LevelModule _level;
		StatModule _stat;
		HealthPowerModule _hp;

		void Start(){
			_stat = GetModule<StatModule>();
			_hp = GetModule<HealthPowerModule> ();
			_level = GetModule<LevelModule>();
			_level.OnLevelUp += (level, upLevel) => {
				// Stat
				_stat.AllowAddPoint(true);
				var point = StatCalculator.GeneratePoint(upLevel, _stat.initPoint);
				_stat.SetPoint(point);

				// HP
				_hp.SetMaxHp(upLevel);
			};
		}

		public override void DefaultValue ()
		{
			GetModule<StatModule> ((stat) => {
				stat.damagePercent = 25f;
				stat.resistancePercent = 15f;
				stat.techniquePercent = 40f;
				stat.luckPercent = 20f;

				stat.damage = 5f;
				stat.resistance = 4f;
				stat.technique = 6f;
				stat.luck = 5f;
			});
			GetModule<LevelModule> ((level) => {
				level.level = 1;
				level.maxLevel = 16;
				level.seed = 20;
			});
			GetModule<HealthPowerModule> ((hp) => {
				hp.hp = 300f;
				hp.maxHp = 300f;
				hp.hpPercent = 10f;
			});
			GetModule<GoldModule> ((gold) => {
				gold.maxGold = 999f;
			});
			GetModule<EnergyModule> ((energy) => {
				energy.maxEnergy = 12f;
			});

			// Inventory is used to store the items
			GetModule<InventoryModule> (inventory => {
				inventory.Add<SpeedyItem>(99);
				inventory.Add<PotionItem>(99);
				inventory.Add<GreatPotionItem>(99);
				inventory.Add<BurstStrengthItem>(99);
				inventory.Add<AntidoteItem>(99);
			});

			// Skill is used to store the skills
			GetModule<SkillModule> (skill => {
				skill.Add<SlashSkill>(1, 1f);
				skill.Add<RiptideSkill>(1, 3f);
				skill.Add<RavageSkill>(1, 1f);
				skill.Add<DistractSkill>(1, 2f);
				skill.Add<HolyKnightSkill>(1, 4f);
			});
		}

		public override void Attack ()
		{
			
		}

		public override void BuyItem ()
		{
			
		}

		public override void Upgrade ()
		{
			
		}
	}
}

