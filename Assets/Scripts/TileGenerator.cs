using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour {
	
	public static GameObject CreateTile(Vector3[] vertices, Material p_material)
	{
		GameObject go = new GameObject("Tile");
		MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		Tuile t = go.AddComponent (typeof(Tuile)) as Tuile;

		go.tag = "Tile";

		Mesh m = new Mesh();
		m.Clear ();

		// Nombre de sommets total pour la triangulation
		int dim = (vertices.Length-1)*3;

		int trianglesCount = dim/3;

		// Tableau des index des sommets des triangles
		int[] triangles = new int[dim];
		int index = 0;

		// Tableau de triangles pour le calcul d'aire
		Triangle[] triangleTab = new Triangle[trianglesCount];

		// Constitution de la liste des triangles d'apres l'ordre des sommets
		// triangles = [0,2,1 - 0,3,2 - 0,4,3 - 0,1,4]
		for (int i = 1; i<=trianglesCount; i++){

			// Triangle pour le calcul d'aire
			Vector3[] triangleVertices = new Vector3[3];
			// Premier sommet
			triangleVertices [0] = vertices [0];

			triangles[index] = 0;
			index++;

			// Deuxième sommet
			// si l'on atteint la fin du triangle, on repasse à 1
			if (i + 1 < vertices.Length) {
				triangleVertices [1] = vertices [i+1];
				triangles [index] = i + 1;

			} else {
				triangleVertices [1] = vertices [1];
				triangles [index] = 1;

			}
			index++;

			// Troisième sommet
			triangleVertices [2] = vertices [i];

			triangles[index] = i;
			index++;

			// La boucle commence à 1, le tableau à 0
			triangleTab [i - 1] = new Triangle(triangleVertices);
		}

		// Le tableau de triangles est passé au script Tuile
		go.GetComponent<Tuile> ().setTriangles(triangleTab);

		go.GetComponent<Tuile> ().initResistance ();

		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (vertices [i].x, vertices [i].z);
		}

		m.vertices = vertices;
		m.triangles = triangles;
		m.uv = uvs;

		mf.mesh = m;

		/*EdgeCollider2D mc = go.AddComponent (typeof(EdgeCollider2D)) as EdgeCollider2D;
		Vector2[] coli = new Vector2[vertices.Length];
		int k = vertices.Length;
		for (int j=0; j < k-1; j++) {
			coli[j] = new Vector2 (vertices [j+1].x, vertices [j+1].z);
			coli[k-1]= new Vector2 (vertices [1].x, vertices [1].z);
		}

		//coli[k] = new Vector2 (vertices [1].x, vertices [1].z);
		mc.points = coli;*/
		MeshCollider mc = go.AddComponent (typeof(MeshCollider)) as MeshCollider;
		mc.sharedMesh = m;
		m.RecalculateBounds ();
		m.RecalculateNormals ();


		LineRenderer l =go.AddComponent(typeof(LineRenderer)) as LineRenderer;
		Vector3[] coli = new Vector3[vertices.Length];
		int k = vertices.Length;
		for (int j=0; j < k-1; j++) {
			coli[j] = new Vector3 (vertices [j+1].x,0, vertices [j+1].z);
			coli[k-1]= new Vector3 (vertices [1].x,0, vertices [1].z);
		}
		l.SetVertexCount (vertices.Length);
		l.SetPositions (coli);
		l.SetWidth (0.3f, 0.3f);
		l.material = p_material ;
		l.receiveShadows = false;
		mr.material = p_material;

		mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		l.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

		return go;
	}
}
