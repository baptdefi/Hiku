using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sardine : MonoBehaviour {

	private float speed = 30f;
	private Rigidbody rb;
	private Vector3 direction;

    // OLD BEHAVIOUR TEST
    //private float maxVelocityx = 20.0f;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> ();
		direction = transform.forward;
		rb.velocity = direction * speed;
	}
	
	// Update is called once per frame
	void Update () {

		//if (transform.position.y <= -10 || transform.position.z <= -35 || transform.position.z >= 35 || transform.position.x <= -80 || transform.position.x >= 30){
		if(transform.position.y <= -10){
			Destroy (gameObject);
		}
	}

	void FixedUpdate(){
		if (!rb.velocity.Equals(direction * speed) && !rb.useGravity) {
			rb.velocity = direction * speed;
		}

        // OLD BEHAVIOUR TEST
		/*if (rb.velocity.magnitude <= maxVelocityx) {
			rb.AddForce (direction, ForceMode.VelocityChange);
		}*/
	}

	void OnTriggerEnter (Collider other){
		// activate gravity if object outside play area
		if (other.gameObject.CompareTag("Bounds")){
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.FreezeRotation;
			rb.AddForce (direction);
		}
	}
}
