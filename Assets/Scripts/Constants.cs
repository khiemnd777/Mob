using System;

namespace Mob
{
	public class Constants
	{
		public const float PRICE_UP_TO = 1.05f;
		public const int HUMAN_MAX_LEVEL = 16;
		public const int ORC_MAX_LEVEL = 16;
		public const int MIN_LEVEL = 2;
		public const int INIT_LEVEL = 1;
		public const float INIT_GAIN_POINT = 0f;
		public const float LERP_TIME = .5f;
		public const float WAIT_FOR_DESTROY = 10f;
		public const float TIME_EFFECT_END_DEFAULT = 3f;
		public const string TARGET_HP_LABEL = "Canvas/EnemyPanel/BodyPanel/TargetHPValue";
		public const string ATTACKER_HP_LABEL = "Canvas/CharacterInfoPanel/HPValue";
		public const string ATTACKER_GOLD_LABEL = "Canvas/CharacterInfoPanel/GoldValue";
		public const string ATTACKER_ENERGY_LABEL = "Canvas/CharacterInfoPanel/EnergyValue";
		public const string ATTACKER_GAIN_POINT_LABEL = "Canvas/CharacterInfoPanel/GainPointValue";
		public const string DECREASE_LABEL = "Prefabs/DecreaseLabel";
		public const string INCREASE_LABEL = "Prefabs/IncreaseLabel";
		public const string EFFECT_SLASH_LINE = "Effects/SlashLine";
		public const string PLAYER1 = "Player1";
		public const string PLAYER2 = "Player2";
		public const string PLAYER3 = "Player3";
		public const string PLAYER4 = "Player4";
		public const string PLAYER5 = "Player5";
		public const string PLAYER6 = "Player6";
		public const string PLAYER7 = "Player7";
		public const string PLAYER8 = "Player8"; 

		// Battle player
		public const string LOCAL_PLAYER = "LocalPlayer";
		public const string LOCAL_CHARACTER = "LocalCharacter";
		public const string SERVER_PLAYER = "Player";
		public const string OPPONENT_PLAYER = "OpponentPlayer";
		public const string OPPONENT_CHARACTER = "OpponentCharacter";

		// Event names
		public const string EVENT_ITEM_BOUGHT_FIRED = "item_bought_fired";
		public const string EVENT_ITEM_OVER_IN_BAG = "item_over_in_bag";
		public const string EVENT_SKILL_LEARNED = "skill_learned";
		public const string EVENT_TAB_CONTENT_FIRED = "fire-tab-content";
		public const string EVENT_TAB_VIEW_INIT = "init-tab-view";
		public const string EVENT_REFRESH_SYNC_SKILL = "refresh-sync-skill";
		public const string EVENT_REFRESH_SYNC_AVAILABLE_SKILL = "refresh-sync-available-skill";
		public const string EVENT_REFRESH_SYNC_LEVEL = "refresh-sync-level";
		public const string EVENT_REFRESH_SYNC_UP_LEVEL = "refresh-sync-up-level";
		public const string EVENT_REFRESH_SYNC_HP = "refresh-sync-hp";
		public const string EVENT_REFRESH_SYNC_ENERGY = "refresh-sync-energy";
		public const string EVENT_REFRESH_SYNC_GOLD = "refresh-sync-gold";
		public const string EVENT_HP_SUBTRACTING_EFFECT = "hp-subtracting-effect";
		public const string EVENT_HP_ADDING_EFFECT = "hp-adding-effect";
		public const string EVENT_ENERGY_SUBTRACTING_EFFECT = "energy-subtracting-effect";
		public const string EVENT_ENERGY_ADDING_EFFECT = "energy-adding-effect";
		public const string EVENT_GOLD_SUBTRACTING_EFFECT = "gold-subtracting-effect";
		public const string EVENT_GOLD_ADDING_EFFECT = "gold-adding-effect";
	}
}

