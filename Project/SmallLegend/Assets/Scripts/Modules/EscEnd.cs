using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscEnd : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown ("escape")) {
			Application.Quit ();
		}
	}
}
