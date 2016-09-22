using UnityEngine;
using System.Collections;

public class BGMFadeOut : MonoBehaviour {

	AudioSource audioSource_;
	bool doing_ = false;

	void Awake(){
		audioSource_ = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (doing_) {
			audioSource_.volume -= Time.deltaTime * 0.5f;
		}

	}

	public void StartFadeOut(){
		doing_ = true;
	}

}
