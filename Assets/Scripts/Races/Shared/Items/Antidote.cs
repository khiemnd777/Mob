using System;
using System.Linq;
using UnityEngine;

namespace Mob
{
	public class Antidote : Affect
	{
		public override void Init ()
		{
			timeToDestroy = 0f;
			gainPoint = 4f;
		}

		public override void Execute ()
		{
			own.GetModule<AffectModule>((a) => {
				a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
			});
		}
	}

	// Item
	public class AntidoteItem: Item, ISelfUsable {
		
		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Antidote> (own, targets);
			return true;
		}
	}

	public class AntidoteBoughtItem: BoughtItem 
	{
		public override void Init ()
		{
			title = "Antidote";
			price = 30f;
			icons.Add ("none", Resources.Load<Sprite> ("Sprites/icon"));
			icons.Add ("default", Resources.LoadAll<Sprite>("Sprites/items").FirstOrDefault(x => x.name == "antidote"));
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<AntidoteItem> (who, price, quantity, x => {
				x.title = title;
				x.icons = icons;
			});
		}

		public override void BuyAndUseImmediately (Race who, Race[] targets, float price = 0)
		{
			timeToDestroy = 5f;
			BuyAndUseImmediately<AntidoteItem> (who, targets, price, x => {
				x.title = title;
				x.timeToDestroy = 2f;
				x.icons = icons;
			});
		}
	}
}

