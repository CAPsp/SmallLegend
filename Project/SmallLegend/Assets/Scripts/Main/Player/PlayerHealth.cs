using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float nonDamageTime_ 	= 1f;
	public static int maxHealth_	= 5;
	public Heart heartUI_;
	public Image displayImage_;
	public Color damageEffectColor_ = new Color(1f, 0f, 0f, 0.5f);

	int health_;
	float timer_;
	AudioSource audioHitSE_;
	bool nonDamage_ = false;

	void Awake(){
		health_ 	= maxHealth_;
		timer_ 		= nonDamageTime_;
		audioHitSE_ = (GetComponents<AudioSource>())[2];
	}

	void Update(){

		if (timer_ < nonDamageTime_) {
			timer_ += Time.deltaTime;
		}
			
		displayImage_.color = Color.Lerp (displayImage_.color, Color.clear, 5f * Time.deltaTime);

	}

	// 受けるダメージは常に１だが、落ちて死んだ時などのことも考慮して引数にダメージをとる
	public void TakeDamage(int damage){

		// 無敵時間中は攻撃を受けない
		if (nonDamage_ || timer_ < nonDamageTime_ || health_ <= 0) {
			return;
		}

		timer_ = 0f;
		health_ -= damage;
		if (health_ < 0) {
			health_ = 0;
		}
			
		heartUI_.ChangeActiveHearts(health_);	// UIに現在のHPを通知

		displayImage_.color = damageEffectColor_;	// 画面にエフェクトをかける

		audioHitSE_.Play ();

	}
		
	public void SetNonDamage(bool value){
		nonDamage_ = value;
	}

}
