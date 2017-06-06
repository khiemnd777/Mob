﻿using System;

namespace Mob
{
	public class Antidote : Affect
	{
		public override float gainPoint {
			get {
				return 4f;
			}
		}

		void Start(){
			own.GetModule<AffectModule>((a) => {
				a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
			});
			Destroy(gameObject, Constants.WAIT_FOR_DESTROY);
		}
	}

	// Item
	public class AntidoteItem: Item, ISelfUsable {
		
		public override string title {
			get {
				return "Antidote";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<Antidote> (own, targets);
			return true;
		}
	}

	public class AntidoteBoughtItem: BoughtItem 
	{

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<AntidoteItem> (who, price, quantity);
		}
	}
}

