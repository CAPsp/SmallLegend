using UnityEngine;using System.Collections;abstract class Weapon : MonoBehaviour{        protected float idleTime_;    protected int damage_;    float timer_ = 0f;    // 戻り値がtrue = 攻撃準備完了    public virtual bool EffectPerUpdate(bool pushButton) {

        timer_ += Time.deltaTime;

        return (timer_ >= idleTime_);
    }

    // 引数は攻撃する方向
    public virtual void Attack(Vector3 angle) {
        timer_ = 0f;
    }}