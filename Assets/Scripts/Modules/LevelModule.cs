using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
	public class LevelModule : RaceModule 
	{
		public event Action<int, int> OnLevelUp;

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
			_dynamicLevel = LevelCalculator.Up (_class.gainPoint, level, out up);
			if (_dynamicLevel > level) {
				level = _dynamicLevel;
				upLevel = up;
				if (OnLevelUp != null) {
					OnLevelUp.Invoke (level, upLevel);
				}
			}
		}
	}
		
}