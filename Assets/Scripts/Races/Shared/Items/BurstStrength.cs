using System;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class BurstStrength : Affect, ICritical
	{
		public override void Init ()
		{
			gainPoint = 6f;
		}

		public override void Execute ()
		{
			
		}

		public override void EmitAffect (EmitAffectArgs args)
		{
			if (typeof(IPhysicalAttackingEventHandler).IsAssignableFrom (args.affect.GetType()) 
				|| typeof(IMagicalAttackingEventHandler).IsAssignableFrom (args.affect.GetType()) ) {
				Destroy (gameObject, 2f);
			}
		}
	}

	// Item
	public class BurstStrengthItem: Item, ISelfUsable
	{	
		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<BurstStrength> (own, targets);
			return true;
		}

		protected override bool Enable ()
		{
			return !Affect.HasAffect<BurstStrength> (own);
		}
	}

	public class BurstStrengthBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Burst strength";
			price = 40f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/items").FirstOrDefault(x => x.name == "burst_strength"));
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<BurstStrengthItem> (who, price, quantity, x=>{
				x.title = title;
				x.icons = icons;
			}, () => {
				this.price *= Constants.PRICE_UP_TO;
			});
		}

		public override void BuyAndUseImmediately (Race who, Race[] targets, float price = 0)
		{
			BuyAndUseImmediately<BurstStrengthItem> (who, targets, price, x => {
				timeToDestroy = 5f;
				x.title = title;
				x.timeToDestroy = 2f;
				x.icons = icons;
			}, () => {
				this.price *= Constants.PRICE_UP_TO;
			});
		}
	}
}

