using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class StatH1ListItem : MobBehaviour
	{
		public StatType statType;
		public Text statText;
		public Text statValue;
		public Text statText1;
		public Text statValue1;
		public Text statText2;
		public Text statValue2;
		public Button addBtn;

		Race _character;
		StatModule _statModule;

		void Start(){
			addBtn.onClick.AddListener (() => {
				_statModule.CmdAddPoint(statType);
			});

			EventManager.StartListening (Constants.EVENT_STAT_POINT_CHANGED, new Action<int, uint> ((point, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				addBtn.interactable = point > 0;
			}));

			EventManager.StartListening(Constants.EVENT_STAT_STRENGTH_CHANGED, new Action<float, uint>((strength, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Strength){
					PrepareItem ("Strength", strength);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_PHYSICAL_ATTACK_CHANGED, new Action<float, uint>((physicalAttack, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Strength){
					PrepareItem1 ("Physical attack", physicalAttack);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_PHYSICAL_DEFEND_CHANGED, new Action<float, uint>((physicalDefend, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Strength){
					PrepareItem2 ("Physical defend", physicalDefend);
				}
			}));

			EventManager.StartListening(Constants.EVENT_STAT_DEXTERITY_CHANGED, new Action<float, uint>((dexterity, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Dexterity){
					PrepareItem ("Dexterity", dexterity);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_ATTACK_RATING_CHANGED, new Action<float, uint>((attackRating, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Dexterity){
					PrepareItem1 ("Attack rating", attackRating);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_CRITICAL_RATING_CHANGED, new Action<float, uint>((criticalRating, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Dexterity){
					PrepareItem2 ("Critical rating", criticalRating);
				}
			}));

			EventManager.StartListening(Constants.EVENT_STAT_INTELLIGENT_CHANGED, new Action<float, uint>((intelligent, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Intelligent){
					PrepareItem ("Intelligent", intelligent);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_MAGIC_ATTACK_CHANGED, new Action<float, uint>((magicAttack, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Intelligent){
					PrepareItem1 ("Magic attack", magicAttack);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_MAGIC_RESIST_CHANGED, new Action<float, uint>((magicResist, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Intelligent){
					PrepareItem2 ("Magic resist", magicResist);
				}
			}));

			EventManager.StartListening(Constants.EVENT_STAT_VITALITY_CHANGED, new Action<float, uint>((vitality, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Vitality){
					PrepareItem ("Vitality", vitality);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_MAX_HP_CHANGED, new Action<float, uint>((maxHp, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Vitality){
					PrepareItem1 ("Max Hp", maxHp);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_REGENERATE_HP_CHANGED, new Action<float, uint>((regenerateHp, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Vitality){
					PrepareItem2 ("Regenerate Hp", regenerateHp);
				}
			}));

			EventManager.StartListening(Constants.EVENT_STAT_LUCK_CHANGED, new Action<float, uint>((luck, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Luck){
					PrepareItem ("Luck", luck);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_LUCK_DICE_CHANGED, new Action<float, uint>((luckDice, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Luck){
					PrepareItem1 ("Luck dice", luckDice);
				}
			}));

			EventManager.StartListening (Constants.EVENT_STAT_LUCK_REWARD_CHANGED, new Action<float, uint>((luckReward, ownNetId) => {
				if(!TryToConnect())
					return;
				if(_character.netId.Value != ownNetId)
					return;
				if(statType == StatType.Luck){
					PrepareItem2 ("Luck reward", luckReward);
				}
			}));
		}

		void Update(){
			if (!TryToConnect())
				return;
			InitItems ();
		}

		bool TryToConnect(){
			return NetworkHelper.instance.TryToConnect (() => {
				if (_character != null && _statModule != null)
					return true;
				_character = Race.GetLocalCharacter ();
				if (_character == null)
					return false;
				_statModule = _character.GetModule<StatModule> ();
				return false;
			});
		}

		bool _isInitItem;

		public void InitItems(){
			if (_isInitItem)
				return;
			_isInitItem = true;
			switch (statType) {
			case StatType.Strength:
				PrepareItem ("Strength", _statModule.strength);
				PrepareItem1 ("Physical attack", _statModule.physicalAttack);
				PrepareItem2 ("Physical defend", _statModule.physicalDefend);
				break;
			case StatType.Dexterity:
				PrepareItem ("Dexterity", _statModule.dexterity);
				PrepareItem1 ("Attack rating", _statModule.attackRating);
				PrepareItem2 ("Critical rating", _statModule.criticalRating);
				break;
			case StatType.Intelligent:
				PrepareItem ("Intelligent", _statModule.intelligent);
				PrepareItem1 ("Magic attack", _statModule.magicAttack);
				PrepareItem2 ("Magic resist", _statModule.magicResist);
				break;
			case StatType.Vitality:
				PrepareItem ("Vitality", _statModule.vitality);
				PrepareItem1 ("Max Hp", _statModule.maxHp);
				PrepareItem2 ("Regenerate Hp", _statModule.regenerateHp);
				break;
			case StatType.Luck:
				PrepareItem ("Luck", _statModule.luck);
				PrepareItem1 ("Luck dice", _statModule.luckDice);
				PrepareItem2 ("Luck reward", _statModule.luckReward);
				break;
			default:
				break;
			}
		}

		void PrepareItem(string name, float value){
			statText.text = name + ":";
			statValue.text = Mathf.Floor(value).ToString();
		}

		void PrepareItem1(string name, float value){
			statText1.text = name + ":";
			statValue1.text = Mathf.Floor(value).ToString();
		}

		void PrepareItem2(string name, float value){
			statText2.text = name + ":";
			statValue2.text = Mathf.Floor(value).ToString();
		}
	}
}

