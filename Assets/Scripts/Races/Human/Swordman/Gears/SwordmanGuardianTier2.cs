//using UnityEngine;
//
//namespace Mob
//{
//	public class SwordmanGuardianTier2 : Gear, IDodgeableChance, IAttackableAffect
//	{
//		public bool use;
//
//		void Start(){
//			
//		}
//
//		void Update(){
//			ExecuteInTurn(own, () => {
//				if(_hasBurning && _burningTarget != null){
//					_burningTarget.GetModule<HealthPowerModule>(hp => hp.SubtractHp(5f));
//				}
//				if(turn == 3){
//					_hasBurning = false;
//					_burningTarget = null;
//				}
//			});
//		}
//
//		public override bool Upgrade(){
//			return false;
//		}
//
//		#region IDodgeableChance implementation
//
//		public float DodgeChance (float accuracy)
//		{
//			return AccuracyCalculator.GetAccuracyWithProbability (15f, accuracy);
//		}
//
//		#endregion
//
//		#region IAttackableAffect implementation
//
//		Race _burningTarget;
//		bool[] _burningChance;
//		bool _hasBurning;
//
//		public void AssignAttackableAffect (Race target)
//		{
//			if(_burningChance == null){
//				_burningChance = Probability.Initialize (new bool[]{ false, true }, new float[]{ 80f, 20f });
//			}
//			_hasBurning = Probability.GetValueInProbability (_burningChance);
//			if (_hasBurning) {
//				_burningTarget = target;
//			}
//		}
//
//		#endregion
//	}
//}
//
