﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Mob
{
	public class AddDefendChance : Affect
	{
		public float chance;

		public override void Init ()
		{
			timeToDestroy = 0f;
			AddPlugin (Effect.CreatePrimitive<AddDefendChanceEffect> (this, own, targets));
		}

		public override void Execute ()
		{
			own.GetModule<StatModule> (x => x.physicalDefend *= (1f + chance));
		}
	}

	public class AddDefendChanceEffect: Effect {
		
		public override void InitPlugin ()
		{
			use = false;
		}
		
		public override IEnumerator Define (Dictionary<string, object> effectValues)
		{
			yield return null;
		}
	}

	public class AddDefendChanceItem: Item {
		
		public float chance;

		public override void Init ()
		{
			title = "+ " + chance * 100f + "% defend";
		}

		public override bool Use (Race[] targets)
		{
			Affect.CreatePrimitiveAndUse<AddDefendChance> (own, targets, x => x.chance = chance);
			return true;
		}
	}

	public class AddDefendChanceBoughtItem: BoughtItem {
		
		public float chance;

		public override void Init ()
		{
			title = "+ " + chance * 100f + "% defend";
		}

		public override void Buy (Race who, float price = 0, int quantity = 0)
		{
			Buy<AddDefendChanceItem> (who, price, quantity, x => x.chance = chance);
		}
	}
}

