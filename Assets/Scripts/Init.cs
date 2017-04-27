using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mob
{
	public class Init : MonoHandler 
	{
		void Start () {
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
		}
	}	
}