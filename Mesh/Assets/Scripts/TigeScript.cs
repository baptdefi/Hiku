using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigeScript : MonoBehaviour {

	public bool tilePresent = false;
	private bool activated = false;
	private int timer=0;
	private GameObject tuile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		timer++;

		if (activated) {
			if (tuile == null) {
				transform.parent.transform.GetChild (1).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			}
		}
		else {
			if (timer > 5) {
				// no tile under the pickup
				if (!tilePresent) {
					// reroll
					GameManagerScript.removeBonus ();
					Destroy (transform.parent.gameObject);
				} else {
					// activate
					activated = true;
					// display
					transform.parent.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.SetActive (true);
					// GameManager timer starts
					GameManagerScript.pickUpPopWaitTimer = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {

		// if a tile is detected, then the emplacement is good
		if (other.gameObject.CompareTag("Tile")){
			tilePresent = true;
			tuile = other.gameObject;
		}
	}
}
