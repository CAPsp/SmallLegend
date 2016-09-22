using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Original.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {

	[SerializeField] Image displayImage_;

	bool isPushed_ 	= false;
	DarkChange darkChange_;

	void Update(){

		if (isPushed_) {
			if (darkChange_.CallAtUpdate()) {
				Application.Quit ();
			}
		}

	}

	public void Push(){
		isPushed_ = true;
		darkChange_ = new DarkChange (displayImage_, 0.5f);
	}
}
