using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meshPref : MonoBehaviour {
	
	/*public Mesh esquimoModel;
	public Mesh oursModel;
	public Mesh penguinModel;
	public Mesh sealModel;*/
	
	private int preferedModelIndex;
	public Sprite Eskim;
	public Sprite Ours;
	public Sprite Pingouin;
	public Sprite Phoque;
	public GameObject Face;
	Image imface;
	// Use this for initialization
	void Start () {
		imface = Face.GetComponent<Image> ();	
		// P1
		if (transform.parent.gameObject.name.Equals("Player1")) {
			preferedModelIndex = PlayerPrefs.GetInt ("PreferedModel1");
		}
		// P2
		else if (transform.parent.gameObject.name.Equals("Player2")) {
			preferedModelIndex = PlayerPrefs.GetInt ("PreferedModel2");
		}
		// P3
		else if (transform.parent.gameObject.name.Equals("Player3")) {
			preferedModelIndex = PlayerPrefs.GetInt ("PreferedModel3");
		}
		// P4
		else if (transform.parent.gameObject.name.Equals("Player4")) {
			preferedModelIndex = PlayerPrefs.GetInt ("PreferedModel4");
		}
		
		//swapmesh = selc.GetComponent<MeshFilter> ().mesh;
			
		/*if(PlayerPrefs.GetInt("PreferedModel1")==0)
			gameObject.GetComponent<MeshFilter> ().mesh= esquimoModel;
		if(PlayerPrefs.GetInt("PreferedModel1")==1)
			gameObject.GetComponent<MeshFilter> ().mesh= oursModel;
		if(PlayerPrefs.GetInt("PreferedModel1")==2)
			gameObject.GetComponent<MeshFilter> ().mesh= penguinModel;
		if(PlayerPrefs.GetInt("PreferedModel1")==3)
			gameObject.GetComponent<MeshFilter> ().mesh= sealModel;*/

		//Debug.Log (preferedModelIndex);

		if (preferedModelIndex == -1) {
			
			// Il n'existe pas de joueur 3 ou 4 donc le gameObject est désactivé
			this.transform.parent.gameObject.SetActive (false);

		} else {
			
			// Init
			gameObject.transform.GetChild (0).gameObject.SetActive (false);
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
			gameObject.transform.GetChild (2).gameObject.SetActive (false);
			gameObject.transform.GetChild (3).gameObject.SetActive (false);

			// Activation du bon objet
			if (preferedModelIndex == 0) {
				gameObject.transform.GetChild (0).gameObject.SetActive (true);
				imface.sprite = Eskim;
			}
			if (preferedModelIndex == 1) {
				gameObject.transform.GetChild (1).gameObject.SetActive (true);
				imface.sprite = Ours;
			}
			if (preferedModelIndex == 2) {
				gameObject.transform.GetChild (2).gameObject.SetActive (true);
				imface.sprite = Phoque;
			}
			if (preferedModelIndex == 3) {
				gameObject.transform.GetChild (3).gameObject.SetActive (true);
				imface.sprite = Pingouin;
			}

		}
				
		//Debug.Log (gameObject.transform.GetChild (0).gameObject.activeInHierarchy);
		//Debug.Log(gameObject.transform.GetChild (1).gameObject.activeInHierarchy);
		//Debug.Log(gameObject.transform.GetChild (2).gameObject.activeInHierarchy);
		//Debug.Log(gameObject.transform.GetChild (3).gameObject.activeInHierarchy);
			

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
