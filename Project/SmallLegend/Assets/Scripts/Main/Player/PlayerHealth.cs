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

	// 受けるダメージは常に１だが、落ちて死んだ時などのことも考慮して引数にダメージをとる
	public void TakeDamage(int damage){

		// 無敵時間中は攻撃を受けない
		if (timer_ < nonDamageTime_ || health_ <= 0) {
			return;
		}

		health_ -= damage;
		if (health_ < 0) {
			health_ = 0;
		}

		// UIに現在のHPを通知
		heartUI_.ChangeActiveHearts(health_);

	}

}
