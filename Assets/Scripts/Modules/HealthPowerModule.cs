using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class HealthPowerModule : RaceModule
	{
		public float hp;
		public float maxHp;
		public float hpPercent;

		public Text hpLabel;
		public Text subHpLabel;

		public void AddHp(float p){
//			var lhp = Mathf.Min(hp + p, maxHp);
			While ((inc, step) => {
				hp = Mathf.Min(hp + inc, maxHp);
			}, p);
//			StartCoroutine(OnLoadingPercent ((percent) => {
//				hp = Mathf.Lerp (hp, lhp, percent);
//			}));
		}

		public void SubtractHp(float p){
			While ((inc, step) => {
				hp = Mathf.Max(hp - inc, 0f);
			}, p);
//			var lhp = Mathf.Max(hp - p, 0f);
//			StartCoroutine(OnLoadingPercent ((percent) => {
//				hp = Mathf.Lerp (hp, lhp, percent);
//			}));
		}

		public void SetFullHp(){
			AddHp (maxHp);
//			StartCoroutine(OnLoadingPercent ((percent) => {
//				hp = Mathf.Lerp(hp, maxHp, percent);
//			}));
		}

		public void SetMaxHp(float time = 1f, bool setFullHp = true){
			var upMaxHp = 1f;
			_race.GetModule<StatModule> (s => upMaxHp = s.maxHp);
			while (time > 0f) {
				While ((inc, step) => {
					//maxHp = Mathf.Min (maxHp + inc, upMaxHp);
					maxHp += Mathf.Clamp(inc, 0f, upMaxHp);
				}, upMaxHp);
//				maxHp += Mathf.Max(maxHp, upMaxHp);
				time--;
			}
//			StartCoroutine(OnLoadingPercent ((percent) => {
//				hp = Mathf.Lerp(hp, maxHp, percent);
//			}));
			if(setFullHp)
				SetFullHp();
//			hp = maxHp;
		}
	}
}

