using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace Mob
{
	public class ItemList : MonoHandler
	{
		public Item[] items;
		public Button[] children;
		public RectTransform scrollPanel;
		public RectTransform listItem;

		void Start(){
			Clear ();
		}

		public void Clear(){
			children = scrollPanel.GetComponentsInChildren<Button> ();
			foreach (var child in children) {
				Destroy (child.gameObject);
			}
			children = new Button[0];
		}

		public void SetItems(Item[] items){
			Clear ();
			this.items = items;
			foreach (var item in this.items) {
				var name = "(" + item.quantity + ") " + item.title;
				var li = Instantiate (listItem, scrollPanel.transform);
				var text = li.GetComponentInChildren<Text> ();
				text.text = name;
				var btn = li.GetComponent<Button> ();
				btn.onClick.AddListener(() => {
					BattleController.playerInTurn.GetModule<InventoryModule>(x => {
						x.Use(item, BattleController.GetTargets());
						SetItems(x.items.ToArray());
					});
				});
			}
		}
	}
}

