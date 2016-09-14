using UnityEngine;
using System.Collections;

// 弓の攻撃前提で作られているが、後々各武器ごとにクラス分けする予定

public class PlayerAttack : MonoBehaviour {

	public GameObject prefabAmmo_;		// 射出するobj
	public Transform targetPoint_;		// 狙うポイント
	public Transform firePoint_;		// 弾生成ポイント
    public float timeBetweenBullets_    = 0.15f;
    public float velocityUpPerTime_     = 50f;
    public float maxVelocity_           = 1000f;
    public float minVelocity_           = 50f;

    float velocity_;

	// Use this for initialization
	void Start () {
        velocity_ = minVelocity_;
    }
	
	// Update is called once per frame
	void Update () {

        // 押し続けている間、力を貯める
		if (Input.GetButton ("Fire1")) {

			DebugString.ReplaceText ("Charge");

			velocity_ += velocityUpPerTime_ * Time.deltaTime;
			if (velocity_ >= maxVelocity_) {
				velocity_ = maxVelocity_;
			}

		}
		else if (velocity_ > minVelocity_) {

			DebugString.ReplaceText ("Shot");

			Shot ();
			velocity_ = minVelocity_;
		}
		else {
			DebugString.ReplaceText ("None");
		}

	}

    void Shot() {

		// 正規化した発射角度を割り出す
		Vector3 shotAngle = targetPoint_.position - firePoint_.position;
		shotAngle = shotAngle.normalized;

		Debug.Log (shotAngle);

		// プレイヤーと被らない位置に弾を生成
		GameObject ammo = Instantiate (prefabAmmo_, firePoint_.position, Quaternion.identity) as GameObject;
		ammo.transform.position += shotAngle * ammo.transform.localScale.x;

		// 発射速度を加算
		ammo.GetComponent<Rigidbody> ().velocity = shotAngle * velocity_;

    }
}
