using System;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class Speedy : Affect, IDodgeableChance
	{
		#region IDodgeableChance implementation

		public float dodgeChance {
			get {
				return 1f;
			}
		}

		#endregion

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
	public class SpeedyItem: Item, ISelfUsable
	{	
		public override bool Use (Race[] targets)
		{
			if (Affect.HasAffect<Speedy> (own)) {
				return false;
			}
			Affect.CreatePrimitive<Speedy> (own, targets);
			return true;
		}
	}

	public class SpeedyBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Speedy";
			price = 40f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/items").FirstOrDefault(x => x.name == "speedy"));
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<SpeedyItem> (who, price, quantity, x => {
				x.title = title;
				x.icons = icons;
			});
		}

		public override void BuyAndUseImmediately (Race who, Race[] targets, float price = 0)
		{
			timeToDestroy = 5f;
			BuyAndUseImmediately<SpeedyItem> (who, targets, price, x => {
				x.title = title;
				x.timeToDestroy = 2f;
				x.icons = icons;
			});
		}
	}
}

