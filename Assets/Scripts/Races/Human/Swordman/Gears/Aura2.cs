using UnityEngine;

namespace Mob
{
	public class Aura2 : Gear
	{
		void Start(){
			EnoughGold (180f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 15f;
					s.resistance += 15f;
					s.technique += 15f;
					s.luck += 15f;
				});
				AddGainPoint(80f);
				SubtractGold(180f);
			});
		}

		public override bool Upgrade ()
		{
			return false;
		}
	}
}

