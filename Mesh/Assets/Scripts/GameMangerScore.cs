using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangerScore : MonoBehaviour {

	bool paused1 = false;
	bool paused2 = false;
	bool paused3=false;
	bool paused4=false;

	int tombe1=0;
	int tombe2=0;
	int tombe3=0;

	public GameObject j1;
	public GameObject j2;
	public GameObject j3;
	public GameObject j4;

	int nbplayer=2;

	void Start(){

		Time.timeScale = 1f;

		if (PlayerPrefs.GetInt ("PreferedModel4") > -1 && PlayerPrefs.GetInt ("PreferedModel3") > -1) {
			nbplayer = 4;
		} 
		else if (PlayerPrefs.GetInt ("PreferedModel4") == -1 && PlayerPrefs.GetInt ("PreferedModel3") > -1) 
		{
			nbplayer=3;
		}

		PlayerPrefs.SetInt ("nbplayer", nbplayer);
	}

	void Update()
	{
		if (nbplayer == 2) {
			if (j1.transform.position.y < -5 && paused1 == false) {
				Time.timeScale = 0f;
				paused1 = true;
				Score1.score += 2;
				Score2.score += 5;

				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");
			} else if (j2.transform.position.y < -5 && paused2 == false) {	
				Time.timeScale = 0f;
				paused2 = true;
				Score1.score += 5;
				Score2.score += 2;
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");
			}
		
		}

		if (nbplayer == 3) {
			if (j1.transform.position.y < -5 && paused1 == false && paused2==false && paused3==false) {

				paused1 = true;
				tombe1 = 1;

				 
			}
			if (j1.transform.position.y < -5 && paused1 == false &&(( paused2==true && paused3==false)||( paused2==false && paused3==true) )) {

				paused1 = true;
				tombe2 = 1 ;


			}
			if (j2.transform.position.y < -5 && paused2 == false && paused1 == false && paused3==false) {	
				
				paused2 = true;
				tombe1 = 2;

			}
			if (j2.transform.position.y < -5 && paused2 == false &&(( paused1==true && paused3==false)||( paused1==false && paused3==true) )) {	

				paused2 = true;
				tombe2 = 2;

			}
			if (j3.transform.position.y < -5 && paused3 == false && paused1==false && paused2==false) {	

				paused3 = true;
				tombe1 = 3;

			}
			if (j3.transform.position.y < -5 && paused3 == false  &&(( paused1==true && paused2==false)||( paused1==false && paused2==true) )) {	

				paused3 = true;
				tombe2 = 3;

			}
			if( paused1==true && paused2==true && paused3==false)
				{
				Time.timeScale = 0f;
				Score3.score += 5;
				if (tombe1 == 1) {
					Score1.score += 1;
					Score2.score += 2;
				} else {
					Score2.score += 1;
					Score1.score += 2;
				}
				
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");

				}
			if( paused1==true && paused3==true && paused2==false)
			{
				Time.timeScale = 0f;
				Score2.score += 5;
				if (tombe1 == 1) {
					Score1.score += 1;
					Score3.score += 2;
				} else {
					Score3.score += 1;
					Score1.score += 2;
				}
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");

			}
			if( paused2==true && paused3==true && paused1==false)
			{
				Time.timeScale = 0f;
				Score1.score += 5;
				if (tombe1 == 2) {
					Score2.score += 1;
					Score3.score += 2;
				} else {
					Score3.score += 1;
					Score2.score += 2;
				}

				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");

			}

		}


		if (nbplayer == 4) {
			if (j1.transform.position.y < -5 && paused1 == false && paused2==false && paused3==false && paused4==false) {

				paused1 = true;
				tombe1 = 1;


			}
			if (j1.transform.position.y < -5 && paused1 == false &&( (paused2==true && paused3==false && paused4==false) || (paused2==false && paused3==true && paused4==false) || (paused2==false && paused3==false && paused4==true) )) {

				paused1 = true;
				tombe2 = 1;

			}
			if (j1.transform.position.y < -5 && paused1 == false &&( (paused2==true && paused3==true && paused4==false) || (paused2==false && paused3==true && paused4==true) || (paused2==true && paused3==false && paused4==true) )) {

				paused1 = true;
				tombe3 = 1;

			}
			if (j2.transform.position.y < -5 && paused2 == false && paused1==false && paused3==false && paused4==false) {

				paused2 = true;
				tombe1 = 2;

			}
			if (j2.transform.position.y < -5 && paused2 == false &&( (paused1==true && paused3==false && paused4==false) || (paused1==false && paused3==true && paused4==false) || (paused1 ==false && paused3 ==false && paused4 ==true) )) {

				paused2 = true;
				tombe2 = 2 ;

			}
			if (j2.transform.position.y < -5 && paused2 == false &&( (paused1==true && paused3==true && paused4==false) || (paused1==false && paused3==true && paused4==true) || (paused1==true && paused3==false && paused4==true) )) {

				paused2 = true;
				tombe3 = 2;

			}
			if (j3.transform.position.y < -5 && paused3 == false && paused1==false && paused2==false && paused4==false) {

				paused3 = true;
				tombe1 = 3;

			}
			if (j3.transform.position.y < -5 && paused3 == false &&( (paused1==true && paused2==false && paused4==false) || (paused1==false && paused2==true && paused4==false) || (paused1 ==false && paused2 ==false && paused4 ==true) )) {

				paused3 = true;
				tombe2 = 3 ;

			}
			if (j3.transform.position.y < -5 && paused3 == false &&( (paused1==true && paused2==true && paused4==false) || (paused1==false && paused2==true && paused4==true) || (paused1==true && paused2==false && paused4==true) )) {

				paused3 = true;
				tombe3 = 3;

			}
			if (j4.transform.position.y < -5 && paused4 == false && paused1==false && paused2==false && paused3==false) {

				paused4 = true;
				tombe1 = 4;

			}
			if (j4.transform.position.y < -5 && paused4 == false &&( (paused1==true && paused2==false && paused3==false) || (paused1==false && paused2==true && paused3==false) || (paused1 ==false && paused2 ==false && paused3 ==true) )) {

				paused4 = true;
				tombe2 = 4 ;

			}
			if (j4.transform.position.y < -5 && paused4 == false &&( (paused1==true && paused2==true && paused3==false) || (paused1==false && paused2==true && paused3==true) || (paused1==true && paused2==false && paused3==true) )) {

				paused4 = true;
				tombe3 = 4;

			}
			if( paused1==true && paused2==true && paused4==true && paused3==false)
			{
				Time.timeScale = 0f;
				Score3.score += 5;
				if (tombe1 == 1) {
					Score1.score += 0;
					if (tombe2 == 2) {
						Score2.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score2.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 2) {
					Score2.score += 0;
					if (tombe2 == 1) {
						Score1.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score1.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 4) {
					Score4.score += 0;
					if (tombe2 == 1) {
						Score1.score += 1;
						Score2.score += 2;
					}
					if (tombe2 == 2) {
						Score1.score += 2;
						Score2.score += 1;
					}
				} 
				PlayerPrefs.SetInt ("Score4", Score4.score);
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");
	
			}
			if( paused1==true && paused3==true && paused4==true && paused2==false)
			{
				Time.timeScale = 0f;
				Score2.score += 5;
				if (tombe1 == 1) {
					Score1.score += 0;
					if (tombe2 == 3) {
						Score3.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score3.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 3) {
					Score3.score += 0;
					if (tombe2 == 1) {
						Score1.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score1.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 4) {
					Score4.score += 0;
					if (tombe2 == 1) {
						Score1.score += 1;
						Score3.score += 2;
					}
					if (tombe2 == 3) {
						Score1.score += 2;
						Score3.score += 1;
					}
				} 
 
				PlayerPrefs.SetInt ("Score4", Score4.score);
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");

			}
			if( paused2==true && paused3==true && paused4==true && paused1==false)
			{
				Time.timeScale = 0f;
				Score1.score += 5;
				if (tombe1 == 2) {
					Score2.score += 0;
					if (tombe2 == 3) {
						Score3.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score3.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 3) {
					Score3.score += 0;
					if (tombe2 == 2) {
						Score2.score += 1;
						Score4.score += 2;
					}
					if (tombe2 == 4) {
						Score2.score += 2;
						Score4.score += 1;
					}
				} 
				if (tombe1 == 4) {
					Score4.score += 0;
					if (tombe2 == 2) {
						Score2.score += 1;
						Score3.score += 2;
					}
					if (tombe2 == 3) {
						Score2.score += 2;
						Score3.score += 1;
					}
				} 
				PlayerPrefs.SetInt ("Score4", Score4.score);
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);
				SceneManager.LoadScene ("Score");

			}
			if( paused2==true && paused3==true && paused1==true && paused4==false)
			{
				Time.timeScale = 0f;
				Score4.score += 5;

				if (tombe1 == 2) {
					Score2.score += 0;
					if (tombe2 == 3) {
						Score3.score += 1;
						Score1.score += 2;
					}
					if (tombe2 == 1) {
						Score3.score += 2;
						Score1.score += 1;
					}
				} 
				if (tombe1 == 3) {
					Score3.score += 0;
					if (tombe2 == 2) {
						Score2.score += 1;
						Score1.score += 2;
					}
					if (tombe2 == 1) {
						Score2.score += 2;
						Score1.score += 1;
					}
				} 
				if (tombe1 == 1) {
					Score1.score += 0;
					if (tombe2 == 2) {
						Score2.score += 1;
						Score3.score += 2;
					}
					if (tombe2 == 3) {
						Score2.score += 2;
						Score3.score += 1;
					}
				} 


				PlayerPrefs.SetInt ("Score4", Score4.score);
				PlayerPrefs.SetInt ("Score3", Score3.score);
				PlayerPrefs.SetInt ("Score2", Score2.score);
				PlayerPrefs.SetInt ("Score1", Score1.score);

				SceneManager.LoadScene ("Score");
			}

		}
			
	}
}