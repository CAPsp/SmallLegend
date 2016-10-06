using UnityEngine;
using System.Collections;

// 他のGameObjectを出現させるスイッチとなる床
public class FloorSwitch : MonoBehaviour {

	[SerializeField] GameObject[] inActivateObj_;

	void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag == "Player" && inActivateObj_ != null) {

			for (int i = 0; i < inActivateObj_.Length; i++) {
				inActivateObj_[i].SetActive (true);
			}
		}       

	}


}
