using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField] float limitTimeSecond_;	// 制限時間
	[SerializeField] PlayerHealth playerHealth_;

	float currentTimeSecond_;
	Text timerText_;

	void Awake(){
		timerText_ 		= GetComponent<Text>();
		currentTimeSecond_ 	= limitTimeSecond_;

		timerText_.text = ((int)currentTimeSecond_).ToString ();
	}

	void Update(){

		currentTimeSecond_ -= Time.deltaTime;
		if (currentTimeSecond_ <= 0.0f) {
			currentTimeSecond_ = 0.0f;
			playerHealth_.TakeDamage (PlayerHealth.maxHealth_);
			Destroy (gameObject);
		}

		timerText_.text = ((int)currentTimeSecond_).ToString ();

	}

}
