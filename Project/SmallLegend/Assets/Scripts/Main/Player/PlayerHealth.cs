using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float nonDamageTime_ 	= 1f;
	public static int maxHealth_	= 5;
	public Heart heartUI_;

	int health_;
	float timer_;

	void Awake(){
		health_ = maxHealth_;
		timer_ 	= nonDamageTime_;
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

		health_--;

		// UIに現在のHPを通知
		heartUI_.ChangeActiveHearts(health_);

	}

}
