using UnityEngine;
using System.Linq;
using System.Collections;

namespace Mob
{
	public class Sword : Affect
	{
		public float damage = 20f;

		public override void Init ()
		{
			gainPoint = 6f;
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.physicalAttack += damage);
		}

		public override bool Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			damage *= 1.2f;
			Execute ();
			AddGainPoint ();

			return true;
		}
	}

	public class SwordItem: GearItem, ISelfUsable {

		public override void Init ()
		{
			upgradePrice = 50f;

			icons.Add ("lvl1", Resources.LoadAll<Sprite> ("Sprites/Gears").FirstOrDefault(x => x.name == "sword_1"));
			icons.Add ("lvl5", Resources.LoadAll<Sprite> ("Sprites/Gears").FirstOrDefault(x => x.name == "sword_5"));
			icons.Add ("lvl10", Resources.LoadAll<Sprite> ("Sprites/Gears").FirstOrDefault(x => x.name == "sword_10"));
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
			if (Affect.HasAffect<Sword> (own))
				return false;

			Affect.CreatePrimitiveAndUse<Sword> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override bool Upgrade (float price = 0)
		{
			if (EnoughGold (own, upgradePrice)) {
				++upgradeCount;
				Affect.HasAffect<Sword> (own, (a) => {
					a.Upgrade();
					title = "Sword lv." + (upgradeCount + 1);
					brief = "+" + Mathf.Floor(a.damage) + " damage";
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

	public class SwordBoughtItem: GearBoughtItem {
		public override void Init ()
		{
			gearType = GearType.Weapon;
			title = "Sword lv.1";
			brief = "+20 damage";
			price = 50f;
		}

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<SwordItem> (who, new Race[]{ who }, price, a => {
				AlternateInStoreState();
				who.GetModule<GearModule> (x => {
					if(x.weapon != null)
						DestroyImmediate (x.weapon.gameObject);
				});
				a.title = title;
				a.brief = brief;
				a.upgradePrice = this.price;
				who.GetModule<GearModule>(x => x.weapon = a);
			});
		}
	}
}

