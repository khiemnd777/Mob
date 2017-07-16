using UnityEngine;
using System.Linq;
using System.Collections;

namespace Mob
{
	public class Cloth : Affect
	{
		public float magicResist = 15f;

		public override void Init ()
		{
			gainPoint = 5f;
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.magicResist += magicResist);
		}

		public override bool Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			magicResist *= 1.2f;
			Execute ();
			AddGainPoint ();

			return true;
		}
	}

	public class ClothItem: GearItem, ISelfUsable {

		public override void Init ()
		{
			upgradePrice = 40f;

			icons.Add ("lvl1", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "cloth_1"));
			icons.Add ("lvl5", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "cloth_5"));
			icons.Add ("lvl10", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "cloth_10"));
		}

		public override Sprite GetIcon(){
			if (upgradeCount >= 0 && upgradeCount < 5) {
				return GetIcon ("lvl1");	
			} else if (upgradeCount >= 5 && upgradeCount < 10) {
				return GetIcon ("lvl5");	
			} else {
				return GetIcon ("lvl10");	
			}
		}

		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Cloth> (own))
				return false;

			Affect.CreatePrimitiveAndUse<Cloth> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override bool Upgrade (float price = 0)
		{
			if (EnoughGold (own, upgradePrice)) {
				++upgradeCount;
				Affect.HasAffect<Cloth> (own, (a) => {
					a.Upgrade();
					title = "Cloth lv." + (upgradeCount + 1);
					brief = "+" + Mathf.Floor(a.magicResist) + " magic resist";
					SubtractGold (own, upgradePrice);
					upgradePrice *= 1.2f;
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

	public class ClothBoughtItem: GearBoughtItem {
		public override void Init ()
		{
			gearType = GearType.Cloth;
			title = "Cloth lv.1";
			brief = "+15 magic resist";
			price = 40f;
		}

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<ClothItem> (who, new Race[]{ who }, price, a => {
				AlternateInStoreState();
				who.GetModule<GearModule> (x => {
					if(x.cloth != null)
						DestroyImmediate (x.cloth.gameObject);
				});
				a.title = title;
				a.brief = brief;
				a.upgradePrice = this.price;
				who.GetModule<GearModule>(x => x.cloth = a);
			});
		}
	}
}

