using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour {

	[SerializeField] Sprite bowSprite_;
	[SerializeField] Sprite machinGunSprite_;

	Image image_;

	void Awake(){
		image_ = GetComponent<Image> ();
		image_.sprite = bowSprite_;
	}

	public void ChangeImageUI(Material to){

		if (to == GameObject.FindWithTag ("Player").GetComponentInChildren<Bow> ().GetComponent<MeshRenderer> ().material) {
			image_.sprite = bowSprite_;
		}
		else {
			image_.sprite = machinGunSprite_;
		}

	}

}
