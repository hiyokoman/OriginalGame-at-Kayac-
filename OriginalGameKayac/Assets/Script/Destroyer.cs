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
			
			Destroy (GameObject.Find ("PlayerSphere"));//プレイヤー削除
			PlayVoice (Random.Range (0, voiceClip.Length));//ランダムでトラップのボイス再生
			Invoke("SceneMove", 1.6f);//2秒後初めに戻る
		}
	}

	void SceneMove(){
		SceneManager.LoadScene("Main");
	}

	void PlayVoice(int i){
		destroyVoice.clip = voiceClip [i];
		destroyVoice.Play();
	}
}
