using UnityEngine;
using System.Collections;

public class PlayerHelth : MonoBehaviour {

	public float nonDamageTime_ = 1f;

	int helth_ = 5;
	float timer_;

	void Awake(){
		timer_ = nonDamageTime_;
	}

	void Update(){

		if (timer_ < nonDamageTime_) {
			timer_ += Time.deltaTime;
		}

	}

	// 受けるダメージは常に１
	public void TakeDamage(){

		// 無敵時間中は攻撃を受けない
		if (timer_ < nonDamageTime_) {
			return;
		}

		helth_--;

		Debug.Log (helth_);

	}

}
