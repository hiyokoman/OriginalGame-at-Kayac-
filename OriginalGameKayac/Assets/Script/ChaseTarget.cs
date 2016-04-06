using UnityEngine;
using System.Collections;

public class ChaseTarget : MonoBehaviour {

	[SerializeField]
	private GameObject target;
	Vector3 distance; //カメラとtargetの距離
	Vector3 cameraP;  //カメラの位置

	void Start () {
		distance = transform.position - target.transform.position;
	}

	void Update () {
		
		cameraP.x = target.transform.position.x + distance.x;
		cameraP.y = target.transform.position.y + distance.y;

		transform.position = new Vector3 (cameraP.x, transform.position.y/*cameraP.y*/, transform.position.z);
	}
}
