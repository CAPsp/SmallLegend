using UnityEngine;using System.Collections;public class Anchor : MonoBehaviour {
    Rigidbody rigidbody_;

    void Awake() {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    // 何かにぶつかったら飛びつける場所か判断してその場所を通知
    void OnCollisionEnter(Collision other) {

        GameObject parent = FindParent(other.gameObject);

        switch (parent.tag) {

            case "Environment":
            case "Enemy":
                Debug.Log("Collide");
                rigidbody_.velocity = Vector3.zero;
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