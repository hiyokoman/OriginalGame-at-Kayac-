using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	[SerializeField]
	private float x, y;
	bool Switch = false;
	GameObject child;	
	GameObject inChild;
	Vector3 colliderSize;

	void Start () {
		//トラップとその子のスキンメッシュオブジェクトを検索
		child = transform.Find("trap").gameObject;  
		inChild = child.transform.Find ("baseMale").gameObject;
	}

	void Update(){
		//Switchがtrue2なったらトラップを動かす
		if (Switch == true) {
			child.transform.position += new Vector3(x, y, 0) * Time.deltaTime;
			Destroy (child, 3.0f);
		}	
	}

	//プレイヤーがtriggerに触れたらtrap演出
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Switch = true;
			child.GetComponent<Animation> ().enabled = true;
			inChild.GetComponent<SkinnedMeshRenderer> ().enabled = true;
		}
	}

	void OnDrawGizmos(){
		colliderSize = GetComponent<BoxCollider>().size;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (transform.localPosition, colliderSize);
	}
}
