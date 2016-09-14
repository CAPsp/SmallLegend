using UnityEngine;using System.Collections;public class PlayerAnchor : MonoBehaviour {    public GameObject prefabAnchor_;    // アンカーとなるprefab
    public Transform targetPoint_;      // 狙うポイント
    public Transform firePoint_;        // 生成ポイント
    public float moveVelocity_ = 50f;

    //LineRenderer anchorLine_;    GameObject currentAnchor_ = null;    void Awake() {
      //  anchorLine_ = GetComponent<LineRenderer>();
    }	// Update is called once per frame	void Update () {

        // アンカー射出中
        if (currentAnchor_ != null) {

            return;
        }

        // 右クリックでアンカーをfirePoint_　めがけて射出
        if (Input.GetButton("Fire2")) {
            Shot();
        }	}    void Shot() {

        // 正規化した発射角度を割り出す
        Vector3 shotAngle = targetPoint_.position - firePoint_.position;
        shotAngle = shotAngle.normalized;

        // プレイヤーと被らない市にアンカー生成
        currentAnchor_ = Instantiate(prefabAnchor_, firePoint_.position, Quaternion.identity) as GameObject;
        currentAnchor_.transform.position += shotAngle * currentAnchor_.transform.localScale.x;

        // 等速直線運動
        currentAnchor_.GetComponent<Rigidbody>().velocity = shotAngle * moveVelocity_;

    }}