using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class Stat2ndController : MonoHandler
	{
		public ScrollableList list;
		public Stat2ndListItem stat2ndListItemResource;

		void Start() {
			list.ClearAll ();
			CreateItems ();
		}

		void CreateItems(){
			// Strength
			PrepareItem(Stat2ndType.PhysicalAttack);
			PrepareItem(Stat2ndType.PhysicalDefend);
			// Dexterity
			PrepareItem(Stat2ndType.AttackRating);
			PrepareItem(Stat2ndType.CriticalRating);
			// Intelligent
			PrepareItem(Stat2ndType.MagicAttack);
			PrepareItem(Stat2ndType.MagicResist);
			// Vitality
			PrepareItem(Stat2ndType.MaxHp);
			PrepareItem(Stat2ndType.RegenerateHp);
			// Luck
			PrepareItem(Stat2ndType.LuckDice);
			PrepareItem(Stat2ndType.LuckReward);

			list.Refresh ();
		}

		void PrepareItem(Stat2ndType stat2ndType){
			var ui = Instantiate<Stat2ndListItem> (stat2ndListItemResource, list.transform);
			ui.stat2ndType = stat2ndType;
		}
	}
}

