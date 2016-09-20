using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public int damage;

	void Awake(){
		AudioSource seOrigin = GetComponent<AudioSource> ();

		AudioSource.PlayClipAtPoint (seOrigin.clip, transform.position, seOrigin.volume);
	}

}
