using UnityEngine;

namespace Mob
{
	public class HealthPowerModule : RaceModule
	{
		public float hp;
		public float maxHp;
		public float hpPercent;

		public void AddHp(float p){
			hp = Mathf.Min(hp + p, maxHp);
		}

		public void SubtractHp(float p){
			hp = Mathf.Max(hp - p, 0f);
		}

		public void SetFullHp(){
			hp = maxHp;
		}

		public void SetMaxHp(float time = 1f){
			var upMaxHp = 1f;
			_race.GetModule<StatModule> (s => upMaxHp = s.maxHp);
			while (time > 0f) {
				maxHp += Mathf.Max(maxHp, upMaxHp);
				time--;
			}
			hp = maxHp;
		}
	}
}

