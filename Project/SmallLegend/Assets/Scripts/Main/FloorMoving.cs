﻿using UnityEngine;

// プレイヤーが乗っているときは目的地に移動、乗っていないときは元の場所に戻る、という動作をするリフト
public class FloorMoving : MonoBehaviour {
        originMoveDistance_ = moveDistance_;
        moveDistance_       = Vector3.zero;
    }
            collisionObjTransform_.position += distance;
        }
            collisionObjTransform_ = null;

        if(other.gameObject.tag == "Player") {
            collisionObjTransform_ = other.gameObject.transform;

            if (!inverse_) {
                moveDistance_ += originMoveDistance_;
                isMoving_ = true;
                inverse_ = true;
            }
        }       

        if (other.gameObject.tag == "Player") {
            collisionObjTransform_ = null;

            if (inverse_) {
                moveDistance_ -= originMoveDistance_;
                isMoving_ = true;
                inverse_ = false;
            }
        }

    }