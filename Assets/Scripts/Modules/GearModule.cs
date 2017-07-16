using UnityEngine;
using System;

namespace Mob
{
	public class GearModule : RaceModule
	{
		public GearItem helm;
		public GearItem armor;
		public GearItem cloth;
		public GearItem weapon;
		public GearItem ring;
		public GearItem artifact;

		public override void Init ()
		{
			
		}

		public bool HasByType(GearType gearType){
			switch (gearType) {
			case GearType.Armor:
				return armor != null;
			case GearType.Artifact:
				return artifact != null;
			case GearType.Cloth:
				return cloth != null;
			case GearType.Helm:
				return helm != null;
			case GearType.Ring:
				return ring != null;
			case GearType.Weapon:
				return weapon != null;
			default:
				return false;
			}
		}

		public void Add(){
			
		}

		public void Upgrade(){
			
		}
	}
}

