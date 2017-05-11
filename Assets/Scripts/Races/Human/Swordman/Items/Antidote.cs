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
		
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<Antidote> (own, targets);
		}
	}
}

