using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class Main : MonoHandler 
	{
		public SkillList skillList;
		public ItemList itemList;
		public TreasureList treasureList;
		public Text damageValue;
		public Text resistanceValue;
		public Text techniqueValue;
		public Text luckValue;
		public Text hpValue;
		public Text levelValue;
		public Text gainPointValue;
		public Text goldValue;
		public Text energyValue;
		public Text goldDiceValue;
		public Text energyDiceValue;
		public Text targetHpValue;
		public Text targetLevelValue;
		public Text countdownValue;
		public Button rollDice;
		public Button endTurn;
		public Button attackBtn;

		CountdownModule cdm;
		
		void Start () {
			cdm = GetComponent<CountdownModule> ();
			cdm.RefreshAndRun ();

			BattleController.Init ();
			BattleController.playerInTurn.GetModule<BagModule> (x => itemList.SetItems (x.items.ToArray()));
			skillList.gameObject.SetActive (false);

			rollDice.onClick.AddListener(() =>{
				var goldDice = Random.Range(1, 10);
				var energyDice = Random.Range(1, 10);
				goldDiceValue.text = goldDice.ToString();
				energyDiceValue.text = energyDice.ToString();
				BattleController.playerInTurn.GetModule<GoldModule>(x => x.AddGold(goldDice));
				BattleController.playerInTurn.GetModule<EnergyModule>(x => x.AddEnergy(energyDice));
			});

			endTurn.onClick.AddListener (() => {
				BattleController.EndTurn();
				goldDiceValue.text = "0";
				energyDiceValue.text = "0";

				VisibleSkillList(false);
				BattleController.playerInTurn.GetModule<BagModule> (x => itemList.SetItems (x.items.ToArray()));
				attackBtn.GetComponentInChildren<Text>().text = "Attack";
			});

			attackBtn.onClick.AddListener (() => {
				VisibleSkillList(!skillList.isActiveAndEnabled);
				attackBtn.GetComponentInChildren<Text>().text = skillList.isActiveAndEnabled ? "Cancel attack" : "Attack";
			});
		}

		void VisibleSkillList(bool visible){
			if (visible) {
				BattleController.playerInTurn.GetModule<SkillModule> (x => skillList.SetSkills (x.skills.ToArray()));
			}
			skillList.gameObject.SetActive (visible);
		}

		void Update(){
			countdownValue.text = cdm.isEnd ? "Time up" : cdm.minutes + ":" + cdm.secondsString;

			if (BattleController.treasure != null && BattleController.treasure.Length > 0) {
				treasureList.SetItems (BattleController.treasure);
				BattleController.treasure = new BoughtItem[0];
			}
			BattleController.playerInTurn.GetModule<StatModule> (x => {
				damageValue.text = x.strength.ToString();
				resistanceValue.text = x.dexterity.ToString();
				techniqueValue.text = x.intelligent.ToString();
				luckValue.text = x.vitality.ToString();
			});
			BattleController.playerInTurn.GetModule<HealthPowerModule> (x => {
				hpValue.text = Mathf.RoundToInt(x.hp) + "/" + Mathf.RoundToInt(x.maxHp);
			});
			BattleController.playerInTurn.GetModule<GoldModule> (x => {
				goldValue.text = x.gold.ToString();
			});
			BattleController.playerInTurn.GetModule<EnergyModule> (x => {
				energyValue.text = x.energy.ToString();
			});
			BattleController.playerInTurn.GetModule<LevelModule> (x => {
				levelValue.text = x.level.ToString();
				gainPointValue.text = Mathf.RoundToInt(BattleController.playerInTurn.gainPoint) + "/" + Mathf.RoundToInt(LevelCalculator.GetPointAt(x.level + 1));
			});

			BattleController.GetTargets()[0].GetModule<HealthPowerModule> (x => {
				targetHpValue.text = Mathf.RoundToInt(x.hp) + "/" + Mathf.RoundToInt(x.maxHp);
			});

			BattleController.GetTargets()[0].GetModule<LevelModule> (x => {
				targetLevelValue.text = x.level.ToString();
			});
		}
	}	
}