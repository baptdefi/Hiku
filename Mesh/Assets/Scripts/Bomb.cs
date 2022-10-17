using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private float bombTimer = 0;
	private float bombDuration = 70;
	private Tuile t;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Timer progress
		if (bombTimer < bombDuration) {
			//Debug.Log ("timer augmente");
			//Debug.Log (bombTimer);
			bombTimer++;
		} else {
			//Debug.Log ("destruction");
			// Tile destruction
			t.destroyTile ();
			// Bomb destruction
			Destroy (gameObject);
		}


		if (transform.position.y < -5) {
			Debug.Log ("Y bas -> destruction de la bombe");
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collisionInfo){
		Debug.Log ("collision");
		if (collisionInfo.gameObject.CompareTag ("Tile")) {
			Debug.Log ("collision avec une tuile");

			//GetComponent<SphereCollider>().isTrigger = true;
			//GetComponent<Rigidbody> ().useGravity = false;
			//GetComponent<Rigidbody> ().isKinematic = true;

			t = collisionInfo.collider.gameObject.GetComponent<Tuile> ();
		}
	}
}
