using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Mob
{
	public class MonoHandler : MonoBehaviour
	{
		public bool IsInLayerMask(int layer, LayerMask layermask)
		{
			return layermask == (layermask | (1 << layer));
		}

		public GameObject[] FindGameObjectsWithLayer (int layer) {
			var goArray = FindObjectsOfType<GameObject>();
			var goList = new List<GameObject>();
			for (var i = 0; i < goArray.Length; i++) {
				if (goArray[i].layer == layer) {
					goList.Add(goArray[i]);
				}
			}
			if (goList.Count == 0) {
				return null;
			}
			return goList.ToArray();
		}

		public GameObject[] FindGameObjectsWithLayer (LayerMask layerMask) {
			var goArray = FindObjectsOfType<GameObject>();
			var goList = new List<GameObject>();
			for (var i = 0; i < goArray.Length; i++) {
				if (IsInLayerMask(goArray[i].layer, layerMask)) {
					goList.Add(goArray[i]);
				}
			}
			if (goList.Count == 0) {
				return null;
			}
			return goList.ToArray();
		}

		public T GetMonoComponent<T>(string name) where T : UnityEngine.Component{
			return GameObject.Find(name).GetComponent<T>();
		}

		public static T GetMonoResource<T>(string path) where T : UnityEngine.Object{
			return (T) Resources.Load(path, typeof(T));
		}

		public T GetChildMonoComponent<T>(string name) where T: UnityEngine.Component{
			var t = transform.Find(name);
			if (typeof(T).IsEqual(typeof(Transform)))
			{
				return t as T;
			}
			return t.gameObject.GetComponent<T>();
		}

		private FieldInfo GetPrefabInfo<T>(){
			var t = typeof(T);
			return GetPrefabInfo(t);
		}

		private FieldInfo GetPrefabInfo(Type type){
			return type.GetField("Prefab", BindingFlags.Public | BindingFlags.Static);
		}

		public static T InstantiateFromMonoResource<T>(string path, params Type[] componentTypes) where T: UnityEngine.Object{
			var resource = GetMonoResource<T>(path);
			var o = Instantiate(resource) as GameObject;
			foreach (var componentType in componentTypes)
			{
				o.AddComponent(componentType);
			}
			return o as T;
		}   

		public static T InstantiateFromMonoResource<T>(string path, Vector3 position, Quaternion rotation, params Type[] componentTypes) where T : UnityEngine.Object{
			var resource = GetMonoResource<T>(path);
			var o = Instantiate(resource, position, rotation) as GameObject;
			foreach (var componentType in componentTypes)
			{
				o.AddComponent(componentType);
			}
			return o as T;
		}

		public static T InstantiateFromMonoResource<T>(string path, Vector3 position, Quaternion rotation, Transform parent, params Type[] componentTypes) where T : UnityEngine.Object {
			var resource = GetMonoResource<T>(path);
			var o = Instantiate(resource, position, rotation, parent) as GameObject;
			foreach (var componentType in componentTypes)
			{
				o.AddComponent(componentType);
			}
			return o as T;
		}

		public T RequireMono<T>(params Type[] componentTypes) where T : MonoHandler {
			var prefabInfo = GetPrefabInfo<T>();
			GameObject go;
			if (prefabInfo == null)
			{
				go = Instantiate(new GameObject(Guid.NewGuid().ToString()));
			}
			else
			{
				var prefabPath = (string) prefabInfo.GetValue(null);
				go = InstantiateFromMonoResource<GameObject>(prefabPath, typeof(T));   
			}
			if (go == null)
				return null;

			AssignMonoComponentTypes(go, componentTypes);

			var c = go.GetComponent<T>();
			return c;
		}

		public T RequireMono<T>(Vector3 position, Quaternion rotation, params Type[] componentTypes) where T : MonoHandler {
			var prefabInfo = GetPrefabInfo<T>();
			GameObject go;
			if (prefabInfo == null)
			{
				go = Instantiate(new GameObject(Guid.NewGuid().ToString()), position, rotation);
			}
			else
			{
				var prefabPath = (string) prefabInfo.GetValue(null);
				go = InstantiateFromMonoResource<GameObject>(prefabPath, position, rotation, typeof(T));   
			}

			if (go == null)
				return null;

			AssignMonoComponentTypes(go, componentTypes);

			var c = go.GetComponent<T>();
			return c;
		}

		public T RequireMono<T>(Vector3 position, Quaternion rotation, Transform parent, params Type[] componentTypes) where T : MonoHandler {
			var prefabInfo = GetPrefabInfo<T>();
			GameObject go;
			if (prefabInfo == null)
			{
				go = Instantiate(new GameObject(Guid.NewGuid().ToString()), position, rotation, parent);
			}
			else
			{
				var prefabPath = (string) prefabInfo.GetValue(null);
				go = InstantiateFromMonoResource<GameObject>(prefabPath, position, rotation, parent, typeof(T));
			}
			if (go == null)
				return null;

			AssignMonoComponentTypes(go, componentTypes);

			var c = go.GetComponent<T>();
			return c;
		}

		private void AssignMonoComponentTypes (GameObject go, params Type[] componentTypes){
			foreach (var componentType in componentTypes)
			{
				go.gameObject.AddComponent(componentType);
			}
		}

		protected IEnumerator OnLoadingPercent(Action<float> act, float deltaTime = 1f){
			if (act == null)
				yield return null;
			var percent = .0f;
			while (percent <= 1f)
			{
				percent += Time.fixedDeltaTime * deltaTime;
				act.Invoke(percent);
				yield return new WaitForFixedUpdate();
			}
			yield return null;
		}

		protected IEnumerator OnLoadingPercent(Action<float> act, Func<bool> cond, float deltaTime = 1f){
			if (cond == null)
				yield return null;
			if (!cond.Invoke())
				yield return null;
			if (act == null)
				yield return null;
			var percent = .0f;
			while (percent <= 1f)
			{
				if (!cond.Invoke())
					break;
				percent += Time.fixedDeltaTime * deltaTime;
				act.Invoke(percent);
				yield return new WaitForFixedUpdate();
			}
			yield return null;
		}

		protected IEnumerator OnLoadingPercent(Action<float> act, Action post, float deltaTime = 1f){
			if (act == null)
				yield return null;
			var percent = .0f;
			while (percent <= 1f)
			{
				percent += Time.fixedDeltaTime * deltaTime;
				act.Invoke(percent);
				yield return new WaitForFixedUpdate();
			}
			if (post != null)
			{
				post.Invoke();
			}
			yield return null;
		}

		protected IEnumerator OnLoadingPercent(Action<float> act, Action post, Func<bool> cond, float deltaTime = 1f){
			if (cond == null)
				yield return null;
			if (!cond.Invoke())
				yield return null;
			if (act == null)
				yield return null;
			var percent = .0f;
			while (percent <= 1f)
			{
				if (!cond.Invoke())
					break;
				percent += Time.fixedDeltaTime * deltaTime;
				act.Invoke(percent);
				yield return new WaitForFixedUpdate();
			}
			if (post != null)
			{
				post.Invoke();
			}
			yield return null;
		}

		protected IEnumerator OnLoadingPercent(Action<float> act, Action pre, Action post, float deltaTime = 1f){
			if (act == null)
				yield return null;
			if (pre != null)
			{
				pre.Invoke();
			}
			var percent = .0f;
			while (percent <= 1f)
			{
				percent += Time.fixedDeltaTime * deltaTime;
				act.Invoke(percent);
				yield return new WaitForFixedUpdate();
			}
			if (post != null)
			{
				post.Invoke();
			}
			yield return null;
		}

		protected IEnumerator OnWaiting(Action act, float t = .0f){
			if (act == null)
				yield return null;
			yield return new WaitForSeconds (t);
			act.Invoke ();
			yield return null;
		}
	}
}

