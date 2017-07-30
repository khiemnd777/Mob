using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class AttackController : MonoHandler
	{
		public ScrollableList list;
		public AttackItem skillItemResource;

		Race _player;
		SkillModule _skillModule;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_skillModule = _player.GetModule<SkillModule> ();
			list.ClearAll ();

			EventManager.StartListening(Constants.EVENT_SKILL_LEARNED, new System.Action(PrepareItems));
		}

		void PrepareItems(){
			list.ClearAll ();
			foreach (var item in _skillModule.skills) {
				PrepareItem (item);
			}
			list.Refresh ();
		}

		void PrepareItem(Skill skill){
			var ui = Instantiate<AttackItem> (skillItemResource, list.transform);
			ui.skill = skill;
		}
	}
}