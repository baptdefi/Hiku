using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	private GameObject tuile;
	public GameObject player = null;

	// Use this for initialization
	void Start () {
		// tuile doit etre initilisé pour ne pas être null de base
		tuile = GameObject.Find ("DebugTrapCube");
	}
	
	// Update is called once per frame
	void Update () {


		if (tuile == null) {
			this.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}

		if (player != null) {
			this.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y-0.9f, player.transform.position.z);
		}

		if (transform.position.y < -5) {
			Debug.Log ("Y bas -> destruction du piege");
			destroyTrap ();
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag("Tile")){

			Debug.Log ("save de la tuile");
			tuile = other.gameObject;
			Debug.Log (tuile);
		}

		if (other.gameObject.CompareTag("Player")){

			if (player == null) {

				Debug.Log ("save du player");
				player = other.gameObject;
				player.GetComponent<PlayerController> ().becomeTrapped (this.gameObject, 1);
				Debug.Log (player);
			}
		}
	}

	public void destroyTrap(){
		Destroy (gameObject);
	}
}
