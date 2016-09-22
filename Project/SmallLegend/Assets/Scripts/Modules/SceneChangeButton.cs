using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Original.UI;

public class SceneChangeButton : MonoBehaviour {

	public string nextSceneName_;
	public Image displayImage_;
	public float darckChangeSpeed_ = 1.0f;

	bool isPushed_ 	= false;
	DarkChange darkChange_;

	void Update(){

		if (isPushed_) {
			if (darkChange_.CallAtUpdate()) {
				SceneManager.LoadScene (nextSceneName_);
			}
		}

	}

	public void Push(){
		isPushed_ = true;
		darkChange_ = new DarkChange (displayImage_, darckChangeSpeed_);
	}

}
