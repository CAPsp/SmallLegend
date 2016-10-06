using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

public class Bow : MonoBehaviour {

	public GameObject prefabArrow_;
	public Transform targetPoint_;
	public float idleTime_				= 0.5f;
	public Image chargeCircleImage_;
    
	public float chargeEndTimeAmount_	= 2f;
    public float maxVelocity_           = 200f;
    public float minVelocity_           = 20f;
    public int maxDamage_               = 100;
    public int minDamage_               = 20;


    float velocity_;
	int damage_;
	float idleTimer_	= 0f;
	float chargeTimer_	= 0f;

	Vector2 initCircleSize_;	// 初期のチャージを表す円の座標
	AudioSource seConcentrate_;
	SphereCollider playerSphereCollider_;

	void Awake(){
		initCircleSize_ = chargeCircleImage_.rectTransform.sizeDelta;
		seConcentrate_ 	= GetComponent<AudioSource> ();
		playerSphereCollider_ = HierarchyUtil.FindParent(gameObject).GetComponent<SphereCollider>();
	}

    void Update() {

		if (idleTimer_ < idleTime_) {
			idleTimer_ += Time.deltaTime;
			return;
		}

        // 押し続けている間、力を貯める
		if (Input.GetButton ("Fire1")) {
			Charge ();
		}
		else if (chargeCircleImage_.enabled) {
			Shot ();
		}

    }

	void Charge(){
		
		// 力溜め開始
		if (!seConcentrate_.isPlaying && chargeTimer_ == 0f) {
			seConcentrate_.Play ();
			chargeCircleImage_.enabled = true;
		}

		chargeTimer_ += Time.deltaTime;

		velocity_ = minVelocity_ + (maxVelocity_ - minVelocity_) * (chargeTimer_ / chargeEndTimeAmount_);
		velocity_ = (velocity_ >= maxVelocity_) ? maxVelocity_ : velocity_;

		damage_ = minDamage_ + (int)((float)(maxDamage_ - minDamage_) * (chargeTimer_ / chargeEndTimeAmount_));
		damage_ = (damage_ >= maxDamage_) ? maxDamage_ : damage_;

		// 溜めた時間によって円が小さくなっていくエフェクト
		chargeCircleImage_.rectTransform.sizeDelta
			= Vector2.Lerp (initCircleSize_, Vector2.zero, (float)damage_ / (float)maxDamage_);

	}

	void Shot(){

		// 正規化した発射角度を割り出す
		Vector3 shotAngle = targetPoint_.position - transform.position;
		shotAngle.Normalize();

        // プレイヤーと被らない位置に弾を生成
		GameObject ammo = Instantiate(prefabArrow_, transform.position, Quaternion.identity) as GameObject;
		ammo.transform.position += shotAngle * playerSphereCollider_.radius;

        // 発射速度を加算
		ammo.GetComponent<Rigidbody>().velocity = shotAngle * velocity_;

		// ダメージの値をArrowクラスのメンバに渡す
		Arrow arrow = ammo.GetComponent<Arrow>();
		if (arrow != null) {
			arrow.damage = damage_;
		}
			
		// 溜めているSEは消す
		if (seConcentrate_.isPlaying) {
			seConcentrate_.Stop ();
		}

		idleTimer_ 		= 0f;
		chargeTimer_ 	= 0f;

		chargeCircleImage_.enabled = false;
		chargeCircleImage_.rectTransform.sizeDelta = initCircleSize_;
	}

}
