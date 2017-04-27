using System;
using UnityEngine;

namespace Mob
{
	public class StatModule : RaceModule
	{
		public int initPoint;

		[Header("Stat")]
		// Damage
		public float damage;

		// Resistance
		public float resistance;

		// Technique
		public float technique;

		// Luck
		public float luck;

		[Header("Stat percent")]
		// Damage percent
		public float damagePercent;

		// Resistance percent
		public float resistancePercent;

		// Technique percent
		public float techniquePercent;

		// Luck percent
		public float luckPercent;

		// allow adding point to stat values
		bool _allowAddPoint;

		public void AllowAddPoint(bool allow){
			_allowAddPoint = allow;
		}

		public int _point;

		public void SetPoint(int point){
			_point += point;
		}

		int[] arr;

		void Start(){
			arr = StatCalculator.InitProbability (damagePercent, resistancePercent, techniquePercent, luckPercent);
		}

		void Update() {
			if (_allowAddPoint) {
				foreach (var statIndex in StatCalculator.GetStatWithProbability (_point, damagePercent, resistancePercent, techniquePercent, luckPercent)) {
					switch (statIndex) {
					case 0:
						damage += 1;
						break;
					case 1:
						resistance += 1;
						break;
					case 2:
						technique += 1;
						break;
					case 3:
						luck += 1;
						break;
					default:
						break;
					}
				}
				_point = 0;
				_allowAddPoint = false;
			}
		}
	}
}

 