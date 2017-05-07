using UnityEngine;
using System.Linq;

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

		public static void HandleAccuracy(ref float accuracy, Race own, Race target){
			var _ = float.MinValue;
			own.GetModule<AffectModule>(am => {
				am.GetSubAffects<IAccurate>(a => {
					_ = Mathf.Max(_, a.HandleAccuracy(target));
				});
			});
			accuracy = Mathf.Max (_, accuracy);
		}

		public static void DodgeChance(ref float accuracy, Race own, Race target){
			var _ = float.MaxValue;
			var ac = accuracy;
			target.GetModule<AffectModule>(am => {
				am.GetSubAffects<IDodgeableChance>(a => {
					_ = Mathf.Min(_, a.DodgeChance(ac));
				});
			});
			accuracy = Mathf.Min (_, accuracy);
		}

		public static float GetAccuracyWithProbability(float chance, float currentAccuracy){
			// init percent of chance
			var percents = new float[] {chance, 100f - chance};
			// init accuracy values
			var accuracies = new float[] {0f, currentAccuracy};
			var arr = Probability.Initialize(accuracies, percents);
			var index = Random.Range (0, arr.Length - 1);
			return arr [index];

		}
	}
}

