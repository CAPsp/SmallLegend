using UnityEngine;
using System.Collections;

public class Enemy2Ammo : MonoBehaviour {

	[SerializeField] float endTime_ = 2f;

	float timer_ = 0f;
	PlayerHealth playerHealth_;

	void Start(){
		timer_ = 0f;
		playerHealth_ = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	void Update(){

		timer_ += Time.deltaTime;
		if (timer_ > endTime_) {
			Destroy (gameObject);
		}

	}

	void OnCollisionEnter(Collision other){

		if (other.gameObject.tag == "Player") {
			playerHealth_.TakeDamage (1);
		}

		Destroy (gameObject);

	}

}
