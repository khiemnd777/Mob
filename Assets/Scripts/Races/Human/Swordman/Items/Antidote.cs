using System;

namespace Mob
{
	public class Antidote : Affect
	{
		void Start(){
			EnoughGold (30f, () => {
				own.GetModule<AffectModule>((a) => {
					a.RemoveAffect(m => typeof(INegativeAffect).IsAssignableFrom(m.GetType()));
				});
				AddGainPoint (4f);
				SubtractGold (30f);
			});
			Destroy(gameObject);
		}
	}
}

