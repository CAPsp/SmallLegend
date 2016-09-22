using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Original.UI{

	// 暗転を処理するクラス
	public class DarkChange{

		Image targetImage_;		// エフェクトをかける対象
		float speed_;
		float alpha_ = 0f;

		public DarkChange(Image target, float speed){
			targetImage_ = target;
			speed_ = speed;

			targetImage_.color = Color.clear;
		}

		// Update関数で呼び出すこと
		// true = 暗転終了
		public bool CallAtUpdate(){

			targetImage_.color = new Color (0f, 0f, 0f, alpha_);
			alpha_ += Time.deltaTime * speed_;

			return (targetImage_.color.a >= 1f);

		}

	}

}