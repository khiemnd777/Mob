using UnityEngine;

namespace Mob
{
	public class BlessingOfAmphitrite: Affect, IMissingHandler
	{
		void Start(){
//			HasAffect<BlessingOfAmphitrite> (own, () => {
//				Destroy(gameObject);
//			});
//			own.GetModule<StatModule>(s => s.technique += 10f);
		}

		#region IMissingHandler implementation

		public void HandleMissing (float damage, Race target)
		{
			target.GetModule<HealthPowerModule>(hp => hp.SubtractHp(damage * .5f));
		}

		#endregion
	}

	public class BlessingOfAmphitriteItem: Item, ISelfUsable
	{
		public override string title {
			get {
				return "Blessing of Amphitrite";
			}
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<BlessingOfAmphitrite> (own, targets);
			return true;
		}
	}

	public class BlessingOfAmphitriteBoughtItem: BoughtItem 
	{
		public override string title {
			get {
				return "Blessing of Amphitrite";
			}
		}

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<BlessingOfAmphitriteItem> (who, price, quantity);
		}
	}
}