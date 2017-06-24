﻿using UnityEngine;
using UnityEngine.UI;
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

		public T GetMonoComponent<T>(string name, Action<T> predicate = null) where T : UnityEngine.Component{
			var go = GameObject.Find(name);
			if (go == null)
				return null;
			var component = go.GetComponent<T> ();
			if (component != null && predicate != null) {
				predicate.Invoke (component);
			}
			return component;
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
			var o = Instantiate(resource);
			var go = o as GameObject;
			foreach (var componentType in componentTypes)
			{
				go.AddComponent(componentType);
			}
			return o;
		}   

		public static T InstantiateFromMonoResource<T>(string path, Vector3 position, Quaternion rotation, params Type[] componentTypes) where T : UnityEngine.Object{
			var resource = GetMonoResource<T>(path);
			var o = Instantiate(resource, position, rotation);
			var go = o as GameObject;
			foreach (var componentType in componentTypes)
			{
				go.AddComponent(componentType);
			}
			return o;
		}

		public static T InstantiateFromMonoResource<T>(string path, Vector3 position, Quaternion rotation, Transform parent, params Type[] componentTypes) where T : UnityEngine.Object {
			var resource = GetMonoResource<T>(path);
			var o = Instantiate(resource, position, rotation, parent);
			var go = o as GameObject;
			foreach (var componentType in componentTypes)
			{
				go.AddComponent(componentType);
			}
			return o;
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
				yield return null;
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

		protected void StartHierarchy(params Func<bool>[] hierarchies){
			StartCoroutine (OnStartHierarchy(hierarchies));
		}

		protected IEnumerator OnStartHierarchy(params Func<bool>[] hierarchies){
			foreach (var hierarchy in hierarchies) {
				yield return new WaitUntil (hierarchy);
			}
			yield return null;
		}

		protected void SetTimeout(Action act, float t = .0f){
			StartCoroutine(OnSetTimeout (act, t));
		}

		protected IEnumerator OnSetTimeout(Action act, float t = .0f){
			if (act == null)
				yield return null;
			act.Invoke ();
			yield return new WaitForSeconds (t);
		}

		protected void JumpEffect(Transform target, Vector3 init, float deltaHeight = 1.5f, float deltaWeight = .25f){
			if (target == null)
				return;
			var scale = target.localScale * deltaHeight;
			StartCoroutine(OnLoadingPercent(percent => {
				if(percent <= deltaWeight)
					target.localScale = Vector3.Lerp(target.localScale, scale, percent);
				else
					target.localScale = Vector3.Lerp(target.localScale, Vector3.one, percent);
			}));
		}

		protected void While(Action<float, float> act, float step, float t = 0.5f){
			StartCoroutine (OnWhile (act, step, t));
		}

		protected IEnumerator OnWhile(Action<float, float> act, float step, float t = 0.5f){
			if (act == null)
				yield return null;
			var inc = 0f;
			var incStep = t;
			while (inc < step) {
				act.Invoke (incStep, inc);
				inc += incStep;
				yield return new WaitWhile (() => inc >= step);
				//yield return new WaitForFixedUpdate();
			}
		}

		protected void ShowSubLabel(string label, Transform target, float value, float deltaMoveUp = 25f, float deltaTime = 0.025f){
			var decreaseLabel = InstantiateFromMonoResource<Text> (label, target.position, target.rotation, target.parent);
			var l = decreaseLabel.transform.localPosition;
			l.y += deltaMoveUp;
			var targetColor = decreaseLabel.color;
			decreaseLabel.text += Mathf.RoundToInt(value);
			targetColor.a = 0f;
			StartCoroutine(OnLoadingPercent(percent => {
				decreaseLabel.color = Color.Lerp(decreaseLabel.color, targetColor, percent);
				decreaseLabel.transform.localPosition = Vector3.Lerp(decreaseLabel.transform.localPosition, l, percent);
			}, () => {
				Destroy(decreaseLabel.gameObject);
			}, deltaTime));
		}

		protected void JumpEffectAndShowSubLabel(Transform target, string label, float value, float deltaMoveUp = 25f, float deltaTime = 0.025f){
			if (target == null)
				return;
			JumpEffect (target, Vector3.one);
			ShowSubLabel (label, target, value, deltaMoveUp, deltaTime);	
		}
	}
}