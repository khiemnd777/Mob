﻿using UnityEngine;

namespace Mob
{
	public class Tier1 : Gear
	{
		void Start(){
			EnoughGold (80f, () => {
				own.GetModule<StatModule> (s => {
					s.damage += 15f;
					s.resistance += 10f;
					s.technique += 8f;
					s.luck += 4f;
				});
				AddGainPoint(40f);
				SubtractGold(80f);
			});
		}

		public override bool Upgrade(){
			return EnoughGold (160f, () => {
				InstantiateFromMonoResource<Tier2> ("Races/Human/Swordman/Gears/Tier2");
				Destroy(gameObject);
			});
		}
	}
}

