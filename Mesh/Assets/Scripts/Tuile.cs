using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuile : MonoBehaviour {

	private Triangle[] triangles;
	private float resistance;
	private bool hasPickUp = false;

	private float destructionValue = 2.5f;

	// step to color change according to resistance
	private float colorStep;
    // color lerp coef
	private float colorLerp = 0.0f;
	// custom blue color
	private Color blueColor = new Color(0.0f, 0.1540003f, 1.0f, 1.0f);

	private MeshRenderer mr;

	void Start () {
		mr = GetComponent<MeshRenderer> ();
	}

	// init resistance regarding the tile area
	public void initResistance(){

		float area = 0;

		for (int i = 0; i < triangles.Length; i++) {
			area += triangles [i].calculateArea ();
		}

		// Add 5% random
		float randomModifier = area * 0.05f;

		if (Random.Range (0, 9) < 5) {
			area = area + randomModifier;
		} else {
			area = area - randomModifier;
		}
			
		resistance = area*10;

        // step according to resistance
		colorStep = destructionValue / resistance;
	}

	public void weakenTile (){

		resistance -= destructionValue;

		// coef update
		colorLerp += colorStep;

		// new color
		Color newColor = Color.Lerp (Color.white, blueColor, colorLerp); 

		mr.material.color = newColor;

		if (resistance <= 1.0f) {
			mr.material.color = Color.black;
		}
		if (resistance <= 0.1f) {
			destroyTile ();
		}
	}

	public void destroyTile(){
		GameManagerScript.removeTile (this.gameObject);
		Destroy (gameObject);
	}

    public float getResistance(){
		return resistance;
    }

	public void setResistance(float p_resistance){
		resistance = p_resistance;
	}

	public void setTriangles(Triangle[] p_triangles){
		triangles = p_triangles;
	}

	public bool getHasPickUp(){
		return hasPickUp;
	}

	public void setHasPickUp(bool p_bool){
		hasPickUp = p_bool;
	}

}


