using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	private GameObject tuile;
	public GameObject player = null;

	// Use this for initialization
	void Start () {
        // tile initialized to be not null
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
            // trap dropped out of play area
            destroyTrap();
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag("Tile")){
            // tile ref save
			tuile = other.gameObject;
		}

		if (other.gameObject.CompareTag("Player")){

			if (player == null) {
                // player ref save
				player = other.gameObject;
				player.GetComponent<PlayerController> ().becomeTrapped (this.gameObject, 1);
			}
		}
	}

	public void destroyTrap(){
		Destroy (gameObject);
	}
}
