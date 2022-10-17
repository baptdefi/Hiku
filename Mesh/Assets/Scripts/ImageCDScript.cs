using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCDScript : MonoBehaviour {

	private GameObject parentCollider;
	private GameObject childImage;

	// Use this for initialization
	void Start () {
		parentCollider = this.transform.parent.transform.parent.gameObject;
		childImage = this.transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (parentCollider.GetComponent<PlayerController> ().getDashState ().ToString ().Equals ("Cooldown")) {


			if (!childImage.activeSelf) {
				childImage.SetActive (true);
			}
			else {
				float timer = parentCollider.GetComponent<PlayerController> ().getDashTimer ();
				float cooldown = parentCollider.GetComponent<PlayerController> ().getDashCooldown ();
				float fillAmount = 0.02f + 1 - timer / cooldown;

				childImage.GetComponent<UnityEngine.UI.Image> ().fillAmount = fillAmount;
			}
		}
		else {

			if (childImage.activeSelf) {
				childImage.SetActive (false);
				childImage.GetComponent<UnityEngine.UI.Image> ().fillAmount = 0.0f;
			}
		}
	}
}
