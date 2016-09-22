using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;

public class PlayerHealth : MonoBehaviour {

	public static int maxHealth_	= 5;

	[SerializeField] float nonDamageTime_ 	= 1f;
	[SerializeField] Heart heartUI_;
	[SerializeField] Image displayImage_;
	[SerializeField] Color damageEffectColor_ = new Color(1f, 0f, 0f, 0.5f);
	[SerializeField] GameObject gameoverObject_;
	[SerializeField] AudioSource audioBGM_;
	[SerializeField] AudioClip audioHitClip_;

	int health_;
	float timer_;
	bool nonDamage_ = false;
	DarkChange darkChange_;
	AudioSource audioSource_;

	void Awake(){
		health_ 		= maxHealth_;
		timer_ 			= nonDamageTime_;
		audioSource_ 	= GetComponent<AudioSource>();
	}

	void Update(){

		if (health_ == 0) {
			DeathEffect ();
			return;
		}

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
		if (health_ <= 0) {
			ReadyDeathEffect ();
		}
			
		heartUI_.ChangeActiveHearts(health_);	// UIに現在のHPを通知

		displayImage_.color = damageEffectColor_;	// 画面にエフェクトをかける

		audioSource_.clip = audioHitClip_;
		audioSource_.Play ();

	}
		
	public void SetNonDamage(bool value){
		nonDamage_ = value;
	}

	void ReadyDeathEffect(){

		health_ = 0;
		darkChange_ = new DarkChange (displayImage_, 1.0f);

		audioBGM_.Stop ();
		GetComponent<PlayerMovement> ().enabled = false;
		GetComponent<PlayerAnchor> ().enabled 	= false;

		// 武器を使えなくする
		GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}
	}

	void DeathEffect(){

		// 気休めの死亡アニメーション
		Quaternion x90Rotation = Quaternion.AngleAxis(90f, Vector3.right);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, x90Rotation, 5f * Time.deltaTime);

		// 暗転処理
		if (darkChange_.CallAtUpdate ()) {
			Cursor.lockState 	= CursorLockMode.None;
			Cursor.visible 		= true;
			gameoverObject_.SetActive (true);
		}
			
	}

}
