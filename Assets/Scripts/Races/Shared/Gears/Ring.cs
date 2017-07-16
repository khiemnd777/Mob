using UnityEngine;
using System;
using System.Linq;

namespace Mob
{
	public class Ring : Affect
	{
		public override void Init ()
		{
			gainPoint = 6f;
		}

		public float point = 1f;

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => {
				x.strength += point;
				x.dexterity += point;
				x.intelligent += point;
				x.vitality += point;
				x.luck += point;
			});
		}

		public override bool Upgrade ()
		{
			++upgradeCount;
			if (upgradeCount == 1) {
				gainPoint = 8f;
				point = 3f;
			}
			if (upgradeCount == 2) {
				gainPoint = 12f;
				point = 6f;
			}
			Execute ();
			AddGainPoint ();

			return true;
		}
	}

	public class RingItem: GearItem, ISelfUsable {
		
		public override void Init ()
		{
			upgradePrice = 50f;

			icons.Add ("lvl1", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "ring_1"));
			icons.Add ("lvl2", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "ring_2"));
			icons.Add ("lvl3", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "ring_3"));
		}

		public override Sprite GetIcon(){
			if (upgradeCount >= 0 && upgradeCount < 2) {
				return GetIcon ("lvl1");	
			} else if (upgradeCount == 2) {
				return GetIcon ("lvl2");	
			} else {
				return GetIcon ("lvl3");	
			}
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Ring> (own))
				return false;

			Affect.CreatePrimitiveAndUse<Ring> (own, targets);
			upgradePrice = 80f;
			return true;
		}

		public override bool Upgrade (float price = 0)
		{
			if (EnoughGold (own, upgradePrice)) {
				++upgradeCount;
				Affect.HasAffect<Ring> (own, (a) => {
					a.Upgrade();
					title = "Ring lv." + (upgradeCount + 1);
					brief = "+" + Mathf.Floor(a.point) + " all stats";
					SubtractGold (own, upgradePrice);
					if(upgradeCount == 1){
						upgradePrice = 120f;
					}
				});
				var addingItem = GetRandomItem ();
				if (addingItem) {
					brief += ", " + addingItem.brief;
				}
				return true;
			}
			return false;
		}
	}

	public class RingBoughtItem: GearBoughtItem {
		public override void Init ()
		{
			gearType = GearType.Ring;
			title = "Ring lv.1";
			brief = "+1 all stats";
			price = 50f;
		}

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<RingItem> (who, new Race[]{ who }, price, a => {
				AlternateInStoreState();
				who.GetModule<GearModule> (x =>{
					if(x.ring != null)
						DestroyImmediate (x.ring.gameObject);
				});
				a.title = title;
				a.brief = brief;
				a.upgradePrice = this.price;
				who.GetModule<GearModule>(x => x.ring = a);
			});
		}
	}
}

