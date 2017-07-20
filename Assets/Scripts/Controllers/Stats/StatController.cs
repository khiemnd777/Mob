using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class StatController : MonoHandler
	{
		public ScrollableList list;
		public StatListItem statListItemResource;

		StatModule _statModule;
		Race _player;

		void Start() {
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_statModule = _player.GetModule<StatModule> ();
			list.ClearAll ();
			CreateItems ();
		}

		void CreateItems(){
			// Strength
			PrepareItem(StatType.Strength);
			// Dexterity
			PrepareItem(StatType.Dexterity);
			// Intelligent
			PrepareItem(StatType.Intelligent);
			// Vitality
			PrepareItem(StatType.Vitality);
			// Luck
			PrepareItem(StatType.Luck);
		}

		void PrepareItem(StatType statType){
			var ui = Instantiate<StatListItem> (statListItemResource, list.transform);
			ui.statType = statType;
		}
	}
}

