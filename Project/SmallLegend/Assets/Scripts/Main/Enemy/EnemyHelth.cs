using UnityEngine;
using System.Collections;

public class EnemyHelth : MonoBehaviour {

    public int helth_;

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == "Ammo") {
            helth_ -= 10;
            Destroy(other.gameObject);

            Debug.Log("Helth : " + helth_);
        }

    }

    void Update() {

        if(helth_ <= 0) {
            Destroy(gameObject);
        }

    }

}
