using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour {
	
	private int selectionP1 = -1;
	private int selectionP2 = -1;
	private int selectionP3 = -1;
	private int selectionP4 = -1;
	
	public List <GameObject> modelsP1 = new List<GameObject> ();
	public List <GameObject> modelsP2 = new List<GameObject> ();
	public List <GameObject> modelsP3 = new List<GameObject> ();
	public List <GameObject> modelsP4 = new List<GameObject> ();
	
	private bool P1ready = false;
	private bool P2ready = false;
	private bool P3ready = false;
	private bool P4ready = false;
	
	private bool P1ingame = false;
	private bool P2ingame = false;
	private bool P3ingame = false;
	private bool P4ingame = false;
	
	private float P1timer = 0;
	private float P2timer = 0;
	private float P3timer = 0;
	private float P4timer = 0;
	
	private float timerDuration = 15;
	
	private bool launchGame = false;
	private bool explanationsPicture1 = false;
	private bool explanationsPicture2 = false;
	private bool explanationsPicture3 = false;


	

	// Use this for initialization
	void Start () {
		
		// list initialization
		foreach (GameObject go in modelsP1) {
			go.SetActive (false);
		}
		foreach (GameObject go in modelsP2) {
			go.SetActive (false);
		}
		foreach (GameObject go in modelsP3) {
			go.SetActive (false);
		}
		foreach (GameObject go in modelsP4) {
			go.SetActive (false);
		}

		PlayerPrefs.SetInt ("PreferedModel1", selectionP1);
		PlayerPrefs.SetInt ("PreferedModel2", selectionP2);
		PlayerPrefs.SetInt ("PreferedModel3", selectionP3);
		PlayerPrefs.SetInt ("PreferedModel4", selectionP4);

	}
	
	// Update is called once per frame
	void Update () {
		
		// P1
		if (!P1ingame){
			if (Input.GetButtonDown("J1_R2")) {
				P1ingame = true;
				//Debug.Log ("P1ingame");

				selectionP1 = 0;
				modelsP1 [selectionP1].SetActive (true);

				GameObject.Find ("PressStartJ1").GetComponent<Text> ().enabled = false;
				GameObject.Find ("PressAJ1").GetComponent<Text> ().enabled = true;
			}
		}
		else{	
			if (!P1ready){

				bool modelAvailable = true;

				int currentModel = selectionP1;

				do {
					modelAvailable = true;

					if (P2ready && PlayerPrefs.GetInt ("PreferedModel2") == selectionP1) {
						//Debug.Log("P1 : Meme modele que P2");
						modelAvailable = false;
						selectionP1++;
						if (selectionP1 == modelsP1.Count)
							selectionP1 = 0;
					}
					else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3") == selectionP1) {
						//Debug.Log("P1 : Meme modele que P3");
						modelAvailable = false;
						selectionP1++;
						if (selectionP1 == modelsP1.Count)
							selectionP1 = 0;
					}
					else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4") == selectionP1) {
						//Debug.Log("P1 : Meme modele que P4");
						modelAvailable = false;
						selectionP1++;
						if (selectionP1 == modelsP1.Count)
							selectionP1 = 0;
					}
				} while (!modelAvailable);

				modelsP1 [currentModel].SetActive (false);
				modelsP1 [selectionP1].SetActive (true);
					
				if (P1timer == 0){
					
					//Get axis inputs
					float hAxis = Input.GetAxis ("J1_LStick_X");
					

					
					if (hAxis <= -0.9f){
						
						// désactive le model
						modelsP1 [selectionP1].SetActive (false);
						
						do{
							modelAvailable = true;
							// passe au model suivant
							selectionP1++;
							if (selectionP1 == modelsP1.Count)
								selectionP1 = 0;
							
							if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP1){
								//Debug.Log("P1 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP1){
								//Debug.Log("P1 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP1){
								//Debug.Log("P1 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);

						// active le model suivant
						modelsP1 [selectionP1].SetActive (true);
						
						P1timer ++;
					}
					
					if (hAxis >= 0.9f) {	
								
						modelsP1 [selectionP1].SetActive (false);
						
						do{
							modelAvailable = true;
								
							// passe au model suivant
							selectionP1--;
							if (selectionP1 < 0)
								selectionP1 = modelsP1.Count - 1;
							
							else if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP1){
								//Debug.Log("P1 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP1){
								//Debug.Log("P1 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP1){
								//Debug.Log("P1 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);
						
						modelsP1 [selectionP1].SetActive (true);
						
						P1timer ++;
					}
				}
				else{
					if (P1timer == timerDuration){
						P1timer = 0;
					}
					else{
						P1timer ++;
					}
				}
				
				// remplacer par start
				if (Input.GetButtonDown("J1_BA")) {
					
					// set l'index du model
					PlayerPrefs.SetInt ("PreferedModel1", selectionP1);
					
					P1ready = true;
					//Debug.Log ("P1ready");

					GameObject.Find ("PressAJ1").GetComponent<Text> ().enabled = false;
					GameObject.Find ("ReadyJ1").GetComponent<Text> ().enabled = true;
				}
			}
		}
		
		// P2
		if (!P2ingame){
			if (Input.GetButtonDown("J2_R2")) {
				P2ingame = true;
				//Debug.Log ("P2ingame");

				selectionP2 = 0;
				modelsP2 [selectionP2].SetActive (true);

				GameObject.Find ("PressStartJ2").GetComponent<Text> ().enabled = false;
				GameObject.Find ("PressAJ2").GetComponent<Text> ().enabled = true;
			}
		}
		else{	
			if (!P2ready){

				bool modelAvailable = true;

				int currentModel = selectionP2;

				do {
					modelAvailable = true;

					if (P1ready && PlayerPrefs.GetInt ("PreferedModel1") == selectionP2) {
						//Debug.Log("P2 : Meme modele que P1");
						modelAvailable = false;
						selectionP2++;
						if (selectionP2 == modelsP2.Count)
							selectionP2 = 0;
					}
					else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3") == selectionP2) {
						//Debug.Log("P2 : Meme modele que P3");
						modelAvailable = false;
						selectionP2++;
						if (selectionP2 == modelsP2.Count)
							selectionP2 = 0;
					}
					else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4") == selectionP2) {
						//Debug.Log("P2 : Meme modele que P4");
						modelAvailable = false;
						selectionP2++;
						if (selectionP2 == modelsP2.Count)
							selectionP2 = 0;
					}
				} while (!modelAvailable);

				modelsP2 [currentModel].SetActive (false);
				modelsP2 [selectionP2].SetActive (true);
					
				if (P2timer == 0){
					
					//Get axis inputs
					float hAxis = Input.GetAxis ("J2_LStick_X");
					

					
					if (hAxis <= -0.9f){
						
						// désactive le model
						modelsP2 [selectionP2].SetActive (false);
						
						do{
							modelAvailable = true;
							// passe au model suivant
							selectionP2++;
							if (selectionP2 == modelsP2.Count)
								selectionP2 = 0;
							
							if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP2){
								////Debug.Log("P2 : Meme modele que P1");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP2){
								////Debug.Log("P2 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP2){
								////Debug.Log("P2 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);

						// active le model suivant
						modelsP2 [selectionP2].SetActive (true);
						
						P2timer ++;
					}
					
					if (hAxis >= 0.9f) {	
								
						modelsP2 [selectionP2].SetActive (false);
						
						do{
							modelAvailable = true;
								
							// passe au model suivant
							selectionP2--;
							if (selectionP2 < 0)
								selectionP2 = modelsP2.Count - 1;
							
							if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP2){
								////Debug.Log("P2 : Meme modele que P1");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP2){
								////Debug.Log("P2 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP2){
								////Debug.Log("P2 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);
						
						modelsP2 [selectionP2].SetActive (true);
						
						P2timer ++;
					}
				}
				else{
					if (P2timer == timerDuration){
						P2timer = 0;
					}
					else{
						P2timer ++;
					}
				}
				
				// remplacer par start
				if (Input.GetButtonDown("J2_BA")) {
					
					// set l'index du model
					PlayerPrefs.SetInt ("PreferedModel2", selectionP2);
					
					P2ready = true;
					////Debug.Log ("P2ready");

					GameObject.Find ("PressAJ2").GetComponent<Text> ().enabled = false;
					GameObject.Find ("ReadyJ2").GetComponent<Text> ().enabled = true;
				}
			}
		}
		
		// P3
		if (!P3ingame){
			if (Input.GetButtonDown("J3_R2")) {
				P3ingame = true;
				////Debug.Log ("P3ingame");

				selectionP3 = 0;
				modelsP3 [selectionP3].SetActive (true);


				GameObject.Find ("PressStartJ3").GetComponent<Text> ().enabled = false;
				GameObject.Find ("PressAJ3").GetComponent<Text> ().enabled = true;

			}
		}
		else{	
			if (!P3ready){

				bool modelAvailable = true;

				int currentModel = selectionP3;

				do {
					modelAvailable = true;

					if (P2ready && PlayerPrefs.GetInt ("PreferedModel2") == selectionP3) {
						////Debug.Log("P3 : Meme modele que P2");
						modelAvailable = false;
						selectionP3++;
						if (selectionP3 == modelsP3.Count)
							selectionP3 = 0;
					}
					else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1") == selectionP3) {
						//Debug.Log("P3 : Meme modele que P1");
						modelAvailable = false;
						selectionP3++;
						if (selectionP3 == modelsP1.Count)
							selectionP3 = 0;
					}
					else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4") == selectionP3) {
						//Debug.Log("P3 : Meme modele que P4");
						modelAvailable = false;
						selectionP3++;
						if (selectionP3 == modelsP3.Count)
							selectionP3 = 0;
					}
				} while (!modelAvailable);

				modelsP3 [currentModel].SetActive (false);
				modelsP3 [selectionP3].SetActive (true);

					
				if (P3timer == 0){
					
					//Get axis inputs
					float hAxis = Input.GetAxis ("J3_LStick_X");
					

					
					if (hAxis <= -0.9f){
						
						// désactive le model
						modelsP3 [selectionP3].SetActive (false);
						
						do{
							modelAvailable = true;
							// passe au model suivant
							selectionP3++;
							if (selectionP3 == modelsP3.Count)
								selectionP3 = 0;
							
							if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP3){
								//Debug.Log("P3 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP3){
								//Debug.Log("P3 : Meme modele que P1");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP3){
								//Debug.Log("P3 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);

						// active le model suivant
						modelsP3 [selectionP3].SetActive (true);
						
						P3timer ++;
					}
					
					if (hAxis >= 0.9f) {	
								
						modelsP3 [selectionP3].SetActive (false);
						
						do{
							modelAvailable = true;
								
							// passe au model suivant
							selectionP3--;
							if (selectionP3 < 0)
								selectionP3 = modelsP3.Count - 1;
							
							if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP3){
								//Debug.Log("P3 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP3){
								//Debug.Log("P3 : Meme modele que P1");
								modelAvailable = false;
							}
							else if (P4ready && PlayerPrefs.GetInt ("PreferedModel4")==selectionP3){
								//Debug.Log("P3 : Meme modele que P4");
								modelAvailable = false;
							}
						}while(!modelAvailable);
						
						modelsP3 [selectionP3].SetActive (true);
						
						P3timer ++;
					}
				}
				else{
					if (P3timer == timerDuration){
						P3timer = 0;
					}
					else{
						P3timer ++;
					}
				}
				
				// remplacer par start
				if (Input.GetButtonDown("J3_BA")) {
					
					// set l'index du model
					PlayerPrefs.SetInt ("PreferedModel3", selectionP3);
					
					P3ready = true;

					//Debug.Log ("P3ready");

					GameObject.Find ("PressAJ3").GetComponent<Text> ().enabled = false;
					GameObject.Find ("ReadyJ3").GetComponent<Text> ().enabled = true;
				}
			}
		}
		
		// P4
		if (!P4ingame){
			if (Input.GetButtonDown("J4_R2")) {
				P4ingame = true;
				//Debug.Log ("P4ingame");

				selectionP4 = 0;
				modelsP4 [selectionP4].SetActive (true);

				GameObject.Find ("PressStartJ4").GetComponent<Text> ().enabled = false;
				GameObject.Find ("PressAJ4").GetComponent<Text> ().enabled = true;
			}
		}
		else{	
			if (!P4ready){

				bool modelAvailable = true;

				int currentModel = selectionP4;

				do {
					modelAvailable = true;

					if (P2ready && PlayerPrefs.GetInt ("PreferedModel2") == selectionP4) {
						//Debug.Log("P4 : Meme modele que P2");
						modelAvailable = false;
						selectionP4++;
						if (selectionP4 == modelsP4.Count)
							selectionP4 = 0;
					}
					else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3") == selectionP4) {
						//Debug.Log("P4 : Meme modele que P3");
						modelAvailable = false;
						selectionP4++;
						if (selectionP4 == modelsP4.Count)
							selectionP4 = 0;
					}
					else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1") == selectionP4) {
						//Debug.Log("P4 : Meme modele que P1");
						modelAvailable = false;
						selectionP4++;
						if (selectionP4 == modelsP4.Count)
							selectionP4 = 0;
					}
				} while (!modelAvailable);

				modelsP4 [currentModel].SetActive (false);
				modelsP4 [selectionP4].SetActive (true);
					
				if (P4timer == 0){
					
					//Get axis inputs
					float hAxis = Input.GetAxis ("J4_LStick_X");
					

					
					if (hAxis <= -0.9f){
						
						// désactive le model
						modelsP4 [selectionP4].SetActive (false);
						
						do{
							modelAvailable = true;
							// passe au model suivant
							selectionP4++;
							if (selectionP4== modelsP4.Count)
								selectionP4 = 0;
							
							if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP4){
								//Debug.Log("P4 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP4){
								//Debug.Log("P4 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP4){
								//Debug.Log("P4 : Meme modele que P1");
								modelAvailable = false;
							}
						}while(!modelAvailable);

						// active le model suivant
						modelsP4 [selectionP4].SetActive (true);
						
						P4timer ++;
					}
					
					if (hAxis >= 0.9f) {	
								
						modelsP4 [selectionP4].SetActive (false);
						
						do{
							modelAvailable = true;
								
							// passe au model suivant
							selectionP4--;
							if (selectionP4 < 0)
								selectionP4 = modelsP4.Count - 1;
							
							if (P2ready && PlayerPrefs.GetInt ("PreferedModel2")==selectionP4){
								//Debug.Log("P4 : Meme modele que P2");
								modelAvailable = false;
							}
							else if (P3ready && PlayerPrefs.GetInt ("PreferedModel3")==selectionP4){
								//Debug.Log("P4 : Meme modele que P3");
								modelAvailable = false;
							}
							else if (P1ready && PlayerPrefs.GetInt ("PreferedModel1")==selectionP4){
								//Debug.Log("P4 : Meme modele que P1");
								modelAvailable = false;
							}
						}while(!modelAvailable);
						
						modelsP4 [selectionP4].SetActive (true);
						
						P4timer ++;
					}
				}
				else{
					if (P4timer == timerDuration){
						P4timer = 0;
					}
					else{
						P4timer ++;
					}
				}
				
				// remplacer par start
				if (Input.GetButtonDown("J4_BA")) {
					
					// set l'index du model
					PlayerPrefs.SetInt ("PreferedModel4", selectionP4);
					
					P4ready = true;

					//Debug.Log ("P4ready");

					GameObject.Find ("PressAJ4").GetComponent<Text> ().enabled = false;
					GameObject.Find ("ReadyJ4").GetComponent<Text> ().enabled = true;
				}
			}
		}


		if (launchGame){
			// Switch scene
			SceneManager.LoadScene ("mainscene");
		}
		else if (explanationsPicture3){

			GameObject.Find ("ExplanationsPicture2").GetComponent<Image> ().enabled = false;
			GameObject.Find ("ExplanationsPicture3").GetComponent<Image> ().enabled = true;

			if (Input.GetButtonDown("J1_R2")) {
				explanationsPicture3 = false;
				launchGame = true;
			}
		}
		else if (explanationsPicture2){

			GameObject.Find ("ExplanationsPicture1").GetComponent<Image> ().enabled = false;
			GameObject.Find ("ExplanationsPicture2").GetComponent<Image> ().enabled = true;

			if (Input.GetButtonDown("J1_R2")) {
				explanationsPicture2 = false;
				explanationsPicture3 = true;
			}
		}
		else if (explanationsPicture1){

			GameObject.Find ("ExplanationsPicture1").GetComponent<Image> ().enabled = true;

			if (Input.GetButtonDown("J1_R2")) {
				explanationsPicture1 = false;
				explanationsPicture2 = true;
			}
		}


		// 4 players ready
		if (P1ingame && P2ingame && P3ingame && P4ingame){
			if (P1ready && P2ready && P3ready && P4ready){
				GameObject.Find ("PressStartToPlay").GetComponent<Text> ().text = "Press Start to play";
				if (Input.GetButtonDown("J1_R2")) {
					explanationsPicture1 = true;
				}
			}	
		}
		// 3 players ready
		else if (P1ingame && P2ingame && P3ingame){
			if (P1ready && P2ready && P3ready){
				GameObject.Find ("PressStartToPlay").GetComponent<Text> ().text = "Press Start to play";
				if (Input.GetButtonDown("J1_R2")) {
					explanationsPicture1 = true;
				}
			}	
		}
		// 2 players ready
		else if (P1ingame && P2ingame){
			if (P1ready && P2ready){
				GameObject.Find ("PressStartToPlay").GetComponent<Text> ().text = "Press Start to play";
				if (Input.GetButtonDown("J1_R2")) {
					explanationsPicture1 = true;
				}
			}	
		}


	}
}
