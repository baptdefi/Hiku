using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.position - GameObject.Find ("Main Camera").transform.position;
		transform.rotation = Quaternion.LookRotation (direction, Vector3.up);
	}
}
