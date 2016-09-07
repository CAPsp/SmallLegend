using UnityEngine;using System.Collections;public class PlayerMovement : MonoBehaviour {    public float speed_ = 6.00f;    public Transform playerRayTransform_;     // 地面設置判定のためのRayを飛ばす体の位置
    public float rayRange_;    Transform cameraTransform_;    float cameraAngleOffsetY;   // 初期のカメラのY軸回転角を保存    Rigidbody playerRigidbody_;    bool isGround_ = false;    void Awake() {        cameraTransform_    = GameObject.FindGameObjectWithTag("MainCamera").transform;        cameraAngleOffsetY  = cameraTransform_.rotation.eulerAngles.y;        playerRigidbody_ = GetComponent<Rigidbody>();    }	void FixedUpdate() {        float h = Input.GetAxis("Horizontal");        float w = Input.GetAxis("Vertical");        Move(h, w);        Turning();        isGround_ = CheckGrounded();
        Debug.Log(isGround_);

        // 接地してる:ジャンプ可能　設置してない：重力付加
        if (isGround_) {

        }        else {

        }    }    void Move(float h, float w) {        // ワールド座標時の移動量に置き換え        Vector3 worldMove = new Vector3(h, 0.0f, w);        // カメラの向きに合わせて移動を決定        Vector3 movement = cameraTransform_.rotation * worldMove;        movement.y = 0.0f;        movement = movement.normalized * speed_ * Time.deltaTime;        playerRigidbody_.MovePosition(transform.position + movement);            }    void Turning() {        // カメラのオイラー回転角を取り出して、Y軸のみの角度を持つQuaternionを得る        Vector3 cameraEulerAngles = cameraTransform_.rotation.eulerAngles;        Quaternion rotationY = Quaternion.AngleAxis(cameraEulerAngles.y - cameraAngleOffsetY, Vector3.up);                playerRigidbody_.MoveRotation(rotationY);    }    // 真下にRayを飛ばして接地してるかを判定    bool CheckGrounded() {

        Debug.DrawLine(playerRayTransform_.position, (playerRayTransform_.position - transform.up * rayRange_), Color.red);

        if (Physics.Linecast(playerRayTransform_.position, (playerRayTransform_.position - transform.up * rayRange_))) {
            return true;
        }

        return false;
    }}