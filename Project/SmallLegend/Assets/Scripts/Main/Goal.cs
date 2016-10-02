using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public AudioSource audioBGM_;
	public float endBGMLimitVolume_ 	= 0.05f;	// BGMが終わったと判断するvolume
	public float endMoveCameraDistance_ = 1f;
	public Transform cameraRigTransform_;
	public Transform clearCameraTransform_;
	public float effectCameraMoveSpeed_ = 5f;
	public GameObject clearUIobject_;

	Transform pivotTransform_;
	Camera mainCamera_;
	GameObject playerObject_;
	bool isClear_ = false;
	Vector3 goalPosition_;	// プレイヤーがゴールしたときの座標

	void Awake(){
		pivotTransform_	= cameraRigTransform_.GetChild (0);
		mainCamera_ 	= pivotTransform_.GetChild (0).gameObject.GetComponent<Camera>();
		playerObject_ 	= GameObject.FindWithTag ("Player");
	}

	void Update(){

		if (isClear_) {

			// プレイヤーはゴールした位置から動かないようにする
			playerObject_.transform.position = goalPosition_;

			if (clearUIobject_.activeInHierarchy) {
				return;
			}
			else if (ClearEffect ()) {
				clearUIobject_.SetActive (true);
			}

		}

	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject == playerObject_) {
			ReadyClearEffect ();
		}

	}

	// クリア時の初期化処理
	void ReadyClearEffect(){

		goalPosition_ = playerObject_.transform.position;

		// ユーザからの入力初期化
		Input.ResetInputAxes ();

		// 無敵にする
		playerObject_.GetComponent<PlayerHealth> ().SetNonDamage (true);

		// 武器を使えなくする
		GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}

		// 地面に着地するように処理
		Rigidbody rigid = playerObject_.GetComponent<Rigidbody> ();
		rigid.useGravity 	= true;
		rigid.drag 			= 0f;

		// 一時的に現在のカメラのパラメーターを保存 
		Vector3 tmpPosition = mainCamera_.transform.position;
		Quaternion tmpRotation = mainCamera_.transform.rotation;

		// カメラの操作権を奪う
		cameraRigTransform_.gameObject.SetActive (false);
		mainCamera_.transform.parent 	= null;
		mainCamera_.transform.position 	= tmpPosition;
		mainCamera_.transform.rotation	= tmpRotation;

		isClear_ = true;

	}

	// エフェクトが終わってなければfalseを返す
	bool ClearEffect(){

		bool isEnd = true;

		// プレイヤーを操作不能にする
		playerObject_.GetComponent<PlayerMovement> ().enabled 	= false;
		playerObject_.GetComponent<PlayerAnchor> ().enabled 	= false;

		// BGMをフェードアウトする処理
		if (audioBGM_.isPlaying) {
			audioBGM_.volume = Mathf.Lerp (audioBGM_.volume, 0.0f, Time.deltaTime);

			if (audioBGM_.volume <= endBGMLimitVolume_) {
				audioBGM_.Stop ();
			}

			isEnd = false;;
		}

		// カメラをPlayerの前に持ってくる処理
		Transform cameraTF = mainCamera_.transform;
		if (Vector3.Distance (clearCameraTransform_.position, cameraTF.position) > endMoveCameraDistance_ ||
			Vector3.Distance (clearCameraTransform_.rotation.eulerAngles, cameraTF.rotation.eulerAngles) > endMoveCameraDistance_) {

			cameraTF.position = Vector3.Lerp (cameraTF.position, clearCameraTransform_.position, Time.deltaTime * effectCameraMoveSpeed_);
			cameraTF.rotation = Quaternion.Lerp (cameraTF.rotation, clearCameraTransform_.rotation, Time.deltaTime * effectCameraMoveSpeed_);

			isEnd = false;
		}

		return isEnd;
	}

}
