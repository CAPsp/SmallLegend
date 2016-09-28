using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour {

	[SerializeField] Transform targetPoint_;
	[SerializeField] float intervalTime_ 	= 0.2f;
	[SerializeField] int damage_ 			= 20;
	[SerializeField] float maxDispersion_ 	= 0.5f;	// 目標ポイントからの最大ばらつき距離
	[SerializeField] float range_			= 20f;
	[SerializeField] float addPower_		= 5f;

	float timer_;
	Ray shootRay_;
	LineRenderer fireLineRenderer_;

	void Awake(){
		timer_ 				= intervalTime_;
		fireLineRenderer_ 	= GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
	
		if (timer_ < intervalTime_) {
			fireLineRenderer_.enabled = false;
			timer_ += Time.deltaTime;
			return;
		}

		// 押し続けている間打ち続ける
		if (Input.GetButton ("Fire1")) {
			Fire ();
		}

	}

	// 弾は即着弾
	void Fire(){

		timer_ = 0f;

		// 発射角度割り出し
		Vector3 shotAngle = targetPoint_.position - transform.position;
		shotAngle = shotAngle.normalized;

		// 半径１の球体内部おランダムな点を返す関数を使って発射位置をばらつかせる
		transform.localPosition = new Vector3(0f, 0f, 0f);
		Vector3 dispersionPoint = Random.insideUnitSphere * maxDispersion_;
		transform.localPosition += dispersionPoint;

		shootRay_.origin 	= transform.position;
		shootRay_.direction = shotAngle;

		Vector3[] lineRenderer = new Vector3[2];
		lineRenderer [0] = shootRay_.origin;

		RaycastHit shootHit;
		if (Physics.Raycast (shootRay_, out shootHit, range_)) {

			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null) {
				enemyHealth.TakeDamage (damage_);
				enemyHealth.transform.gameObject.GetComponent<Rigidbody> ().AddForce (shotAngle * addPower_, ForceMode.VelocityChange);
			}

			lineRenderer [1] = shootHit.point;

		}
		else {
			lineRenderer [1] = shootRay_.origin + shootRay_.direction * range_;
		}

		fireLineRenderer_.SetPositions (lineRenderer);
		fireLineRenderer_.enabled = true;

	}
}
