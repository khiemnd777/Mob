using UnityEngine;

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

		public void AddGold (float e)
		{
			_gold = Mathf.Min (_gold + e, maxGold);
		}

		public void SubtractGold (float e)
		{
			_gold = Mathf.Max (_gold - e, 0f);
		}
	}
}

