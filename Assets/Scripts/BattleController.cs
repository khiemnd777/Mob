using UnityEngine;
using System.Linq;

namespace Mob
{
	public class BattleController
	{
		public static Race[] players;
		public static Race playerInTurn;
		public static BoughtItem[] treasure;

		public static void Init(){
			// Init players in a battle
			Race.Create<Swordman> ("Races/Human/Swordman", (p1) => {
				p1.tag = Constants.PLAYER1;
				p1.name = Constants.PLAYER1;
				p1.DefaultValue ();
			});

			Race.Create<Swordman> ("Races/Human/Swordman", (p2) => {
				p2.tag = Constants.PLAYER2;
				p2.name = Constants.PLAYER2;
				p2.DefaultValue ();
			});

			players = Race.FindWithPlayerId(Constants.PLAYER1, Constants.PLAYER2);

			// Init treasure system
			Treasure.Init ();

			// Init first player in turn
			playerInTurn = GetNextPlayer();
		}

		public static void EndTurn(){
			if (playerInTurn == null)
				return;
			// current player ends turn
			playerInTurn.EndTurn ();

			// next player starts sturn
			playerInTurn = GetNextPlayer();
			playerInTurn.StartTurn ();
		}

		public static void Attack(){
			if (playerInTurn == null)
				return;
			var targets = GetTargets ();
			playerInTurn.Attack (targets, null);
		}

		public static Race[] GetTargets(){
			return players.Where (x => !x.tag.Equals (playerInTurn.tag)).ToArray ();
		}

		static Race GetNextPlayer(){
			if (playerInTurn == null) {
				return players [Random.Range(0, players.Length - 1)];
			}
			for (var i = 0; i < players.Length; i++) {
				var player = players [i];
				if (!player.tag.Equals (playerInTurn.tag))
					continue;
				if (i == players.Length - 1) {
					return players [0];
				}
				return players [i + 1];
			}
			return null;
		}
	}
}

