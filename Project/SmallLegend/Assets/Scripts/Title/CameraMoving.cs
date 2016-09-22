using UnityEngine;
using System.Collections;

public class CameraMoving : MonoBehaviour {

	[SerializeField] float moveSpeed_ 	= 0.5f;
	[SerializeField] float rotateSpeed_ = 0.5f;
	[SerializeField] GameObject[] candidatePositions_;

	Transform destination_;
	int index_;

	void Start(){
		destination_ = PositionSelect (-1);
	}

	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp (transform.position, destination_.position, Time.deltaTime * moveSpeed_);
		transform.rotation = Quaternion.Lerp (transform.rotation, destination_.rotation, Time.deltaTime * rotateSpeed_);

		// 目的地についたら次の候補を選ぶ
		if (Vector3.Distance (transform.position, destination_.position) <= 1.0f) {
			destination_ = PositionSelect (index_);
		}

	}

	// ランダムに次のカメラの動きの指標となるオブジェクトを候補から選ぶ
	Transform PositionSelect(int before){

		// 直前のものと同じにならないよう選択
		do{
			index_ = Random.Range (0, candidatePositions_.Length);
		}while(index_ == before);

		Transform[] children = candidatePositions_[index_].transform.GetComponentsInChildren<Transform>();
		transform.position = children [1].position;
		transform.rotation = children [1].rotation;

		return children [2];
	}
}
