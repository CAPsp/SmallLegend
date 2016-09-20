using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

	public GameObject prefabArrow_;
	public Transform targetPoint_;
	public float idleTime_				= 0.2f;
    
	public float velocityUpPerSecond_   = 100f;
    public float maxVelocity_           = 200f;
    public float minVelocity_           = 25f;

    public int damegeUpPerSecond_       = 50;
    public int maxDamage_               = 100;
    public int minDamage_               = 20;

    float velocity_;
	int damage_;
	float timer_;
	AudioSource seConcentrate_;

	void Awake(){
		velocity_ 	= minVelocity_;
		timer_ 		= idleTime_;
		damage_		= minDamage_;

		seConcentrate_ = GetComponent<AudioSource> ();
	}

    void Update() {

		if (timer_ < idleTime_) {
			timer_ += Time.deltaTime;
			return;
		}

        // 押し続けている間、力を貯める
		if (Input.GetButton ("Fire1")) {

			if (!seConcentrate_.isPlaying && velocity_ <= minVelocity_) {
				seConcentrate_.Play ();
			}

			velocity_ += velocityUpPerSecond_ * Time.deltaTime;
			velocity_ = (velocity_ >= maxVelocity_) ? maxVelocity_ : velocity_;

			damage_ += (int)((float)damegeUpPerSecond_ * Time.deltaTime);
			damage_ = (damage_ >= maxDamage_) ? maxDamage_ : damage_;

		}
		else if (velocity_ > minVelocity_) {
			Shot ();
		}


    }

	void Shot(){

		// 正規化した発射角度を割り出す
		Vector3 shotAngle = targetPoint_.position - transform.position;
		shotAngle = shotAngle.normalized;

        // プレイヤーと被らない位置に弾を生成
		GameObject ammo = Instantiate(prefabArrow_, transform.position, Quaternion.identity) as GameObject;
        ammo.transform.position += shotAngle * ammo.transform.localScale.x;

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

		timer_ = 0f;
		velocity_   = minVelocity_;
		damage_     = minDamage_;
	}

}
