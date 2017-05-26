using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class GameStarting : MonoHandler
	{
		void Start(){
			GetComponent<Button> ().onClick.AddListener (() => {
				GetMonoComponent<Main>("Main").enabled = true;
				GetMonoComponent<RectTransform>("Canvas/OverlayPanel").gameObject.SetActive(false);
			});
		}
	}
}

