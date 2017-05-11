using UnityEngine;

namespace Mob
{
	public class GaiaCell : Affect, IRestorableHealthPower
	{
		void Start(){
			HasAffect<GaiaCell> (own, () => {
				Destroy(gameObject);
			});
			own.GetModule<StatModule>(s => s.resistance += 10f);
		}

		#region IRestorableHealthPower implementation

		public float RestoreHealthPower (float hp)
		{
			return hp + hp * .5f;
		}

		#endregion
	}

	public class GaiaCellItem: Item
	{
		public override void Use (Race[] targets)
		{
			Affect.CreatePrimitive<GaiaCell> (own, targets);
		}
	}
}

