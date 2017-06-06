﻿using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class GoldModule : RaceModule
	{
		public float maxGold;

		float _gold;

		public float gold {
			get{
				return _gold;
			}
		}

		public float goldLabel;

		public void AddGold (float e)
		{
			e *= 10f;
			_gold = Mathf.Min (_gold + e, maxGold);
			While ((inc, step) => {
				goldLabel = Mathf.Min(goldLabel + inc, maxGold);
			}, e, 1f);
		}

		public void SubtractGold (float e)
		{
			_gold = Mathf.Max (_gold - e, 0f);
			While ((inc, step) => {
				goldLabel = Mathf.Max(goldLabel - e, 0f);
			}, e, 1f);

			var goldValue = GetMonoComponent<Text> (Constants.ATTACKER_GOLD_LABEL);
			JumpEffect (goldValue.transform, Vector3.one);
			ShowSubLabel (Constants.DECREASE_LABEL, goldValue.transform, e);
		}
	}
}

