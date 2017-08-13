using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Linq;

namespace Mob
{
	public class SkillController : MobBehaviour
	{
		public ScrollableList list;
		public SkillItem skillItemResource;

		Race _character;
		SkillModule _skillModule;

		void Start(){
			list.ClearAll ();
			EventManager.StartListening (Constants.EVENT_SKILL_LEARNED, new Action (RefreshItems));
		}

		void Update(){
			if (!NetworkHelper.instance.TryToConnect (() => {
				if (_character != null && _skillModule != null)
					return true;
				_character = Race.GetLocalCharacter ();
				if(_character == null)
					return false;
				_skillModule = _character.GetModule<SkillModule>();
				return false;
			}))
				return;

			CreateItems ();
			RefreshItems ();
		}

		bool isCreateItems;
		void CreateItems(){
			if (!isCreateItems) {
				isCreateItems = true;
				foreach (var item in _skillModule.networkAvailableSkills) {
					PrepareItems (item);
				}
				list.Refresh ();
			}
		}

		void RefreshItems(){
			var itemUIs = list.GetItems ().Select (x => x.GetComponent<SkillItem> ()).ToArray();
			foreach (var item in itemUIs) {
				if (!_skillModule.networkAvailableSkills.Any (x => item.boughtItem.Equals (x))) {
					DestroyImmediate (item.gameObject);
				}
			}
			foreach (var item in _skillModule.networkAvailableSkills) {
				if(!itemUIs.Any(x => item.Equals(x.boughtItem))){
					PrepareItems (item);
					continue;
				}
				var itemUI = itemUIs.FirstOrDefault (x => item.Equals (x.boughtItem));
				itemUI.boughtItem = item;
				itemUI.PrepareItem ();
			}
		}

		void PrepareItems(SyncSkillBoughtItem boughtItem){
			var ui = Instantiate<SkillItem> (skillItemResource, list.transform);
			ui.boughtItem = boughtItem;
			ui.PrepareItem ();
		}
	}
}