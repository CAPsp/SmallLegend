using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int health_;
	[SerializeField] Material damageMeshMaterial_;
	[SerializeField] float damageEffectTime_	= 0.5f;

	AudioSource hitSE_;
	MeshRenderer[] modelMeshes_;
    Material[] defaultMeshMaterials_;
    float timer_ = 0f;


	void Awake(){
		hitSE_ 			= GetComponent<AudioSource> ();
		modelMeshes_ 	= gameObject.GetComponentsInChildren<MeshRenderer> ();

        // メッシュごとに割り当てられてるテクスチャを保存
        defaultMeshMaterials_ = new Material[modelMeshes_.Length];
        for(int i = 0; i < modelMeshes_.Length; i++) {
            defaultMeshMaterials_[i] = modelMeshes_[i].material;
        }
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
				MeshesChange (null);
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

    // nullが引数に入ったらデフォルトのメッシュに戻す
	void MeshesChange(Material toMesh){

        if(toMesh == null) {
            
            for(int i = 0; i < modelMeshes_.Length; i++) {
                modelMeshes_[i].material = defaultMeshMaterials_[i];
            }

            return;
        }

		for (int i = 0; i < modelMeshes_.Length; i++) {
			modelMeshes_ [i].material = toMesh;
		}

	}

}
