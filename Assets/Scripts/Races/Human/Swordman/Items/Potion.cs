using System;

namespace Mob
{
	public class Potion : Affect
	{
		void Start(){
			EnoughGold (40f, () => {
				own.GetModule<HealthPowerModule> ((hp) => {
					hp.AddHp(50f);
				});
				AddGainPoint (5f);
				SubtractGold (40f);	
			});
			Destroy(gameObject);
		}
	}
}

