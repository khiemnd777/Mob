using System;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class AddDamageChance : Affect
	{
		public float chance;

		public override void Init ()
		{
			timeToDestroy = 0f;
			AddPlugin (Effect.CreatePrimitive<AddDamageChanceEffect> (this, own, targets));
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.physicalAttack *= (1f + chance));
		}
	}

	public class AddDamageChanceEffect: Effect {

		public override void InitPlugin ()
		{
			use = false;
		}

		public override IEnumerator Define (Dictionary<string, object> effectValues)
		{
			yield return null;
		}
	}

	public class AddDamageChanceItem: Item {

		public float chance;

		public override void Init ()
		{
			title = "+ " + chance * 100f + "% damage";
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<AddDamageChance> (own, targets, x => x.chance = chance);
			return true;
		}
	}

	public class AddDamageChanceBoughtItem: BoughtItem {

		public float chance;

		public override void Init ()
		{
			title = "+ " + chance * 100f + "% damage";
		}

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<AddDamageChanceItem> (who, price, quantity, x => x.chance = chance);
		}
	}
}

