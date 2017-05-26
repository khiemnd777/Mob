using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
	public class LevelModule : RaceModule 
	{
		// Level
		public int level;

		// Up level
		public int upLevel;

		// Max level
		public int maxLevel;

		// Seed to generate next level
		public float seed;

		int _dynamicLevel;
		
		void Update(){
			int up;
			LevelCalculator.maxLevel = maxLevel;
			LevelCalculator.seed = seed;
			_dynamicLevel = LevelCalculator.Up (_race.gainPoint, level, out up);
			if (_dynamicLevel > level) {
				level = _dynamicLevel;
				upLevel = up;

				GetModules<ILevelUpEventHandler> (x => {
					x.OnLevelUp(level, upLevel);
				});
				BattleController.EmitLevelUpEvent(_race, level, upLevel);
			}
		}
	}

	public interface ILevelUpEventHandler
	{
		void OnLevelUp(int level, int levelUp);
	}
}