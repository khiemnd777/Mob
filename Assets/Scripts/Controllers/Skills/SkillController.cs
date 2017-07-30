using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class SkillController : MonoHandler
	{
		public ScrollableList list;
		public SkillItem skillItemResource;

		Race _player;
		SkillModule _skillModule;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_skillModule = _player.GetModule<SkillModule> ();
			list.ClearAll ();
			InitItems ();
		}

		void InitItems(){
			list.ClearAll ();
			foreach (var item in _skillModule.availableSkills) {
				PrepareItems (item);
			}
			list.Refresh ();
		}

		void PrepareItems(SkillBoughtItem boughtItem){
			var ui = Instantiate<SkillItem> (skillItemResource, list.transform);
			ui.boughtItem = boughtItem;
		}
	}
}