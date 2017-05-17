using System;

namespace Mob
{
	public class SwordmanAura1 : Gear, ICriticalHandler
	{
		void Start(){
			own.GetModule<StatModule> (s => {
				s.damage += 5f;
				s.resistance += 5f;
				s.technique += 5f;
				s.luck += 5f;
			});
			AddGainPoint(45f);
//			EnoughGold (80f, () => {
//				
//				SubtractGold(80f);
//			});
		}

		public override bool Upgrade ()
		{
			return EnoughGold (180f, () => {
				InstantiateFromMonoResource<SwordmanAura2> ("Races/Human/Swordman/Gears/Aura2");
				Destroy(gameObject);
			});
		}

		#region ICriticalHandler implementation

		public float HandleCriticalDamage (float damage, float accuracy, Race target)
		{
			return damage * 2f;
		}

		#endregion
	}

	public class SwordmanAura1Item: Item
	{
		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<SwordmanAura1> (own, targets);
			return true;
		}
	}

	public class SwordmanAura1BoughtItem: BoughtItem
	{
		public override void Buy (Race who, float price, int quantity)
		{
			Buy<SwordmanAura1Item> (who, price, quantity);
		}
	}
}

