using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
	public class StatCalculator
	{
		public static float Add(float value, int point, float percent){
			var delta = point * percent / 100f;
			var addingPoint = value + delta;
			return addingPoint;
		}

		public static int GeneratePoint(int upLevel, int point){
			return point * upLevel;
		}

		public static int[] InitProbability(params float[] percentStats) {
			var random = new System.Random ();
			// init array with 100 elements;
			var arr = new int[100];
			for (var x = 0; x < arr.Length; x++) {
				var t = 0f;
				var v = 0;
				for (var y = 0; y < percentStats.Length; y++) {
					t += percentStats [y];
					if (x == t) {
						continue;
					} else if (x < t) {
						v = y;
						break;
					}
				}
				arr [x] = v;
			}
			arr = arr.OrderBy (x => random.Next ()).ToArray ();
			return arr;
		}

		public static IEnumerable<int?> GetStatWithProbability(int point, int[] arr, params float[] percentStats){
			var random = new System.Random ();
			for (var x = 0; x < point; x++) {	
				var index = Random.Range (0, arr.Length - 1);
				yield return arr [index];
			}

			yield return null;
		}

		public static IEnumerable<int?> GetStatWithProbability(int point , params float[] percentStats){
			var random = new System.Random ();
			var arr = InitProbability (percentStats);
			for (var x = 0; x < point; x++) {	
				var index = Random.Range (0, arr.Length - 1);
				yield return arr [index];
			}

			yield return null;	
		}
	}
}