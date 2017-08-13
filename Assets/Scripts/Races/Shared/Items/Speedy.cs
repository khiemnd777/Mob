﻿using System;
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
			Affect.CreatePrimitiveAndUse<Speedy> (own, targets);
			return true;
		}

		protected override bool Interact ()
		{
			return !Affect.HasAffect<Speedy> (own) && EnoughEnergy () && EnoughLevel () && EnoughCooldown (); ;
		}
	}

	public class SpeedyBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Speedy";
			price = 40f;

			icon.prefabs.Add ("none", "Sprites/icon");
			icon.prefabs.Add ("default", "Sprites/items => speedy");
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<SpeedyItem> (who, price, quantity, x => {
				x.title = title;
				x.icon = icon;
			}, () => {
				this.price *= Constants.PRICE_UP_TO;
			});
		}

		public override void BuyAndUseImmediately (Race who, Race[] targets, float price = 0)
		{
			BuyAndUseImmediately<SpeedyItem> (who, targets, price, x => {
				timeToDestroy = 5f;
				x.title = title;
				x.timeToDestroy = 2f;
				x.icon = icon;
			}, () => {
				this.price *= Constants.PRICE_UP_TO;
			});
		}
	}
}

