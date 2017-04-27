using System;

namespace Mob
{
	public class Speedy : Affect
	{
		public bool use;

		void Start(){
			if(HasAffect<Speedy>(own)){
				Destroy (gameObject);
				return;
			}
			EnoughGold (40f, () => {
				AddGainPoint (6f);
				SubtractGold (40f);
			});
		}

		void Update(){
			if (use) {
				Destroy (gameObject);
			}
		}
	}
}

