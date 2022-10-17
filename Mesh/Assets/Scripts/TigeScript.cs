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

		// si le pickup est activé
		if (activated) {
			if (tuile == null) {
				transform.parent.transform.GetChild (1).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			}
		}
		// si le pickup n'est pas encore activé
		else {
			// test du timer
			if (timer > 5) {
				// s'il n'y a pas de tuile en dessous
				if (!tilePresent) {
					// reroll
					//Debug.Log("reroll");
					GameManagerScript.removeBonus ();
					Destroy (transform.parent.gameObject);
				} else {
					// le pickup s'active
					activated = true;
					// le pickup est affiché
					transform.parent.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.SetActive (true);
					// mise en place du timer du GameManager
					GameManagerScript.pickUpPopWaitTimer = true;
				}
			}

		}
	}

	void OnTriggerEnter(Collider other) {

		// si un tuile est touchée, alors l'emplacement est bon
		if (other.gameObject.CompareTag("Tile")){
			tilePresent = true;
			tuile = other.gameObject;
		}
	}
}
