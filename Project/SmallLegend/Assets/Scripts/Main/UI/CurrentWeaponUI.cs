using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour {

	Image image_;

	void Awake(){
		image_ = GetComponent<Image> ();
		image_.material = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Bow>().gameObject.GetComponent<MeshRenderer>().material;
	}

	public void ChangeImageUI(Material to){
		image_.material = to;
	}

}
