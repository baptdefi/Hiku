using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private float bombTimer = 0;
	private float bombDuration = 140;
	private Tuile t;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

		// Timer progress
		if (bombTimer < bombDuration) {
			bombTimer++;
		}
        else {
            // Tile destruction
            t.destroyTile ();

			// Bomb destruction
			Destroy (gameObject);
		}

		if (transform.position.y < -5) {
            // bomb dropped out of play area
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collisionInfo){

		if (collisionInfo.gameObject.CompareTag("Tile")) {
            t = collisionInfo.collider.gameObject.GetComponent<Tuile> ();
		}
    }
}
