using UnityEngine;
using System.Collections;
using Utility;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float speed_ = 6.00f;
    [SerializeField] Transform playerRayTransform_;     // 地面設置判定のためのRayを飛ばす体の位置
    [SerializeField] float rayRange_;
	[SerializeField] AudioClip audioJumpClip_;
	[SerializeField] AudioClip audioLandingClip_;
	[SerializeField] float jumpIntervalTime_ = 0.7f;

    Transform cameraTransform_;
    float cameraAngleOffsetY;   // 初期のカメラのY軸回転角を保存
    Rigidbody playerRigidbody_;
    bool isGround_ = true;
    Vector3 velocity_;
	AudioSource audioSource_;
    Animator playerAnimator;
	float timer_;

    // 入力を一時的に格納する
    float keyH_, keyW_;
    bool keyJump_;

    void Awake() {
		
        cameraTransform_    = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraAngleOffsetY  = cameraTransform_.rotation.eulerAngles.y;

        playerRigidbody_ = GetComponent<Rigidbody>();

        velocity_ = Vector3.zero;

		audioSource_ 	= GetComponent<AudioSource> ();
		playerAnimator 	= GetComponent<Animator> ();

		timer_ = jumpIntervalTime_;
        ClearInput();
    }

    // 入力受付
    void Update() {
        keyH_       = Input.GetAxis("Horizontal");
        keyW_       = Input.GetAxis("Vertical");
        keyJump_    = Input.GetButton("Jump");
    }

    // 動きを反映
	void FixedUpdate() {

		if (timer_ <= jumpIntervalTime_) {
			timer_ += Time.deltaTime;
		}

        Move(keyH_, keyW_);
        Turning();

		bool tmp = isGround_;
        isGround_ = CheckGrounded();

		// 直前でfalse、現在trueなら着地した直後とみなす
		if (!tmp && isGround_) {
			AudioUtil.PlayFromClips (audioSource_, audioLandingClip_);
		}

        // 接地してる:ジャンプ可能　設置してない：重力付加
        if (isGround_) {
            velocity_ = Vector3.zero;

			if (timer_ > jumpIntervalTime_ && keyJump_) {
				AudioUtil.PlayFromClips (audioSource_, audioJumpClip_);
                velocity_.y = Physics.gravity.y * (-1.0f);
				timer_ 		= 0f;
            }

        }
        else {
            velocity_.y += (Physics.gravity.y * 3.0f) * Time.deltaTime;
        }

        // 重力もしくはジャンプで計算した速度からプレイヤーの位置を調整
        playerRigidbody_.MovePosition(playerRigidbody_.position + velocity_ * Time.deltaTime);

        ClearInput();
    }


    void Move(float h, float w) {

        // ワールド座標時の移動量に置き換え
        Vector3 worldMove = new Vector3(h, 0.0f, w);

        //アニメーション
        if (h != 0f || w != 0f)
        {
            playerAnimator.SetBool("Walk",true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }

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

    void ClearInput() {
        keyH_ = 0f;
        keyW_ = 0f;
        keyJump_ = false;
    }

}
