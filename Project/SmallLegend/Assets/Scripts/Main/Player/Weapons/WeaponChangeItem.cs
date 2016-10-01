using UnityEngine;
using System.Collections;

public class WeaponChangeItem : MonoBehaviour {

	[SerializeField, TooltipAttribute("1: MachineGun, other: Bow")] int weaponID_ = 0;
	[SerializeField] CurrentWeaponUI weaponUI_;

	GameObject parentOfWeapons_;
	MonoBehaviour thisWeapon_;		// このオブジェクトが請け負う武器
	MeshRenderer mesh_;

	void Awake(){

		weaponUI_ = GameObject.FindGameObjectWithTag ("UIWeapon").GetComponent<CurrentWeaponUI>();

		parentOfWeapons_ = GameObject.FindGameObjectWithTag ("Weapon").transform.parent.gameObject;

		switch (weaponID_) {
		case 1:
			thisWeapon_ = parentOfWeapons_.GetComponentInChildren<MachineGun> ();
			break;

		default:
			thisWeapon_ = parentOfWeapons_.GetComponentInChildren<Bow> ();
			break;
		}

		mesh_ 			= GetComponentInChildren<MeshRenderer> ();
		mesh_.material 	= thisWeapon_.GetComponent<MeshRenderer> ().material;

	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Player") {
			WeaponChange ();
		}

	}

	void WeaponChange(){

		// 同じ武器ならスルー
		MonoBehaviour current = CurrentWeapon ();
		if (current == thisWeapon_) {
			return;
		}

		current.enabled 	= false;
		thisWeapon_.enabled = true;

		// アイテムとプレイヤーの武器を入れ替えるような形にする
		MonoBehaviour tmp 	= current;
		current 			= thisWeapon_;
		thisWeapon_ 		= tmp;

		mesh_.material = thisWeapon_.gameObject.GetComponent<MeshRenderer> ().material;
		weaponUI_.ChangeImageUI (current.gameObject.GetComponent<MeshRenderer>().material);

	}

	MonoBehaviour CurrentWeapon(){

		if(parentOfWeapons_.GetComponentInChildren<MachineGun> ().enabled){
			return parentOfWeapons_.GetComponentInChildren<MachineGun> ();
		}
		else{
			return parentOfWeapons_.GetComponentInChildren<Bow> ();
		}

	}

}
