using UnityEngine;
using System.Collections;

public class Enemy2Attack : MonoBehaviour {

	[SerializeField] float searchDistance_ 	= 15f;
	[SerializeField] GameObject prefabAmmo_;
	[SerializeField] float speed_ 			= 20f;
	[SerializeField] Transform shotPoint_;
	[SerializeField] float intervalTime_ 	= 1f;

	GameObject player_;
	float timer_;

	void Awake(){
		player_ = GameObject.FindGameObjectWithTag ("Player");
		timer_ 	= intervalTime_;
	}

	void Update(){

		if (timer_ < intervalTime_) {
			timer_ += Time.deltaTime;
			return;
		}

		if (DistanceFromPlayer () < searchDistance_) {
			Shot ();
		}

	}

	float DistanceFromPlayer(){
		return Vector3.Distance(transform.position, player_.transform.position);
	}

	void Shot(){

		// プレイヤーとの角度を計算
		Vector3 angle = player_.transform.position - shotPoint_.position;
		angle.Normalize ();


		// プレイヤーの方向を向く(y軸のみ回転)
		Quaternion targetAngle = Quaternion.FromToRotation (transform.forward, angle);
		transform.Rotate( new Vector3(0f, targetAngle.eulerAngles.y, 0f) );

		// 弾を撃ち出す
		GameObject ammo = Instantiate(prefabAmmo_, shotPoint_.position, Quaternion.identity) as GameObject;
		ammo.GetComponent<Rigidbody>().velocity = angle * speed_;

		timer_ = 0f;

	}

}
