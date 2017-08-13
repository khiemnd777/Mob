using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class Stat2ndListItem : MobNetworkBehaviour
	{
		public Stat2ndType stat2ndType;
		public Text statText;
		public Text statValue;

		BattlePlayer _player;
		Race _character;
		StatModule _statModule;

		void Update(){
			if (!NetworkHelper.instance.TryToConnect (() => {
				if (_character != null && _statModule != null)
					return true;
				_character = Race.GetLocalCharacter ();
				if(_character == null)
					return false;
				_statModule = _character.GetModule<StatModule> ();
				return false;
			}))
				return;
			Alternate ();
		}

		void Alternate(){
			switch (stat2ndType) {
			case Stat2ndType.PhysicalAttack:
				PrepareItems ("Physical attack", _statModule.physicalAttack);
				break;
			case Stat2ndType.PhysicalDefend:
				PrepareItems ("Physical defend", _statModule.physicalDefend);
				break;
			case Stat2ndType.AttackRating:
				PrepareItems ("Attack rating", _statModule.attackRating);
				break;
			case Stat2ndType.CriticalRating:
				PrepareItems ("Critical rating", _statModule.criticalRating);
				break;
			case Stat2ndType.MagicAttack:
				PrepareItems ("Magic attack", _statModule.magicAttack);
				break;
			case Stat2ndType.MagicResist:
				PrepareItems ("Magic resist", _statModule.magicResist);
				break;
			case Stat2ndType.MaxHp:
				PrepareItems ("Max Hp", _statModule.maxHp);
				break;
			case Stat2ndType.RegenerateHp:
				PrepareItems ("Regenerate Hp", _statModule.regenerateHp);
				break;
			case Stat2ndType.LuckDice:
				PrepareItems ("Luck dice", _statModule.luckDice);
				break;
			case Stat2ndType.LuckReward:
				PrepareItems ("Luck reward", _statModule.luckReward);
				break;
			default:
				break;
			}
		}

		void PrepareItems(string name, float value){
			statText.text = name + ":";
			statValue.text = Mathf.Floor(value).ToString();
		}
	}
}

