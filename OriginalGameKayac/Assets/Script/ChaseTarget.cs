using UnityEngine;
using System.Collections;

public class ChaseTarget : MonoBehaviour {

	public GameObject target;
	Vector3 distance, cameraP; 

	void Start () {
		distance = transform.position - target.transform.position;
	}

	void Update () {
		
		cameraP.x = target.transform.position.x + distance.x;
		cameraP.y = target.transform.position.y + distance.y;

		transform.position = new Vector3 (cameraP.x, transform.position.y/*cameraP.y*/, transform.position.z);
	}
}
