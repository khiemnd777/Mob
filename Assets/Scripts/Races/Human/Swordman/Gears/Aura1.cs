using System;

namespace Mob
{
	public class Aura1 : Gear
	{
		void Start(){
			EnoughGold (80f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 5f;
					s.resistance += 5f;
					s.technique += 5f;
					s.luck += 5f;
				});
				AddGainPoint(45f);
				SubtractGold(80f);
			});
		}

		public override bool Upgrade ()
		{
			return EnoughGold (180f, () => {
				InstantiateFromMonoResource<Aura2> ("Races/Human/Swordman/Gears/Aura2");
				Destroy(gameObject);
			});
		}
	}
}

