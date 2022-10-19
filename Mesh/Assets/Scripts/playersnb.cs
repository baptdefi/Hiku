using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playersnb : MonoBehaviour {

	public GameObject j3;
	public GameObject t3;
	public GameObject tablescore;
	public GameObject ecranv;

	int score1;
	int score2;
	int score3;
	int score4;
	int choix=-1;

	public GameObject t4;
	public GameObject j4;
	public GameObject rank3;
	public GameObject rank4;
	public GameObject tetev;
	public GameObject textv;

	public Sprite eskimo;
	public Sprite pingouin;
	public Sprite ours;
	public Sprite phoque;
	public Sprite vide;

	Image img;
	Text t;

	private int maxScore = 15;
	private int nbJoueurGagnant = 0;


	// Use this for initialization
	void Start () {

		score1 = (PlayerPrefs.GetInt ("Score1"));
		score2 = (PlayerPrefs.GetInt ("Score2"));

		if (score1 >= maxScore) {
			nbJoueurGagnant++;
		}
		if (score2 >= maxScore) {
			nbJoueurGagnant++;
		}

		// 3 players
		if (PlayerPrefs.GetInt ("nbplayer") >= 3) {
			j3.SetActive (true);
			t3.SetActive (true);
			rank3.SetActive (true);

			score3 = (PlayerPrefs.GetInt ("Score3"));
			if (score3 >= maxScore) {
				nbJoueurGagnant++;
			}
		}

		// 4 players
		if (PlayerPrefs.GetInt ("nbplayer") == 4) {
			j4.SetActive (true);
			t4.SetActive (true);
			rank4.SetActive (true);

			score4 = (PlayerPrefs.GetInt ("Score4"));
			if (score4 >= maxScore) {
				nbJoueurGagnant++;
			}
		}
		
		// At least one winner
		if (nbJoueurGagnant > 0) {
			tablescore.SetActive (false);
			ecranv.SetActive (true);
			img = tetev.GetComponent<Image> ();
			t = textv.GetComponent<Text> ();

			// Draw
			if (nbJoueurGagnant > 1) {
				img.sprite = vide;
				t.text = "Egalite";
				t.color = new Color (1f, 0.5f, 0f);
			} else {
				if (score1 >= maxScore) {
					choix = PlayerPrefs.GetInt ("PreferedModel1");
				} else if (score2 >= maxScore) {
					choix = PlayerPrefs.GetInt ("PreferedModel2");
				}
				if (PlayerPrefs.GetInt ("nbplayer") >= 3) {
					if (score3 >= maxScore) {
						choix = PlayerPrefs.GetInt ("PreferedModel3");
					}
				}
				if (PlayerPrefs.GetInt ("nbplayer") == 4) {
					if (score4 >= maxScore) {
						choix = PlayerPrefs.GetInt ("PreferedModel4");
					}
				}

				if (choix == 0)
					img.sprite = eskimo;
				else if (choix == 1)
					img.sprite = ours;
				else if (choix == 2)
					img.sprite = phoque;
				else if (choix == 3)
					img.sprite = pingouin;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

        // Start button
        if (Input.GetButtonDown ("J1_R2") || Input.GetButtonDown("J1_Keyboard_Enter")) {
			// Game over
			if (nbJoueurGagnant >= 1) {
				// Scores reset
				Score1.score = 0;
				Score2.score = 0;
				Score3.score = 0;
				Score4.score = 0;

				SceneManager.LoadScene ("Load");
			}
			// Game running
			else {
				SceneManager.LoadScene ("mainscene");
			}
		}
	}
}
