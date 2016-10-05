using UnityEngine;
using System.Collections;

public class ReticleController : MonoBehaviour {

    public float reticleMaxDistance_ = 50f;

    Transform mainCameraTransform_;
    Ray obstacleCheckRay_;
    RaycastHit hitRay_;
    int obstacleMask_;  // 障害物と認識するマスク番号

    void Awake() {
        mainCameraTransform_ = transform.parent;
        obstacleMask_ = LayerMask.GetMask("Obstacle");
    }

    // 最長距離内に障害物があったら照準をそこに合わす。なければ最長距離に照準を置く
    // この処理はRayを使って行う
    void Update() {

        obstacleCheckRay_.origin = mainCameraTransform_.position;
        
        Vector3 maxVector3 = mainCameraTransform_.transform.position + (mainCameraTransform_.forward * reticleMaxDistance_);
        Vector3 angle = maxVector3 - mainCameraTransform_.transform.position;
        angle = angle.normalized;
        obstacleCheckRay_.direction = angle;

        Debug.DrawLine(obstacleCheckRay_.origin, maxVector3, Color.blue);

        if (Physics.Raycast(obstacleCheckRay_, out hitRay_, reticleMaxDistance_, obstacleMask_)) {
            transform.position = hitRay_.point;
        }
        else {
            transform.position = maxVector3;
        }

    }

}
