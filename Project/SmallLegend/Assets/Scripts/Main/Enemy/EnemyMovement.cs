using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float searchDistance_ = 10f;	// 索敵距離

	Vector3 startPoint_;
	Transform player_;
	NavMeshAgent nav_;

	void Awake(){
		startPoint_ = transform.position;
		player_ 	= GameObject.FindGameObjectWithTag ("Player").transform;
		nav_ 		= GetComponent<NavMeshAgent>();
	}

	void Update(){

		float distance = Vector3.Distance(player_.position, transform.position);

		if (distance <= searchDistance_) {
			nav_.SetDestination (player_.position);
		} 
		else {
			nav_.SetDestination (startPoint_);
		}

	}

}