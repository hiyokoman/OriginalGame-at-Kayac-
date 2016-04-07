using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour {
	
	[SerializeField] 
	private AudioClip[] voiceClip;
	AudioSource destroyVoice;	

	void Start(){
		destroyVoice = GetComponent<AudioSource>();
	}

	//死亡処理
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			other.gameObject.SetActive(false); //プレイヤー削除
			PlayVoice ();                      //ランダムでトラップのボイス再生
			Invoke("SceneMove", 1.6f);         //1.6秒後スタートに戻る
		}
	}

	void SceneMove(){
		SceneManager.LoadScene("Main");
	}

	void PlayVoice(){
		destroyVoice.clip = voiceClip [Random.Range(0, voiceClip.Length)];
		destroyVoice.Play();
	}
}
