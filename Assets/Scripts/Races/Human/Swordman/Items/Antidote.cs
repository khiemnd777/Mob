using System;

namespace Mob
{
	public class Antidote : Affect
	{
		void Start(){
			own.GetModule<AffectModule>((a) => {
				a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
			});
			AddGainPoint (4f);
			Destroy(gameObject);
		}
	}

	// Item
	public class AntidoteItem: Item {
		
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

