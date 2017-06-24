using UnityEngine;
using System;
using System.Collections;

namespace Mob
{
	public sealed class PhysicalAttackingEventArgs{
		public Affect affect { get; set; }
		public Race target{ get; set; }
		public float outputDamage { get; set; }
		public bool isCritHit{get;set;}
	}
	public interface IPhysicalAttackingEventHandler {
		float bonusDamage { get; }
	}
}

