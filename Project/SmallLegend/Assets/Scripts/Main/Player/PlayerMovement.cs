using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed_ = 6.00f;
    public Transform playerRayTransform_;     // 地面設置判定のためのRayを飛ばす体の位置
    public float rayRange_;

    Transform cameraTransform_;
    float cameraAngleOffsetY;   // 初期のカメラのY軸回転角を保存
    Rigidbody playerRigidbody_;
    bool isGround_ = true;
    Vector3 velocity_;
	AudioSource[] audioSources_;

    void Awake() {
        cameraTransform_    = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraAngleOffsetY  = cameraTransform_.rotation.eulerAngles.y;

        playerRigidbody_ = GetComponent<Rigidbody>();

        velocity_ = Vector3.zero;

		audioSources_ = GetComponents<AudioSource> ();	// 0がジャンプ、1が着地
    }

	void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float w = Input.GetAxis("Vertical");

        Move(h, w);
        Turning();

		bool tmp = isGround_;
        isGround_ = CheckGrounded();

		// 直前でfalse、現在trueなら着地した直後とみなす
		if (!tmp && isGround_) {
			audioSources_ [1].Play ();
		}

        // 接地してる:ジャンプ可能　設置してない：重力付加
        if (isGround_) {
            velocity_ = Vector3.zero;

            if (Input.GetButton("Jump")) {
				audioSources_ [0].Play ();
                velocity_.y = Physics.gravity.y * (-1.0f);   
            }

        }
        else {
            velocity_.y += (Physics.gravity.y * 3.0f) * Time.deltaTime;
        }

        // 重力もしくはジャンプで計算した速度からプレイヤーの位置を調整
        playerRigidbody_.MovePosition(playerRigidbody_.position + velocity_ * Time.deltaTime);
    }


    void Move(float h, float w) {

        // ワールド座標時の移動量に置き換え
        Vector3 worldMove = new Vector3(h, 0.0f, w);

        // カメラの向きに合わせて移動を決定
        Vector3 movement = cameraTransform_.rotation * worldMove;
        movement.y = 0.0f;

        movement = movement.normalized * speed_ * Time.deltaTime;
        playerRigidbody_.MovePosition(transform.position + movement);
        
    }


    void Turning() {

        // カメラのオイラー回転角を取り出して、Y軸のみの角度を持つQuaternionを得る
        Vector3 cameraEulerAngles = cameraTransform_.rotation.eulerAngles;
        Quaternion rotationY = Quaternion.AngleAxis(cameraEulerAngles.y - cameraAngleOffsetY, Vector3.up);
        
        playerRigidbody_.MoveRotation(rotationY);
    }


    // 真下にRayを飛ばして接地してるかを判定
    bool CheckGrounded() {

        Debug.DrawLine(playerRayTransform_.position, (playerRayTransform_.position - transform.up * rayRange_), Color.red);

		RaycastHit hitCollider;
		if (Physics.Linecast(playerRayTransform_.position, (playerRayTransform_.position - transform.up * rayRange_), out hitCollider)) {
			
			return !hitCollider.collider.isTrigger;
        }

        return false;
    }

}
