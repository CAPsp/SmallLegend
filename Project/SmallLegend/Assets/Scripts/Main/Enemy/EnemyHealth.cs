using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health_;

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == "Ammo") {

			Arrow arrow = other.gameObject.GetComponent<Arrow> ();
			int damage = (arrow != null) ? arrow.damage : 10;

            health_ -= damage;
            Destroy(other.gameObject);

            Debug.Log("Health : " + health_);
        }

    }

    void Update() {

        if(health_ <= 0) {
            Destroy(gameObject);
        }

    }

}
