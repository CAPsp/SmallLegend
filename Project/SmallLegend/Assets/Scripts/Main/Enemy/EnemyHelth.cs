using UnityEngine;
using System.Collections;

public class EnemyHelth : MonoBehaviour {

    public int helth_;

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == "Ammo") {

			Arrow arrow = other.gameObject.GetComponent<Arrow> ();
			int damage = (arrow != null) ? arrow.damage : 10;

            helth_ -= damage;
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
