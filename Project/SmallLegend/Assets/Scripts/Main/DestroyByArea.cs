using UnityEngine;
using System.Collections;

public class DestroyByArea : MonoBehaviour {
    
	public PlayerHealth playerHealth_;

    void OnTriggerExit(Collider other) {

		Debug.Log (other.gameObject.tag);

		if (other.gameObject.tag == "Player") {
			playerHealth_.TakeDamage (PlayerHealth.maxHealth_);
		}
		else {
			Destroy (other.gameObject);
		}

    }

}
