using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Mob
{
	public enum StatType {
		Strength, Dexterity, Intelligent, Vitality, Luck
	}

	public enum Stat2ndType {
		PhysicalAttack, PhysicalDefend
		, AttackRating, CriticalRating
		, MagicAttack, MagicResist
		, MaxHp, RegenerateHp
		, LuckDice, LuckReward
	}
	
	public class StatModule : RaceModule
	{
		public int initPoint = 5;

		int logPoint;
		[Header("Gain point")]
		[SyncVar(hook="OnPointChanged")] 	public int point;

		void OnPointChanged(int currentPoint){
			EventManager.TriggerEvent (Constants.EVENT_STAT_POINT_CHANGED, new {point = currentPoint});
		}

		[Header("Strength")]
		[SyncVar(hook="OnStrengthChanged")]	public float strength = 1f;

		void OnStrengthChanged(float currentStrength){
			EventManager.TriggerEvent (Constants.EVENT_STAT_STRENGTH_CHANGED, new { strength = currentStrength });
		}

		[Header("Sub-strength")]
		[SyncVar(hook="OnPhysicalAttackChanged")]	public float physicalAttack;

		void OnPhysicalAttackChanged (float currentPhysicalAttack){
			EventManager.TriggerEvent (Constants.EVENT_STAT_PHYSICAL_ATTACK_CHANGED, new {physicalAttack = currentPhysicalAttack});
		}

		public float physicalAttackSeed = 2f;

		[SyncVar(hook="OnPhysicalDefendChanged")] 	public float physicalDefend;
		void OnPhysicalDefendChanged(float currentPhysicalDefend){
			EventManager.TriggerEvent (Constants.EVENT_STAT_PHYSICAL_DEFEND_CHANGED, new { physicalDefend = currentPhysicalDefend });
		}

		public float physicalDefendSeed = 1.5f;

		[Header("Dexterity")]
		[SyncVar(hook="OnDexterityChanged")] 	public float dexterity = 1f;

		void OnDexterityChanged(float currentDexterity){
			EventManager.TriggerEvent (Constants.EVENT_STAT_DEXTERITY_CHANGED, new {dexterity = currentDexterity});
		}

		[Header("Sub-dexterity")]
		[SyncVar(hook="OnAttackRatingChanged")] 	public float attackRating;

		void OnAttackRatingChanged(float currentAttackRating){
			EventManager.TriggerEvent (Constants.EVENT_STAT_ATTACK_RATING_CHANGED, new {attackRating = currentAttackRating});
		}

		public float attackRatingSeed = 2f;
		[SyncVar(hook="OnCriticalRatingChanged")] 	public float criticalRating;

		void OnCriticalRatingChanged(float currentCriticalRating){
			EventManager.TriggerEvent (Constants.EVENT_STAT_CRITICAL_RATING_CHANGED, new {criticalRating = currentCriticalRating});	
		}

		public float criticalRatingSeed = 0.75f;

		[Header("Intelligent")]
		[SyncVar(hook="OnIntelligentChanged")] 	public float intelligent = 1f;

		void OnIntelligentChanged(float currentIntelligent){
			EventManager.TriggerEvent (Constants.EVENT_STAT_INTELLIGENT_CHANGED, new { intelligent = currentIntelligent });
		}

		[Header("Sub-intelligent")]
		[SyncVar(hook="OnMagicAttackChanged")] 	public float magicAttack;

		void OnMagicAttackChanged(float currentMagicAttack){
			EventManager.TriggerEvent (Constants.EVENT_STAT_MAGIC_ATTACK_CHANGED, new { magicAttack = currentMagicAttack });
		}

		public float magicAttackSeed = 1.75f;

		[SyncVar(hook="OnMagicResistChanged")] 	public float magicResist;

		void OnMagicResistChanged(float currentMagicResist){
			EventManager.TriggerEvent (Constants.EVENT_STAT_MAGIC_RESIST_CHANGED, new { magicResist = currentMagicResist });
		}

		public float magicResistSeed = 1.5f;

		[Header("Vitality")]
		[SyncVar(hook="OnVitalityChanged")] 	public float vitality = 1f;

		void OnVitalityChanged(float currentVitality){
			EventManager.TriggerEvent (Constants.EVENT_STAT_VITALITY_CHANGED, new { vitality = currentVitality });	
		}

		[Header("Sub-vitality")]
		[SyncVar(hook="OnMaxHpChanged")]	public float maxHp;
		void OnMaxHpChanged(float currentMaxHp){
			EventManager.TriggerEvent (Constants.EVENT_STAT_MAX_HP_CHANGED, new { maxHp = currentMaxHp });
		}
		public float maxHpSeed = 3f;
		[SyncVar(hook="OnRegenerateHpChanged")]	public float regenerateHp;
		void OnRegenerateHpChanged(float currentRegenerateHp){
			EventManager.TriggerEvent (Constants.EVENT_STAT_REGENERATE_HP_CHANGED, new {regenerateHp = currentRegenerateHp});
		}
		public float regenerateHpSeed = 0.75f;

		[Header("Luck")]
		[SyncVar(hook="OnLuckChanged")]	public float luck = 1f;
		void OnLuckChanged(float currentLuck){
			EventManager.TriggerEvent (Constants.EVENT_STAT_LUCK_CHANGED, new { luck = currentLuck });
		}
		[Header("Sub-luck")]
		[SyncVar(hook="OnLuckDiceChanged")]	public float luckDice;
		void OnLuckDiceChanged(float currentLuckDice){
			EventManager.TriggerEvent (Constants.EVENT_STAT_LUCK_DICE_CHANGED, new { luckDice = currentLuckDice });
		}
		public float luckDiceSeed = 1f;
		[SyncVar(hook="OnLuckRewardChanged")]	public float luckReward;
		void OnLuckRewardChanged(float currentLuckReward){
			EventManager.TriggerEvent (Constants.EVENT_STAT_LUCK_REWARD_CHANGED, new {luckReward = currentLuckReward});
		}
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

		[Command]
		public void CmdAddPoint(StatType statType){
			if (!isServer)
				return;
			AddPoint (statType);
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

 