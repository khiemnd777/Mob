using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class StatListItem : MonoHandler
	{
		public StatType statType;
		public Text statText;
		public Text statValue;
		public Button addBtn;

		Race _player;
		StatModule _statModule;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_statModule = _player.GetModule<StatModule> ();

			addBtn.onClick.AddListener (() => {
				_statModule.AddPoint(statType);
			});
		}

		void Update(){
			Alternate ();
		}

		void Alternate(){
			switch (statType) {
			case StatType.Strength:
				PrepareItems ("Strength", _statModule.strength);
				break;
			case StatType.Dexterity:
				PrepareItems ("Dexterity", _statModule.dexterity);
				break;
			case StatType.Intelligent:
				PrepareItems ("Intelligent", _statModule.intelligent);
				break;
			case StatType.Vitality:
				PrepareItems ("Vitality", _statModule.vitality);
				break;
			case StatType.Luck:
				PrepareItems ("Luck", _statModule.luck);
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

