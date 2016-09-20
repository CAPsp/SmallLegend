using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public int damage;

	AudioSource se_;
	bool isDestroying_ = false;

	void Awake(){
		se_ = GetComponent<AudioSource> ();
		se_.Play ();
	}

	void Update(){

		if (isDestroying_ && !se_.isPlaying) {
			Destroy(gameObject);
		}

	}

	// 何かに当たったら消える
	void OnCollisionEnter(Collision other) {

		if (other.gameObject.tag == "Player") {
			return;
		}

		isDestroying_ = true;

		GetComponent<Collider> ().enabled 	= false;		// 当たり判定を消す

		// その位置に固定する
		Rigidbody rigid 	= GetComponent<Rigidbody>();
		rigid.velocity 		= Vector3.zero;
		rigid.useGravity 	= false;
	}
}
