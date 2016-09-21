using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {
	
	public void Push(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
