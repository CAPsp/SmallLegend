using UnityEngine;
using System.Collections;

public class Enemy1Attack : MonoBehaviour {

	public float boundVelocity_ = 10f;	// 跳ね返る速度

	GameObject player_;
	PlayerHealth playerHealth_;
	Rigidbody rigidbody_;

	void Awake(){
		player_ 		= GameObject.FindGameObjectWithTag ("Player");
		playerHealth_ 	= player_.GetComponent<PlayerHealth> ();
		rigidbody_ 		= GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision other) {

		if (other.gameObject == player_) {
			playerHealth_.TakeDamage (1);

			// プレイヤーと逆方向に吹っ飛ぶ
			Vector3 angle = transform.position - other.transform.position;
			angle.Normalize();
			rigidbody_.velocity = angle * boundVelocity_;

		}

    }

}
