using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour {

	public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("J1_R2"))
			SceneManager.LoadScene ("Menu Selection");

		if (!canvas.activeSelf) {
			if (Input.GetButtonDown ("J1_BY"))
				canvas.SetActive (true);
		}
		else {
			if (Input.GetButtonDown ("J1_BB"))
				canvas.SetActive (false);
		}
	}
}
