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
	
	public class ScalableGridItem : MonoHandler
	{
		public int row, column;
		public ScalableGridType scalableType;

		RectTransform _rect;
		GridLayoutGroup _grid;

		void Start(){
			_rect = GetComponent<RectTransform> ();
			_grid = GetComponent<GridLayoutGroup> ();
			Scale ();
		}

		void Update(){
			Scale ();
		}

		public void Scale(){
			switch (scalableType) {
			default:
			case ScalableGridType.Both:
				_grid.cellSize = new Vector2 (_rect.rect.width / row, _rect.rect.height / column);
				break;
			case ScalableGridType.Width:
				_grid.cellSize = new Vector2 (_rect.rect.width / row, _rect.rect.width / row);
				break;
			case ScalableGridType.Height:
				_grid.cellSize = new Vector2 (_rect.rect.height / column, _rect.rect.height / column);
				break;
			}
			//_rect.SetSize (new Vector2(_grid.preferredWidth, _grid.preferredHeight), true);
		}
	}
}

