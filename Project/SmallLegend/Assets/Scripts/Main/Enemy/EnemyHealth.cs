using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health_;
	public Material defaultMeshMaterial_;
	public Material damageMeshMaterial_;
	public float damageEffectTime_	= 0.5f;

	AudioSource hitSE_;
	MeshRenderer[] modelMeshes_;
	float timer_ = 0f;


	void Awake(){
		hitSE_ 			= GetComponent<AudioSource> ();
		modelMeshes_ 	= gameObject.GetComponentsInChildren<MeshRenderer> ();
	}

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag == "Ammo") {
			Arrow arrow = other.gameObject.GetComponent<Arrow> ();
			int damage = (arrow != null) ? arrow.damage : 10;

			TakeDamage (damage);
        }

    }

    void Update() {

		// ダメージによって体が赤くなる時間を計算
		if (timer_ < damageEffectTime_) {
			timer_ += Time.deltaTime;

			if (timer_ >= damageEffectTime_) {
				MeshesChange (defaultMeshMaterial_);
			}
		}

        if(health_ <= 0) {
			Death ();
        }

    }

	// SEが鳴って体は一瞬赤くなる
	public void TakeDamage(int damage){

		health_ -= damage;

		hitSE_.Play ();

		MeshesChange (damageMeshMaterial_);
		timer_ = 0f;

		Debug.Log("Health : " + health_);
	}

	void Death(){

		GetComponent<Collider> ().enabled 		= false;

		EnemyMovement movement = GetComponent<EnemyMovement> ();
		if (movement != null) {
			movement.enabled = false;
		}

		if (hitSE_.isPlaying) {
			return;
		}

		Destroy(gameObject);

	}

	void MeshesChange(Material toMesh){

		for (int i = 0; i < modelMeshes_.Length; i++) {
			modelMeshes_ [i].material = toMesh;
		}

	}

}
