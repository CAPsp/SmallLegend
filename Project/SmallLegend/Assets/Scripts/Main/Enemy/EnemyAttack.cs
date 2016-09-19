using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float boundVelocity_ = 10f;	// 跳ね返る速度

	GameObject player_;
	PlayerHelth playerHelth_;
	Rigidbody rigidbody_;

	void Awake(){
		player_ 		= GameObject.FindGameObjectWithTag ("Player");
		playerHelth_ 	= player_.GetComponent<PlayerHelth> ();
		rigidbody_ 		= GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision other) {

		if (other.gameObject == player_) {
			playerHelth_.TakeDamage ();

			// プレイヤーと逆方向に吹っ飛ぶ
			Vector3 angle = transform.position - other.transform.position;
			angle.Normalize();
			rigidbody_.velocity = angle * boundVelocity_;

		}

    }

}
