using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

	MeshFilter mf;
	MeshCollider mc;

	Mesh m;

	Vector3[] vertices;
	int[] triangles;
	Vector2[] uvs;

	void Awake()
    {
		mf = GetComponent<MeshFilter>();
		mc = GetComponent<MeshCollider>();
	}

	// Use this for initialization
	void Start ()
    {
		MakeMeshData ();
		CreateMesh ();
	}
	
	void MakeMeshData()
    {
		//Create an array of vertices
		vertices = new Vector3[]{new Vector3(2,1,2), new Vector3(1,1,3), new Vector3(3,1,3) , new Vector3(3,1,1), new Vector3(2,1,0), new Vector3(1,1,1), new Vector3(1,0,1), new Vector3(2,0,0),new Vector3(3,0,1),new Vector3(1,0,3), new Vector3(3,0,3)};
		//Create an array of integers
		//triangles = new int[]{0,1,2,0,2,3,0,3,4,0,4,5,0,5,1,1,2,10,2,10,9,2,3,9,3,8,9,3,4,8,4,7,8,4,5,6,5,6,7,5,1,10,1,10,6};
		triangles = new int[]{0,1,2,0,2,3,0,3,4,0,4,5,0,5,1,1,10,2,1,9,10};

		uvs = new Vector2[vertices.Length];

		for (int i = 0; i < uvs.Length; i++)
        {
			uvs [i] = new Vector2 (vertices [i].x-1, vertices [i].z-1);
		}
    }

	void CreateMesh()
    {
		m = new Mesh ();
		m.Clear ();
		m.vertices = vertices;
		m.triangles = triangles;
		m.uv = uvs;
		mf.mesh = m;

		mc.sharedMesh = m;

		m.RecalculateBounds ();
		m.RecalculateNormals ();
	}
}