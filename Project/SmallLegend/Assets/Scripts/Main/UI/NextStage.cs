using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextStage : MonoBehaviour {

	public string nextSceneName_;
	public Image displayImage_;

	bool isPushed_ 	= false;
	float alpha = 0f;

	void Update(){

		if (isPushed_) {
			if (DarkChange ()) {
				SceneManager.LoadScene (nextSceneName_);
			}
		}

	}

	bool DarkChange(){
		
		displayImage_.color = new Color (0f, 0f, 0f, alpha);
		alpha += 0.05f;

		if (displayImage_.color.a >= 1.0f) {
			return true;
		}

		return false;
	}

	public void Push(){
		isPushed_ = true;
		displayImage_.color = Color.clear;
	}

}
