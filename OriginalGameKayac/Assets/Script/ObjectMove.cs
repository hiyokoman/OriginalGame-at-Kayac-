using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {
	
	public float speed;
	public float amplitude;

	Vector3 startPosition;
	Vector3 diff;

	void Start () {
		startPosition = transform.position;
	}

	void Update () {
		float x = amplitude * Mathf.Sin (Time.time * speed);
		transform.position = startPosition + new Vector3 (x, 0, 0);
	}
}
