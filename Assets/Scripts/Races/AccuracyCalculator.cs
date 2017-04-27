using UnityEngine;

namespace Mob
{
	public class AccuracyCalculator
	{
		public static float ToPercent(float attackerTechnique, float targetEvasion){
			var acNum = (attackerTechnique * 0.4f - targetEvasion) + 9f;
			if (acNum < 0f) {
				return 0f;
			} else if (Mathf.Clamp (acNum, 0f, 0.9f) == acNum) {
				return .25f;
			} else if (Mathf.Clamp (acNum, 1f, 2f) == acNum) {
				return .3f;
			} else if (Mathf.Clamp (acNum, 3f, 4f) == acNum) {
				return .4f;
			} else if (Mathf.Clamp (acNum, 5f, 5.9f) == acNum) {
				return .5f;
			} else if (Mathf.Clamp (acNum, 6f, 6.9f) == acNum) {
				return .6f;
			} else if (Mathf.Clamp (acNum, 7f, 7.9f) == acNum) {
				return .8f; 
			} else {	
				return 1f;
			}
		}
	}
}

