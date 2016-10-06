using UnityEngine;
using System.Collections;

// プレイヤーが乗っているときは目的地に移動、乗っていないときは元の場所に戻る、という動作をするリフト
public class FloorMoving : MonoBehaviour {

    [SerializeField] Vector3 moveDistance_;
    [SerializeField] float speed_;
	[SerializeField] bool oneWay_ = false;	// trueにすると一方通行になる 

    Transform collisionObjTransform_;     	// リフトの移動に合わせて移動するため必要
    bool isMoving_  = false;
    bool inverse_   = false;
    Vector3 originMoveDistance_;

    void Awake() {
        originMoveDistance_ = moveDistance_;
        moveDistance_       = Vector3.zero;
    }

    void Update() {

        if (!isMoving_) {
            return;
        }

        Move();

    }

    void Move() {

        // このフレーム中に移動する距離を計算
        Vector3 distance = moveDistance_.normalized * (Time.deltaTime * speed_);

        // リフトとそれに乗っているオブジェクトを移動
        transform.position += distance;
        if(collisionObjTransform_ != null) {
            collisionObjTransform_.position += distance;
        }

        moveDistance_ -= distance;
        if (moveDistance_.magnitude <= 0.1f) {

            isMoving_ = false;
            collisionObjTransform_ = null;
        }

    }

    void OnCollisionEnter(Collision other) {

        if(other.gameObject.tag == "Player") {
            collisionObjTransform_ = other.gameObject.transform;

            if (!inverse_) {
                moveDistance_ += originMoveDistance_;
                isMoving_ = true;
                inverse_ = true;
            }
        }       

    }

    // 元の位置に戻る
    void OnCollisionExit(Collision other) {

        if (other.gameObject.tag == "Player") {
            collisionObjTransform_ = null;

			if (oneWay_) {
				return;
			}

            if (inverse_) {
                moveDistance_ -= originMoveDistance_;
                isMoving_ = true;
                inverse_ = false;
            }
        }

    }

}
