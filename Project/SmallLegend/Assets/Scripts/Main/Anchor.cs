using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour {

	public float limitAnchorLength_ = 20f;
	public float disableLength_		= 2f;	// 戻ってきた後に消える判定をする最大距離範囲

	Vector3 initialPoint_;		// 射出直後のアンカーの場所
    Rigidbody rigidbody_;
    PlayerAnchor playerAnchor_;
	bool isReturning_;

    void Awake() {
		initialPoint_ 	= transform.position;
		rigidbody_ 		= GetComponent<Rigidbody> ();

		GameObject player 	= GameObject.FindGameObjectWithTag("Player");
		playerAnchor_ 		= player.GetComponent<PlayerAnchor>();
		isReturning_ 		= false;
    }

	void Update(){

		if (isReturning_) {

			if (Vector3.Distance (initialPoint_, transform.position) <= disableLength_) {
				Destroy (this.gameObject);
			}

		}
		else {

			// アンカーの射程距離を超えていたら当たり判定を決して戻ってくる
			if (limitAnchorLength_ < Vector3.Distance (initialPoint_, transform.position)) {
				rigidbody_.velocity = rigidbody_.velocity * (-1);
				GetComponent<Collider> ().enabled = false;
				isReturning_ = true;
			}

		}

	}

    // 何かにぶつかったら飛びつける場所か判断してその場所を通知
    void OnCollisionEnter(Collision other) {

        GameObject parent = FindParent(other.gameObject);

        switch (parent.tag) {

            case "Environment":
            case "Enemy":
                rigidbody_.velocity = Vector3.zero;
                playerAnchor_.SetAnchorInfo(rigidbody_.transform);
                break;

        }


    }

    // 一番親のオブジェクトのタグを持ってくる
    GameObject FindParent(GameObject child) {

        if(child.transform.parent == null) {
            return child;
        }
        else {
            return FindParent(child.transform.parent.gameObject);
        }
        
    }

}
