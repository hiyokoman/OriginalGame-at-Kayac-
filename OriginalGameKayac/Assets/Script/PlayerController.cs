using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	CharacterController controller;
	GameObject player;
	Light frontLight, backLight;
	Vector3 move = Vector3.zero;
	Vector3 diff;
	Renderer renderer;
	readonly Vector3 Lane1 = new Vector3 (0, 0, -2); //手前のレーン
	readonly Vector3 Lane2 = new Vector3 (0, 0, 2);  //奥のレーン
	int jumpCount = 0;     //ジャンプ回数をカウント

	[SerializeField] float speed = 6.0f;     //移動速度
	[SerializeField] float jumpPower = 8.0f; //ジャンプの大きさ
	[SerializeField] float gravity = 20.0f;  //重力
	[SerializeField] int MaxJumpCount;       //ジャンプ回数上限
	[SerializeField] Material frontMaterial, backMaterial; //front用とback用のマテリアル

	void Awake () {
		controller = GetComponent<CharacterController> ();
		renderer = GetComponent<Renderer> ();
		player = GameObject.Find ("PlayerSphere");
		frontLight = transform.Find ("FrontLight").GetComponent<Light> ();
		backLight = transform.Find ("BackLight").GetComponent<Light> ();


	}

	void Update () {
		
		//プレイヤーが死んでいたらreturn
		if (player.activeSelf == null) return;
        float y = move.y;
		move = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, 0.0f);
		move *= speed;
		move.y += y;

		//方向キー↑と↓の入力によってレーン移動とマテリアル変更
		if (Input.GetKeyDown ("up")) {
			ChangeLane ();
			renderer.material = backMaterial; 
			frontLight.enabled = false;
			backLight.enabled = true;

		}else if(Input.GetKeyDown ("down")){
			ChangeLane ();
			renderer.material = frontMaterial; 
			backLight.enabled = false;
			frontLight.enabled = true;
		}

		//地面に接地した場合重力とジャンプ回数リセット
		if (controller.isGrounded) {
			move.y = 0.0f;
			jumpCount = 0;
		}

		//ジャンプ処理
		if (Input.GetKeyDown (KeyCode.Space) && jumpCount < MaxJumpCount) {
			move.y = jumpPower;
			jumpCount++;
		}

		move.y -= gravity * Time.deltaTime;  //重力処理
		controller.Move (move * Time.deltaTime); //移動処理
	}

	//レーンを変更
	void ChangeLane(){
		transform.position = new Vector3(transform.position.x,
			                             transform.position.y,
			                             Mathf.Clamp(transform.position.z + Input.GetAxisRaw("Vertical")*4, Lane1.z, Lane2.z)
			                             );
	}

	//リフトに乗ったらリフトとplayerを親子関係にする
	void OnTriggerEnter(Collider other){
		if (other.tag == "Moving")
			transform.parent = other.transform;
	}

	//リフトを離れたら親子関係を解消
	void OnTriggerExit(Collider other){
		transform.parent = null;
	}	
}
