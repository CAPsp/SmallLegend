using UnityEngine;
using System.Collections;

public class Bow : Weapon {

    public GameObject prefabArrow_;

    public float velocityUpPerSecond_   = 20f;
    public float maxVelocity_           = 200f;
    public float minVelocity_           = 20f;

    public int damegeUpPerSecond_       = 10;
    public int maxDamage_               = 100;
    public int minDamage_               = 10;

    float velocity_ = 0f;

    public Bow(){
        idleTime_   = 0.2f;
        damage_     = 10;
    }    public override bool EffectPerUpdate(bool pushButton) {

        if (!base.EffectPerUpdate(pushButton)) {
            return false;
        }

        // 押し続けている間、力を貯める
        if (pushButton) {

            velocity_ += velocityUpPerSecond_ * Time.deltaTime;
            velocity_ = (velocity_ >= maxVelocity_) ? maxVelocity_ : velocity_;

            damage_ += (int)((float)damegeUpPerSecond_ * Time.deltaTime);
            damage_ = (damage_ >= maxDamage_) ? maxDamage_ : damage_;

        }
        else if(velocity_ >= minVelocity_){
            velocity_   = 0f;
            damage_     = minDamage_;
            return true;
        }

        return false;
    }


    public override void Attack(Vector3 angle) {

        base.Attack(angle);

        // プレイヤーと被らない位置に弾を生成
        GameObject ammo = Instantiate(prefabAmmo_, firePoint_.position, Quaternion.identity) as GameObject;
        ammo.transform.position += shotAngle * ammo.transform.localScale.x;

        // 発射速度を加算
        ammo.GetComponent<Rigidbody>().velocity = shotAngle * velocity_;
    }

}
