using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public AudioSource audioBGM_;
	public float endBGMLimitVolume_ = 0.05f;	// BGMが終わったと判断するvolume
	public Camera mainCamera_;

	GameObject playerObject_;
	bool isClear_ = false;

	void Awake(){
		playerObject_ = GameObject.FindWithTag ("Player");
	}

	void Update(){

		if (isClear_) {

			if (ClearEffect ()) {
				Debug.Log ("End");
			}

		}

	}

	// 何かに当たったら消える
	void OnCollisionEnter(Collision other) {

		if (other.gameObject == playerObject_) {
			ReadyClearEffect ();
		}

	}

	// クリア時の初期化処理
	void ReadyClearEffect(){
		
		// ユーザからの入力初期化
		Input.ResetInputAxes ();

		// プレイヤーを操作不能にする
		playerObject_.GetComponent<PlayerMovement> ().enabled 	= false;
		playerObject_.GetComponent<PlayerAnchor> ().enabled 	= false;

		// カメラの操作権を奪う
		Vector3 currentCameraPoint 		= mainCamera_.transform.parent.transform.parent.transform.position;
		mainCamera_.transform.parent 	= null;
		mainCamera_.transform.position 	= currentCameraPoint;

		isClear_ = true;

	}

	// エフェクトが終わってなければfalseを返す
	bool ClearEffect(){

		bool isEnd = true;

		// BGMをフェードアウトする処理
		if (audioBGM_.isPlaying) {
			audioBGM_.volume = Mathf.Lerp (audioBGM_.volume, 0.0f, Time.deltaTime);

			if (audioBGM_.volume <= endBGMLimitVolume_) {
				audioBGM_.Stop ();
			}

			isEnd = false;;
		}

		// カメラをPlayerの前に持ってくる処理


		return isEnd;
	}

}
