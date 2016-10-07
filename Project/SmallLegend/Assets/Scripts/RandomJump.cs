using UnityEngine;
using System.Collections;

// ぴょんぴょん（ ＾ω＾）
public class RandomJump : MonoBehaviour {

	[SerializeField] float jumpHeight_ 		= 1f;
	[SerializeField] float speed_			= 1f;
	[SerializeField] float judgeInterval 	= 0.5f;

	float originY_;
	bool isJumping_ = false;
	float timer_ 	= 0f;

	void Awake(){
		originY_ = transform.position.y;
	}

	void Update(){
		
		if (!isJumping_) {

			if (timer_ < judgeInterval) {
				timer_ += Time.deltaTime;
				return;
			}
			timer_ = 0f;

			if (Random.value >= 0.5f) {
				isJumping_ = true;
			}
		}
		else {

			Vector3 pos = transform.position;

			transform.position = new Vector3 (pos.x, pos.y + Time.deltaTime * speed_, pos.z);

			// 頂点過ぎたら
			if (speed_ > 0 && originY_ + jumpHeight_ < transform.position.y) {
				speed_ *= (-1f);
			}
			else if(transform.position.y < originY_){
				
				transform.position = new Vector3 (pos.x, originY_, pos.z);
				speed_ *= (-1f);
				isJumping_ = false;
			}

		}

	}

}
