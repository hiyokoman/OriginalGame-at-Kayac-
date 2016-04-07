using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	[SerializeField] float x, y;
	[SerializeField] float destroyTime;//トラップを消去するまでの時間
	[SerializeField] bool Static; //トラップが動くかどうか

	GameObject child;	
	GameObject inChild;
	Vector3 colliderSize;

	void Start () {
		//トラップとその子のスキンメッシュオブジェクトを検索
		child = transform.Find("trap").gameObject;  
		inChild = child.transform.Find ("baseMale").gameObject;
	}

	void Update(){
		//activeになったらトラップを動かす
		if (child.activeSelf == true) {
			child.transform.position += new Vector3 (x, y, 0) * Time.deltaTime;
			if (Static == false) Invoke ("trapOff", destroyTime); //静的ではないトラップはdestroyTime後にoff
		}
	}

	//プレイヤーがtriggerに触れたらtrap演出
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") child.SetActive (true);
	}

	//トラップを非アクティブにする
	void trapOff(){
		child.SetActive (false);
	}

	void OnDrawGizmos(){
		colliderSize = GetComponent<BoxCollider>().size;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (transform.position, colliderSize);
	}
}
