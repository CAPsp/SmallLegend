using UnityEngine;
using System.Collections;

public class Enemy2Attack : MonoBehaviour {

    [SerializeField] float searchDistance_ = 15f;       // 索敵範囲
    [SerializeField] float handsUpDistance_ = 2.5f;     // お手上げになる範囲(2Dマリオのパックンみたいな)
    [SerializeField] GameObject prefabAmmo_;
    [SerializeField] float speed_ = 20f;
    [SerializeField] Transform shotPoint_;
    [SerializeField] float intervalTime_ = 1f;

    GameObject player_;
    float timer_;

    void Awake() {
        player_ = GameObject.FindGameObjectWithTag("Player");
        timer_ = 0f;
    }

    void Update() {

        float distance = DistanceFromPlayer();
		if (distance < searchDistance_ && handsUpDistance_ < distance) {

			timer_ += Time.deltaTime;

			Rotation ();

			if (timer_ >= intervalTime_) {
				Shot ();
			}
		}
		else {
			timer_ = 0f;
		}

    }

    float DistanceFromPlayer() {
        return Vector3.Distance(transform.position, player_.transform.position);
    }

    void Rotation() {

        // プレイヤーの方向を向く(y軸のみ回転)
        Quaternion targetAngle = Quaternion.FromToRotation(transform.forward, CalcAngleBetweenPlayer());
        transform.Rotate(new Vector3(0f, targetAngle.eulerAngles.y, 0f));

    }

	void Shot(){

		// 弾を撃ち出す
		GameObject ammo = Instantiate(prefabAmmo_, shotPoint_.position, Quaternion.identity) as GameObject;
		ammo.GetComponent<Rigidbody>().velocity = CalcAngleBetweenPlayer() * speed_;

		timer_ = 0f;

	}

    // プレイヤーとの角度を計算
    Vector3 CalcAngleBetweenPlayer() {

        Vector3 angle = player_.transform.position - shotPoint_.position;
        angle.Normalize();

        return angle;
    }

}
