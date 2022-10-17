using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {

	private Vector3[] vertices;

	public Triangle(){
		vertices = new Vector3[3];
	}

	public Triangle(Vector3[] p_vertices){
		vertices = p_vertices;
	}

	public void setVertices(Vector3[] p_vertices){
		vertices = p_vertices;
	}

	public Vector3[] getVertices(){
		return vertices;
	}

	public float calculateArea(){

		float firstTerm = vertices [0].x * (vertices [1].z - vertices [2].z);
		float secondTerm = vertices [1].x * (vertices [2].z - vertices [0].z);
		float thirdTerm = vertices [2].x * (vertices [0].z - vertices [1].z);

		float area = (firstTerm + secondTerm + thirdTerm) / 2;
		area = Mathf.Abs (area);

		return area;
	}
}
