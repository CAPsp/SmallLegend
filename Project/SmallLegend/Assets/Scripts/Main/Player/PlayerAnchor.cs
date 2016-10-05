using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerAnchor : MonoBehaviour {
	
	[SerializeField] GameObject prefabAnchor_;   			// アンカーとなるprefab
	[SerializeField] Transform targetPoint_;     			// 狙うポイント(Reticle)
	[SerializeField] Transform firePoint_;       			// 生成ポイント
	[SerializeField] float moveVelocity_      	= 50f;
	[SerializeField] float invalidDistance_   	= 3f;   // アンカー移動が無効になるプレイヤーとアンカーの位置
	[SerializeField] float intervalTime_ 		= 0.2f;
	[SerializeField] Image reticle_;
	[SerializeField] Sprite availableReticleSprite_;

	GameObject currentAnchor_	= null;
	Transform anchorPoint_      = null;
	bool isMoving_              = false;
	Rigidbody playerRigidbody_;
	PlayerMovement playerMovement_;
	Collider playerCollider_;
	float timer_;
	AvailableAnchorReticle availableAnchorReticle_;
	
	
	void Awake() {
		playerMovement_     	= GetComponent<PlayerMovement>();
		playerRigidbody_    	= GetComponent<Rigidbody>();
		playerCollider_     	= GetComponent<Collider>();
		timer_					= intervalTime_;

		availableAnchorReticle_ = new AvailableAnchorReticle (reticle_, availableReticleSprite_ ,reticle_.sprite);
	}
	
	// Update is called once per frame
	void Update () {

		timer_ += Time.deltaTime;
		if (timer_ >= float.MaxValue) {	// ないだろうけどオーバーフロー回避
			timer_ = intervalTime_;
		}

		availableAnchorReticle_.Update (firePoint_.position, targetPoint_.position);

		// 右クリックでアンカーをtargetPoint_めがけて射出
		if (timer_ >= intervalTime_ && currentAnchor_ == null && Input.GetButton("Fire2")) {
			Shot();
		}
		
		// アンカーで移動中
		if (isMoving_) {
			Moving();               
		}
		
		// アンカーが刺さったらそのポイントに移動
		else if (anchorPoint_ != null) {
			
			// アンカーで移動できるか判断
			if (CheckValidAnchor()) {
				isMoving_ = true;
				playerMovement_.enabled = false;    // アンカー移動中は通常の移動処理を無効に
			}
			else {
				Destroy(currentAnchor_);
				anchorPoint_ = null;
			}
		}
		
	}
	
	// アンカー移動中にオブジェクトとぶつかったら移動処理終了
	void OnCollisionEnter(Collision other) {
		
		if (isMoving_) {
			isMoving_ = false;
			Destroy(currentAnchor_);
			anchorPoint_ = null;
			playerMovement_.enabled = true;
		}
	}
	
	void Shot() {
		
		// 正規化した発射角度を割り出す
		Vector3 shotAngle = targetPoint_.position - firePoint_.position;
		shotAngle = shotAngle.normalized;
		
		// プレイヤーと被らない位置にアンカー生成
		currentAnchor_ = Instantiate(prefabAnchor_, firePoint_.position, Quaternion.identity) as GameObject;
		currentAnchor_.transform.position += shotAngle * currentAnchor_.transform.localScale.x;
		
		// 等速直線運動
		currentAnchor_.GetComponent<Rigidbody>().velocity = shotAngle * moveVelocity_;

		timer_ = 0f;
	}
	
	void Moving() {

		try{
			// 角度を計算、正規化
			Vector3 basePoint = playerCollider_.transform.position - new Vector3(0f, playerCollider_.bounds.extents.y, 0f);
			Vector3 angle = anchorPoint_.position - basePoint;
			angle.Normalize();
			
			playerRigidbody_.transform.position += angle * (moveVelocity_ * Time.deltaTime);   // 移動速度はアンカーと同じ
		
		}catch(MissingReferenceException){	// anchorPointがnullになるときがあったので例外処理追加
			Debug.Log("(´∀｀)＜ぬるぽ");
			isMoving_ = false;
            playerMovement_.enabled = true;
        }
	
	}
	
	bool CheckValidAnchor() {
		
		// アンカーとプレイヤーの位置が近すぎる場合無効
		float distance = Vector3.Distance(anchorPoint_.position, playerCollider_.transform.position);
		if (distance <= invalidDistance_) {
			return false;
		}
		
		return true;
		
	}
	
	// Anchorクラスから刺さった地点の情報が通知されるメソッド
	public void SetAnchorInfo(Transform point) {
		anchorPoint_ = point;
	}
	
}