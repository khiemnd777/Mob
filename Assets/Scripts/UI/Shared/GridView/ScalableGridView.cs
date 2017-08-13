using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	[Serializable]
	public enum ScalableGridType{
		Both, Width, Height
	}
	
	public class ScalableGridView : MobBehaviour
	{
		public int row, column;
		public RectTransform parent;
		public ScalableGridType scalableType;

		RectTransform _rect;
		GridLayoutGroup _grid;

		void Start(){
			_rect = GetComponent<RectTransform> ();
			_grid = GetComponent<GridLayoutGroup> ();
		}

		void Update(){
			Scale ();
		}

		public void ClearAll(){
			foreach (var item in GetItems()) {
				DestroyImmediate (item.gameObject);
			}
			(_rect ?? (_rect = GetComponent<RectTransform>())).SetHeight(0f);
		}

		public GridItem[] GetItems(){
			return GetComponentsInChildren<GridItem> (false);
		}

		public void Scale(){
			if (GetItems ().Length == 0)
				return;
			switch (scalableType) {
			default:
			case ScalableGridType.Both:
				_grid.cellSize = new Vector2 (parent.rect.width / row, parent.rect.height / column);
				break;
			case ScalableGridType.Width:
				_grid.cellSize = new Vector2 (parent.rect.width / row, parent.rect.width / row);
				break;
			case ScalableGridType.Height:
				_grid.cellSize = new Vector2 (parent.rect.height / column, parent.rect.height / column);
				break;
			}
			_rect.SetHeight(_grid.preferredHeight);
		}
	}
}

