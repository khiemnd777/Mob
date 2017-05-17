using UnityEngine;
using System;
using System.Collections;

namespace Mob
{
	public class CountdownModule : MonoHandler
	{
		public float timer = 90f;
		public float minutes { get { return _m; } }
		public string minutesString {
			get{
				return "0" + _m;
			}
		}
		public float seconds { get { return _s; } }
		public string secondsString{
			get{
				var pre = "";
				if (_s < 10f) {
					pre = "0";
				}
				return pre + _s;
			}
		}
		public bool isEnd { get { return _isEnd; } }

		float _t;
		float _m;
		float _s;
		bool _stop;
		bool _isEnd;

		void Start(){
			_t = timer;
		}

		IEnumerator Countingdown(){
			while (_t >= 0f && !_stop) {
				_m = Mathf.Floor (_t / 60f);
				_s = Mathf.RoundToInt (_t % 60f);
				_t = Mathf.Max(0f, --_t);
				if (_isEnd)
					break;
				yield return new WaitForSeconds (1f);
				_isEnd = _t == 0f;
			}
			yield return null;
		}

		public void Run(){
			_isEnd = false;
			_stop = false;
			StartCoroutine (Countingdown ());
		}

		public void RefreshAndRun(){
			Stop ();
			_t = timer;
			Run ();
		}

		public void Stop(){
			_stop = true;
		}
	}
}

