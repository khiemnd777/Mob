using System;
using UnityEngine;

namespace Mob
{
	public enum StatType {
		Strength, Dexterity, Intelligent, Vitality, Luck
	}
	
	public class StatModule : RaceModule
	{
		public int initPoint = 5;

		[Header("Strength")]
		public float strength = 1f;
		[Header("Sub-strength")]
		public float physicalAttack;
		public float physicalAttackSeed = 2f;
		public float physicalDefend;
		public float physicalDefendSeed = 1.5f;

		[Header("Dexterity")]
		public float dexterity = 1f;
		[Header("Sub-dexterity")]
		public float attackRating;
		public float attackRatingSeed = 2f;
		public float criticalRating;
		public float criticalRatingSeed = 0.01f;

		[Header("Intelligent")]
		public float intelligent = 1f;
		[Header("Sub-intelligent")]
		public float magicAttack;
		public float magicAttackSeed = 1.75f;
		public float magicResist;
		public float magicResistSeed = 1.5f;

		[Header("Vitality")]
		public float vitality = 1f;
		[Header("Sub-vitality")]
		public float maxHp;
		public float maxHpSeed = 3f;
		public float regenerateHp;
		public float regenerateHpSeed = 0.002f;

		[Header("Luck")]
		public float luck = 1f;
		[Header("Sub-luck")]
		public float luckDice;
		public float luckDiceSeed = 1f;
		public float luckReward;
		public float luckRewardSeed = 1f;

		[Header("Stat percent")]
		public float strengthPercent;
		public float dexterityPercent;
		public float intelligentPercent;
		public float vitalityPercent;
		public float luckPercent;

		// allow adding point to stat values
		bool _autoAddPoint;

		public void AutoAddPoint(bool allow){
			_autoAddPoint = allow;
		}

		int logPoint;
		public int point;

		public void SetPoint(int point){
			this.point += point;
		}

		public void ResetPoint(){
			if (logPoint == 0)
				return;
			point += logPoint;
			logPoint = 0;
		}

		public void AddPoint(StatType statType, int point){
			switch (statType) {
			case StatType.Strength:
				{
					strength += point;
					logPoint += point;
					physicalAttack = strength * physicalAttackSeed; 
					physicalDefend = strength * physicalDefendSeed;
				}
				break;
			case StatType.Dexterity:
				{
					dexterity += point;
					logPoint += point;
					attackRating = dexterity * attackRatingSeed;
					criticalRating = dexterity * criticalRatingSeed;
				}
				break;
			case StatType.Intelligent:
				{
					intelligent += point;
					logPoint += point;
					magicAttack = intelligent * magicAttackSeed;
					magicResist = intelligent * magicResistSeed;
				}
				break;
			case StatType.Vitality:
				{
					vitality += point;
					logPoint += point;
					if (Mathf.Clamp (vitality, 10f, 20f) == vitality) {
						maxHpSeed = 5f;
					}
					if (Mathf.Clamp (vitality, 21f, 40f) == vitality) {
						maxHpSeed = 6f;
					}
					if (Mathf.Clamp (vitality, 41f, 60f) == vitality) {
						maxHpSeed = 8f;
					}
					if (Mathf.Clamp (vitality, 61f, 80f) == vitality) {
						maxHpSeed = 11f;
					}
					if (vitality > 80f) {
						maxHpSeed = 15f;
					}
					maxHp = vitality * maxHpSeed;
					regenerateHp = vitality * regenerateHpSeed;
				}
				break;
			case StatType.Luck:
				{
					luck += point;
					logPoint += point;
					luckDice = luck * luckDiceSeed;
					luckReward = luck * luckRewardSeed;
				}
				break;
			default:
				break;
			}
		}

		bool increaseMaxHP;

		public void AddPoint(StatType statType){
			if (point == 0)
				return;
			
			switch (statType) {
			case StatType.Strength:
				{
					++strength;
					++logPoint;
					physicalAttack = strength * physicalAttackSeed; 
					physicalDefend = strength * physicalDefendSeed;
					point = Mathf.Max(0, point - 1);
				}
				break;
			case StatType.Dexterity:
				{
					++dexterity;
					++logPoint;
					attackRating = dexterity * attackRatingSeed;
					criticalRating = dexterity * criticalRatingSeed;
					point = Mathf.Max(0, point - 1);
				}
				break;
			case StatType.Intelligent:
				{
					++intelligent;
					++logPoint;
					magicAttack = intelligent * magicAttackSeed;
					magicResist = intelligent * magicResistSeed;
					point = Mathf.Max(0, point - 1);
				}
				break;
			case StatType.Vitality:
				{
					++vitality;
					++logPoint;
					if (Mathf.Clamp (vitality, 10f, 20f) == vitality) {
						maxHpSeed = 5f;
						increaseMaxHP = vitality <= 10f;
					}
					if (Mathf.Clamp (vitality, 21f, 40f) == vitality) {
						maxHpSeed = 6f;
						increaseMaxHP = vitality <= 21f;
					}
					if (Mathf.Clamp (vitality, 41f, 60f) == vitality) {
						maxHpSeed = 8f;
						increaseMaxHP = vitality <= 41f;
					}
					if (Mathf.Clamp (vitality, 61f, 80f) == vitality) {
						maxHpSeed = 11f;
						increaseMaxHP = vitality <= 61f;
					}
					if (vitality > 80f) {
						maxHpSeed = 15f;
						increaseMaxHP = vitality <= 81f;
					}
					maxHp = vitality * maxHpSeed;
					regenerateHp = vitality * regenerateHpSeed;
					point = Mathf.Max(0, point - 1);
					if (increaseMaxHP) {
						GetModule<HealthPowerModule> (x => x.SetMaxHp (setFullHp: false));
						increaseMaxHP = false;
					}
				}
				break;
			case StatType.Luck:
				{
					++luck;
					++logPoint;
					luckDice = luck * luckDiceSeed;
					luckReward = luck * luckRewardSeed;
					point = Mathf.Max(0, point - 1);
				}
				break;
			default:
				break;
			}
		}

		void AutoCalculatePoint(){
			if (_autoAddPoint) {
				foreach (var statIndex in StatCalculator.GetStatWithProbability (point, strengthPercent, dexterityPercent, intelligentPercent, vitalityPercent, luckPercent)) {
					switch (statIndex) {
					case 0:
						AddPoint (StatType.Strength);
						break;
					case 1:
						AddPoint (StatType.Dexterity);
						break;
					case 2:
						AddPoint (StatType.Intelligent);
						break;
					case 3:
						AddPoint (StatType.Vitality);
						break;
					case 4:
						AddPoint (StatType.Luck);
						break;
					default:
						break;
					}
				}
				this.point = 0;
				_autoAddPoint = false;
			}
		}

		void Update() {
			AutoCalculatePoint ();
		}
	}
}

 