using UnityEngine;
using System.Collections;

namespace Mob
{
	public class SlashLine : MonoHandler
	{
		public Transform target;
		public float maxDistance = 10f;
		public float speed = 0.1f;

		Transform _cachedTransform;
		Vector2 destination;

		void Start(){
			_cachedTransform = transform;
			_cachedTransform.parent = target.parent;
			_cachedTransform.position = Spawn (); //Random.insideUnitCircle * maxDistance + (Vector2)target.position;
			//_cachedTransform.position = Vector2.ClampMagnitude (_cachedTransform.position - target.position, maxDistance);
			destination = target.position + (target.position - _cachedTransform.position);
			Destroy (gameObject, 2f);
		}

		Vector2 Spawn(){
			while (true) {
				var p = Random.insideUnitCircle * maxDistance + (Vector2)target.position;
				if (Vector2.Distance (p, (Vector2)target.position) < Random.Range (maxDistance * 0.875f, maxDistance)) {
					continue;
				}
				return p;
			}
		}

		void FixedUpdate(){
			_cachedTransform.position = Vector2.Lerp (_cachedTransform.position, destination, speed);
			speed *= 1.1f;
		}
	}
}

