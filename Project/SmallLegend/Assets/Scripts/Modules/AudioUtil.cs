using UnityEngine;
using System.Collections;

namespace Utility{

	// サウンド関連の汎用的な処理をまとめたクラス
	public class AudioUtil{

		// 音楽を再生する(AudioSourceが複数のAudioClipを扱う際使う)
		public static void PlayFromClips(AudioSource source, AudioClip clip){
			source.clip = clip;
			source.Play ();
		}

		// 音楽を停止する(AudioSourceが複数のAudioClipを扱う際使う)
		public static void StopFromClips(AudioSource source, AudioClip clip){
			if (source.isPlaying && source.clip == clip) {
				source.Stop ();
			}
		}

	}

}