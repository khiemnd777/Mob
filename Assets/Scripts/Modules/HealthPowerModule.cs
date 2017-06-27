using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class HealthPowerModule : RaceModule
	{
		public float hp;
		public float hpEffect;
		public float maxHp;
		public float maxHpEffect;
		public float hpPercent;

		public override void Init ()
		{
			hpEffect = hp;
			maxHpEffect = maxHp;
		}

		public void AddHp(float p){
			hp = Mathf.Min(hp + p, maxHp);
		}

		public void AddHpEffect(){
			MathfLerp (hpEffect, hp, p => hpEffect = p);
//			While ((inc, step) => {
//				hpEffect = Mathf.Clamp(hpEffect + inc, hp, maxHp);
//			}, p);
		}

		public void SubtractHp(float p){
			hp = Mathf.Max(hp - p, 0f);
		}

		public void SubtractHpEffect(){
//			var p = hpEffect - hp;
			MathfLerp (hpEffect, hp, (p) => hpEffect = p);
//			While ((inc, step) => {
//				hpEffect = Mathf.Max(hpEffect - inc, 0f);
//			}, p);
		}

		public void SetFullHp(){
			AddHp (maxHp);
		}

		public void SetFullHpEffect(){
			AddHpEffect ();
		}

		public void SetMaxHp(float time = 1f, bool setFullHp = true){
			var upMaxHp = 1f;
			_race.GetModule<StatModule> (s => upMaxHp = s.maxHp);
			while (time > 0f) {
				maxHp += Mathf.Max(maxHp, upMaxHp);
				time--;
			}
			if(setFullHp)
				SetFullHp();
		}

		public void SetMaxHpEffect(bool setFullHp = true){
			While ((inc, step) => {
				maxHpEffect = Mathf.Min(maxHpEffect + inc, maxHp);
			}, maxHp - maxHpEffect);
			if(setFullHp)
				SetFullHpEffect();
		}
	}
}

