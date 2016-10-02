using UnityEngine;
using System.Collections;
using Utility;

public class Anchor : MonoBehaviour {

	public static float limitAnchorLength_ 	= 20f;

	[SerializeField] float disableLength_		= 2f;			// 戻ってきた後に消える判定をする最大距離範囲
	[SerializeField] AudioClip audioHitClip_;

    PlayerAnchor playerAnchor_;
	Transform playerTransform_;
	Rigidbody rigidbody_;
	LineRenderer anchorLine_;
	AudioSource audioSource_;
	bool isReturning_;

    void Awake() {
		
		GameObject player 	= GameObject.FindGameObjectWithTag("Player");
		playerAnchor_ 		= player.GetComponent<PlayerAnchor>();
		playerTransform_ 	= player.transform;

		rigidbody_ 			= GetComponent<Rigidbody> ();
		anchorLine_ 		= GetComponent<LineRenderer> ();
		audioSource_		= GetComponent<AudioSource>();
		isReturning_ 		= false;

		audioSource_.Play ();	// AudioSourceに初期で入っている音をならす(射出音を想定)
    }

	void Update(){

		// アンカーの軌跡を表示
		anchorLine_.SetPosition (0, playerTransform_.position);
		anchorLine_.SetPosition (1, transform.position);

		if (isReturning_) {

			if (Vector3.Distance (playerTransform_.position, transform.position) <= disableLength_) {
				Destroy (this.gameObject);
			}

			// プレイヤー方向を計算
			Vector3 angle = playerTransform_.position - transform.position;
			angle.Normalize ();

			// もともとの速度の大きさと方向から速度を修正
			rigidbody_.velocity = rigidbody_.velocity.magnitude * angle;
		}
		else {

			// アンカーの射程距離を超えていたら当たり判定を決してプレイヤーのところへ戻ってくるよう仕向ける
			if (limitAnchorLength_ < Vector3.Distance (playerTransform_.position, transform.position)) {
				GetComponent<Collider> ().enabled = false;
				isReturning_ = true;
			}

		}

	}

    // 何かにぶつかったら飛びつける場所か判断してその場所を通知
    void OnCollisionEnter(Collision other) {

		GameObject parent = HierarchyUtil.FindParent(other.gameObject);

        switch (parent.tag) {

		case "Environment":
		case "Enemy":
			AudioUtil.PlayFromClips (audioSource_, audioHitClip_);
            rigidbody_.velocity = Vector3.zero;
            playerAnchor_.SetAnchorInfo(rigidbody_.transform);
            break;

        }

    }

}
