using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private CharacterController controller;
	private Vector3 move = Vector3.zero;
	private Vector3 Lane1;         //奥のレーン
	private Vector3 Lane2;         //手前のレーン
	private int jumpCount = 0;     //ジャンプ回数をカウント

	public float speed = 6.0f;     //移動速度
	public float jumpPower = 8.0f; //ジャンプの大きさ
	public float gravity = 20.0f;  //重力
	public int MaxJumpCount;       //ジャンプ回数上限

	void Start () {
		controller = GetComponent<CharacterController> ();
		Lane1 = new Vector3 (0, 0, -2);
		Lane2 = new Vector3 (0, 0, 2);
	}

	void Update () {
		float y = move.y;

		move = new Vector3 (Input.GetAxis ("Horizontal")*0.8f, 0.0f, 0.0f);
		move *= speed;
		move.y += y;

		//方向キー↑か↓を入力するとレーン移動
		if (Input.GetKeyDown ("up") || Input.GetKeyDown ("down")) ChangeLane ();

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

		move.y -= gravity * Time.deltaTime;      //重力処理
		controller.Move (move * Time.deltaTime); //移動処理
	}

	//レーンを変更
	void ChangeLane(){
		transform.position = new Vector3(transform.position.x,
			                             transform.position.y,
			                             Mathf.Clamp(transform.position.z + Input.GetAxisRaw("Vertical")*4, Lane1.z, Lane2.z)
			                             );
	}
}
