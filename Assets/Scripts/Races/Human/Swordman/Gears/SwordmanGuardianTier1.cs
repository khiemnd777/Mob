using UnityEngine;
using System.Collections.Generic;

namespace Mob
{
	public class SwordmanGuardianTier1 : Gear, IDodgeableChance, IAttackableAffect
	{
		public bool use;

		void Start(){
			EnoughGold(100f, () => {
				
				AddGainPoint(60f);
			});
		}

		void Update(){
			ExecuteInTurn(own, () => {
				if(_hasBurning && _burningTarget != null){
					_burningTarget.GetModule<HealthPowerModule>(hp => hp.SubtractHp(3f));
				}
				if(turn == 3){
					_hasBurning = false;
					_burningTarget = null;
				}
			});
		}

		public override bool Upgrade(){
			return EnoughGold (240f, () => {
				InstantiateFromMonoResource<SwordmanGuardianTier2> ("Races/Human/Swordman/Items/SwordmanGuardianTier2");
				Destroy(gameObject);
			});
		}

		#region IDodgeableChance implementation

		public float DodgeChance (float accuracy)
		{
			return AccuracyCalculator.GetAccuracyWithProbability (10f, accuracy);
		}

		#endregion

		#region IAttackableAffect implementation

		Race _burningTarget;
		bool[] _burningChance;
		bool _hasBurning;

		public void AssignAttackableAffect (Race target)
		{
			if(_burningChance == null){
				_burningChance = Probability.Initialize (new bool[]{ false, true }, new float[]{ 80f, 20f });
			}
			_hasBurning = Probability.GetValueInProbability (_burningChance);
			if (_hasBurning) {
				_burningTarget = target;
			}
		}

		#endregion
	}
}

