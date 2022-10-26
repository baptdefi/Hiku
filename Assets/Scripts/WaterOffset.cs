using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOffset : MonoBehaviour {

	public float speedX = 0.1f;
	public float speedY = 0.1f;
	private float curX;
	private float curY;
	private MeshRenderer r;

	void Awake(){
		r = GetComponent<MeshRenderer> ();
		curX = r.material.mainTextureOffset.x;
		curY = r.material.mainTextureOffset.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curX += Time.deltaTime * speedX;
		curY += Time.deltaTime * speedY;

		r.material.SetTextureOffset ("_MainTex", new Vector2 (curX, curY));
	}
}
