using UnityEngine;using System.Collections;

// 弓の攻撃前提で作られているが、後々各武器ごとにクラス分けする予定

public class PlayerAttack : MonoBehaviour {    public GameObject ammo_;    public float timeBetweenBullets_    = 0.15f;
    public float velocityUpPerTime_     = 5f;
    public float maxVelocity_           = 20f;
    public float minVelocity_           = 1f;

    float velocity_;	// Use this for initialization	void Start () {
        velocity_ = minVelocity_;
    }		// Update is called once per frame	void Update () {

        // 押し続けている間、力を貯める
        if (Input.GetButton("Fire1")) {

            velocity_ += velocityUpPerTime_ * Time.deltaTime;
            if(velocity_ >= maxVelocity_) {
                velocity_ = maxVelocity_;
            }

        }        else if (velocity_ > 0f) {
            Shot();
            velocity_ = minVelocity_;
        }	}    void Shot() {

    }}