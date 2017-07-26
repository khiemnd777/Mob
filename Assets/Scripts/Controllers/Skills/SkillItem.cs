using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class SkillItem : MonoHandler
	{
		public Text title;
		public Text brief;
		public Image icon;
		public Button learnBtn;

		public BoughtItem boughtItem;

		Race _player;
		SkillModule _skillModule;

		void Start(){
			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
			_skillModule = _player.GetModule<SkillModule> ();

			learnBtn.onClick.AddListener (() => {
				_skillModule.PickAvailableSkill(boughtItem);
			});
		}

		void Update(){
			learnBtn.interactable = boughtItem.interactable;
			title.text = boughtItem.title;
			brief.text = boughtItem.brief;
			icon.sprite = boughtItem.GetIcon("default") ?? boughtItem.GetIcon("none");
		}
	}
}

