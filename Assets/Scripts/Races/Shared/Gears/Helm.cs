using UnityEngine;
using System.Linq;
using System.Collections;

namespace Mob
{
	public class Helm : Affect
	{
		public float hp = 50f;

		public override void Init ()
		{
			gainPoint = 5f;
		}

		public override void Execute ()
		{
			own.GetModule<HealthPowerModule>(x => x.AddHp(hp));
		}

		public override bool Upgrade ()
		{
			++upgradeCount;
			gainPoint += upgradeCount % 3 == 0 ? Mathf.Ceil(1f * 1.175f) : 0f;
			hp *= 1.2f;
			Execute ();
			AddGainPoint ();

			return true;
		}
	}

	public class HelmItem: GearItem, ISelfUsable {
		
		public override void Init ()
		{
			upgradePrice = 40f;

			icons.Add ("lvl1", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "helm_1"));
			icons.Add ("lvl5", Resources.LoadAll<Sprite> ("Sprites/Gears").FirstOrDefault (x => x.name == "helm_5"));
			icons.Add ("lvl10", Resources.LoadAll<Sprite>("Sprites/Gears").FirstOrDefault(x => x.name == "helm_10"));
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
			if (Affect.HasAffect<Helm> (own))
				return false;
			
			Affect.CreatePrimitiveAndUse<Helm> (own, targets);
			upgradePrice *= 1.2f;
			return true;
		}

		public override bool Upgrade (float price = 0)
		{
			if (EnoughGold (own, upgradePrice)) {
				++upgradeCount;
				Affect.HasAffect<Helm> (own, (a) => {
					a.Upgrade ();
					title = "Helm lv." + (upgradeCount + 1);
					brief = "+" + Mathf.Floor(a.hp) + " hp";
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

	public class HelmBoughtItem: GearBoughtItem {
		public override void Init ()
		{
			gearType = GearType.Helm;
			title = "Helm lv.1";
			brief = "+50 hp";
			price = 40f;
		}

		public override void Buy (Race who, float price = 0f, int quantity = 0)
		{
			BuyAndUseImmediately<HelmItem> (who, new Race[]{ who }, price, a => {
				AlternateInStoreState();
				who.GetModule<GearModule> (x => {
					if(x.helm != null)
						DestroyImmediate (x.helm.gameObject);
				});
				a.title = title;
				a.brief = brief;
				a.upgradePrice = this.price;
				who.GetModule<GearModule>(x => x.helm = a);
			});
		}
	}
}

