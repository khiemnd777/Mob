using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Mob
{
	public class HealthPowerRestoring : Affect
	{
		public float extraHp;

		Text attackerHpLabel;

		void Start(){
			attackerHpLabel = GetMonoComponent<Text> (Constants.ATTACKER_HP_LABEL);

			own.GetModule<HealthPowerModule>(hp => {
				var _ = float.MinValue;
				own.GetModule<AffectModule>(am => {
					am.GetSubAffects<IRestorableHealthPower>(a => {
						_ = Mathf.Max(_, a.RestoreHealthPower(extraHp));
					});
				});
				extraHp = Mathf.Max (_, extraHp);
				hp.AddHp(extraHp);

				JumpEffect (attackerHpLabel.transform, Vector3.one);

				ShowSubLabel (Constants.INCREASE_LABEL, attackerHpLabel.transform, extraHp);
			});

			Destroy (gameObject, Constants.WAIT_FOR_DESTROY);
		}
	}

	public class HealthPowerRestoringItem: Item
	{
		public override string title {
			get {
				return "+" + extraHp + " HP";
			}
		}

		public float extraHp;

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitive<HealthPowerRestoring> (own, targets, hp => hp.extraHp = extraHp);
			return true;
		}
	}

	public class HealthPowerRestoringBoughtItem: BoughtItem
	{
		public override string title {
			get {
				return "+" + extraHp + " HP";
			}
		}

		public float extraHp;

		public override void Buy (Race who, float price, int quantity)
		{
			Buy<HealthPowerRestoringItem> (who, price, quantity, e => e.extraHp = extraHp);
		}
	}
}

