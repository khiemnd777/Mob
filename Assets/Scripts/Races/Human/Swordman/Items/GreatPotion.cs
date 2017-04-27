using System;

namespace Mob
{
	public class GreatPotion : Affect
	{
		void Start(){
			EnoughGold (80f, () => {
				own.GetModule<HealthPowerModule> ((hp) => {
					hp.AddHp(150f);
				});
				AddGainPoint (8f);
				SubtractGold (80f);	
			});
			Destroy(gameObject);
		}
	}
}

