using UnityEngine;using System.Collections;public class Anchor : MonoBehaviour {
    Rigidbody rigidbody_;
    PlayerAnchor playerAnchor_;

    void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerAnchor_ = player.GetComponent<PlayerAnchor>();

        rigidbody_ = GetComponent<Rigidbody>();
    }

    // 何かにぶつかったら飛びつける場所か判断してその場所を通知
    void OnCollisionEnter(Collision other) {

        GameObject parent = FindParent(other.gameObject);

        switch (parent.tag) {

            case "Environment":
            case "Enemy":
                rigidbody_.velocity = Vector3.zero;
                playerAnchor_.SetAnchorInfo(other.transform);
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
        
    }}