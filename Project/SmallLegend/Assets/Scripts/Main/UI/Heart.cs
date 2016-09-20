using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

	public GameObject heartPrefab_;

	GameObject[] hearts_;

	void Awake(){
		hearts_ = new GameObject[PlayerHealth.maxHealth_];

		for (int i = 0; i < hearts_.Length; i++) {
			hearts_ [i] = Instantiate (heartPrefab_);
			hearts_ [i].transform.SetParent (transform, false);
		}
	}

	public void ChangeActiveHearts(int hp){

		int max = PlayerHealth.maxHealth_;

		// ハートは配列でいう0から順にInactiveになっていく点に注意(ややこしい...)
		int inactiveBorder = max - hp;
		for (int i = 0; i < inactiveBorder && i < max; i++) {

			// 子オブジェクトであるハート(Image)がActiveだったら消す
			Image[] images = hearts_ [i].GetComponentsInChildren<Image>(true);
			images [1].enabled = false;

		}

	}
}
