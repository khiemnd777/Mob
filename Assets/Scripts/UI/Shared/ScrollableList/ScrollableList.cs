using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mob
{
	public class ScrollableList : MonoHandler 
	{
		public Padding padding;

		RectTransform _rectTrans;
		ScrollRect _scrollRect;

		void Start(){
			_rectTrans = GetComponent<RectTransform> ();
			_scrollRect = GetComponentInParent<ScrollRect> ();

			_rectTrans.SetPivot (new Vector2 (0.5f, 1f));
			_scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
			_scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

			Init ();
		}

		public void Init(){
			Refresh ();
		}

		public void ClearAll(){
			foreach (var item in GetItems()) {
				DestroyImmediate (item.gameObject);
			}
			_rectTrans.SetHeight(0f);
		}

		ScrollableListItem[] GetItems(){
			return GetComponentsInChildren<ScrollableListItem> (false);
		}

		public void Refresh(){
			NeatlyItems ();
			_rectTrans.SetTopPosition (Vector2.zero);
		}

		void NeatlyItems () {
			ScrollableListItem prevItem = null;
			var totalItemHeight = 0f;
			var index = 0;
			var items = GetItems ();
			foreach (var item in items) {
				var itemPaddingBottom = index == items.Length  ? 0f : item.padding.Bottom;
				var itemHeight = item.GetComponent<RectTransform> ().rect.height + itemPaddingBottom;
				if (prevItem != null) {
					var nextPosition = prevItem.transform.position - new Vector3 (0f, itemHeight, 0f);
					item.transform.position = nextPosition;
				}
				prevItem = item;
				totalItemHeight += itemHeight;
				++index;
			}
			_rectTrans.SetHeight(totalItemHeight);
		}

//		void Update() {
//			NeatlyItems ();
//		}	
	}
}