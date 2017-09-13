using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob 
{
	public class LeftPanelController : MobBehaviour {
		public Button statsGroupBtn;
		public Button shopGroupBtn;
		public Button skillGroupBtn;

		public RectTransform statsGroup;
		public RectTransform shopGroup;
		public RectTransform skillGroup;

		RectTransform _parent;

		void Start(){
			statsGroupBtn.onClick.AddListener (() => {
				statsGroup.SetHeight(statsGroup.parent.GetComponent<RectTransform>().rect.height);
				statsGroup.SetPositionOfPivot (Vector2.zero);
			});
			shopGroupBtn.onClick.AddListener (() => {
				shopGroup.SetHeight(shopGroup.parent.GetComponent<RectTransform>().rect.height);
				shopGroup.SetPositionOfPivot (Vector2.zero);
			});
			skillGroupBtn.onClick.AddListener (() => {
				skillGroup.SetHeight(skillGroup.parent.GetComponent<RectTransform>().rect.height);
				skillGroup.SetPositionOfPivot (Vector2.zero);
			});

			HideAllGroups ();
		}

		void HideAllGroups() {
			MathfLerp (statsGroup.parent.GetComponent<RectTransform> ().rect.height, 0f, r => {
				statsGroup.SetHeight (r);	
				statsGroup.SetPositionOfPivot (new Vector2(0f, -(statsGroup.parent.GetComponent<RectTransform> ().rect.height - r) * statsGroup.pivot.y));
			}, 0.5f);
//			statsGroup.SetHeight (0f);
//			statsGroup.SetPositionOfPivot (new Vector2(0f, -statsGroup.parent.GetComponent<RectTransform>().rect.height * statsGroup.pivot.y));

			shopGroup.SetHeight (0f);
			shopGroup.SetPositionOfPivot (new Vector2(0f, -shopGroup.parent.GetComponent<RectTransform>().rect.height * shopGroup.pivot.y));

			skillGroup.SetHeight (0f);
			skillGroup.SetPositionOfPivot (new Vector2(0f, -skillGroup.parent.GetComponent<RectTransform>().rect.height * skillGroup.pivot.y));
		}
	}
}