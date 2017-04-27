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
			var player1 = FindWithPlayerId(Constants.PLAYER1);
			var player2 = FindWithPlayerId (Constants.PLAYER2);
//			Affect.Create<PunchAffect> ("Affects/PunchAffect", player1[0], player1);
			Affect.Create<Slash>("Races/Human/Swordman/Skills/Slash", player1[0], player2);
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

