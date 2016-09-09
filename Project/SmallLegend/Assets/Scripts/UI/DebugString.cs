using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugString : MonoBehaviour {

    public Transform[] transform_;

	static string text_ = "";
    Text debugString_;

    void Awake() {
        debugString_ = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        debugString_.text = "";

        for (int i = 0; i < transform_.Length; i++) {
            debugString_.text += transform_[i].ToString() + "\n";
            debugString_.text += "Transform: " + transform_[i].position.ToString() + "\n";
            debugString_.text += "Rotation: " + transform_[i].rotation.ToString() + "\n\n";
        }

		debugString_.text += text_;
	}

	// 表示する文字を置き換える
	public static void ReplaceText(string text){
		text_ = text;
	}

	// 表示する文字を付け足す
	public static void AddText(string text){
		text_ += text;
	}

}
