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
			other.gameObject.SetActive(false);//プレイヤー削除
			PlayVoice (Random.Range (0, voiceClip.Length));//ランダムでトラップのボイス再生
			Invoke("SceneMove", 1.6f);//1.6秒後初めに戻る
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
