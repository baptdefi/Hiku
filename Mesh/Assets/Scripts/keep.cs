using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keep : MonoBehaviour {
	public GameObject S1;
	public GameObject S2;

	// Use this for initialization
	private static keep instanceRef;

	void Start()
	{		
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("mainscene"))
        {
			gameObject.SetActive (true);
		}

		if (SceneManager.GetActiveScene () != SceneManager.GetSceneByName ("Load"))
		{
            DontDestroyOnLoad (S1);
			DontDestroyOnLoad (S2);
		}
		else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Load"))
        {
			DestroyObject (gameObject);
		}

		if (instanceRef == null)
        {
			instanceRef = this;
		}
        else
        {
			DestroyObject (gameObject);
		}
	}

	void Update()
	{
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("mainscene"))
        {
			instanceRef.gameObject.SetActive (true);
		}
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Load"))
        {
			this.GetComponentInChildren<Score1> ().reset ();
			this.GetComponentInChildren<Score2> ().reset ();
			gameObject.SetActive (false);
		} 
	}
}
