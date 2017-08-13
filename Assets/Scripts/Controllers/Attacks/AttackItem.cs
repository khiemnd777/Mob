//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Mob
//{
//	public class AttackItem : MobBehaviour
//	{
//		public Text title;
//		public Text brief;
//		public Text useTxt;
//		public Image icon;
//		public Button useBtn;
//
//		public Skill skill;
//
//		Race _player;
//		SkillModule _skillModule;
//
//		void Start(){
//			_player = Race.FindWithPlayerId (Constants.PLAYER1) [0];
//			_skillModule = _player.GetModule<SkillModule> ();
//
//			useBtn.onClick.AddListener (() => {
//				_skillModule.Use(skill, new Race[]{});
//			});
//		}
//
//		void Update(){
//			if (skill == null)
//				return;
//			useTxt.text = skill.cooldownable ? "Cooldown" : "Use";
//			useBtn.interactable = skill.interactable;
//			title.text = skill.title + "";
//			brief.text = skill.brief;
//			icon.sprite = skill.GetIcon("default") ?? skill.GetIcon("none") ?? null;
//		}
//	}
//}
//
