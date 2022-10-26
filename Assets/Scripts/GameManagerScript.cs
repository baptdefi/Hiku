using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;
using System.IO;

//using System.Collections.Generic;
using Delaunay;
using Delaunay.Geo;

using VoronoiNS;


public class GameManagerScript : MonoBehaviour {

	public GameObject pickUp;
	private static int bonusCount = 0;
	private int BONUSMAX = 5;
	public static bool pickUpPopWaitTimer = true;
	private float pickUpPopTimer = 0;
	private float pickUpPopcooldown = 200;

	public Material tileMat;
	public Material mat;
	public Material mat2;
	public Material mat3;
	public Material mat4;
	public Material mat5;
	public Material mat6;
	public Material mat7;
	public Material mat8;
	public Material mat9;
	public Material mat10;
	private int numSitesToGenerate = 200;
	private List<Vector2> m_points;
	private float m_mapWidth = 100;
	private float m_mapHeight = 50;
	private List<LineSegment> m_edges = null;
	private List<LineSegment> m_spanningTree;
	private List<LineSegment> m_delaunayTriangulation;
	private Delaunay.Voronoi v;

	public static List<GameObject> tilesList = new List<GameObject> ();

	private Material[] mats;

	private Vector3[] vertices;

	// Use this for initialization
	private void Start () {

		bonusCount = 0;
		
		List<Site> sites = new List<Site> ();

		List<uint> colors = new List<uint> ();

		m_points = new List<Vector2> ();

		for (int i = 0; i < numSitesToGenerate; i++) {
			colors.Add (0);
			m_points.Add (new Vector2 (
				UnityEngine.Random.Range (0, m_mapWidth),
				UnityEngine.Random.Range (0, m_mapHeight))
			);
		}
			
		v = new Delaunay.Voronoi (m_points, colors, new Rect (0, 0, m_mapWidth, m_mapHeight));

		m_edges = v.VoronoiDiagram ();

		m_spanningTree = v.SpanningTree (KruskalType.MINIMUM);
		m_delaunayTriangulation = v.DelaunayTriangulation ();

		//Debug.Log ("Created a Voronoi object. But for Unity, it's recommend you convert it to a VoronoiMap (data structure using Unity GameObjects and MonoBehaviours)");
		//Example:
		//VoronoiDiagram map = VoronoiDiagram.CreateDiagramFromVoronoiOutput( v, true );

		//Regions Generation
		v.Regions();

		sites = v.getSites();

		// map Site + Color
		Dictionary<Site, int> sitesWithColors = new Dictionary<Site, int> ();

		// color init to 0
		foreach (Site s in sites) {
			sitesWithColors.Add (s, 0);
		}

		int cpt = 0;
		foreach (Site s in sites) {

			//Debug.Log ("Site principal numero" + s.x);

			List<Site> neighbours = s.NeighborSites();
			//Debug.Log ("nombre de voisin " + neighbours.Count);

			List<int> cols = new List<int>();

			//Debug.Log ("VOISINS");
			//liste de couleur utilisées
			foreach (Site n in neighbours) {
				
				//Debug.Log ("Site voisin numero" + n.x);

				int color = sitesWithColors[n];

				if (cols.Count == 0 || !cols.Contains (color)) {
					//Debug.Log ("num de la couleur : " + color);
					cols.Add (color);
					//Debug.Log ("taille de la liste de couleur deja utilise : " + cols.Count);
				}


			}
			//Debug.Log ("SORTIEVOISINS");

			cols.Sort ();

			int newcolor = 0;
			foreach (int c in cols) {
				//Debug.Log ("couleur dans la liste interdite : " + c);
				if (newcolor == c) {
					//Debug.Log ("newcolor == c");
					newcolor++;
					//Debug.Log ("valeur de newcolor : "+ newcolor);
				}
				//else
					//Debug.Log ("newcolor != c");
			}
			sitesWithColors [s] = newcolor;
			//Debug.Log ("Couleur du site principal : " + sitesWithColors[s]);

			cpt++;
		}

		//Debug.Log ("nb de site : " + cpt);

		foreach (Site s in sites) {

			List<Vector2> regionList = s.getRegions ();
			int dim = regionList.Count + 1;

			vertices = new Vector3[dim];

			vertices [0] = new Vector3 (s.Coord.x, 0, s.Coord.y);
			//Debug.Log (vertices [0]);

			for (int i = 0; i < dim-1; i++) {
				vertices [i + 1] = new Vector3 (regionList[i].x, 0, regionList[i].y);
				//Debug.Log (vertices [i+1]);
			}

			//Debug.Log (vertices.Length);

			//Vector3[] vertices2 = new Vector3[]{new Vector3(2,0,2.5f), new Vector3(1.23f,0,3), new Vector3(3,0,3) , new Vector3(3,0,1), new Vector3(2,0,0), new Vector3(1,0,1)};

			Dictionary<int, Material> colorWithMaterial = new Dictionary<int, Material> ();
			mats = new Material[10]{ mat, mat2, mat3, mat4, mat5, mat6, mat7, mat8, mat9, mat10};

			for (int i = 0; i < mats.Length; i++) {
				colorWithMaterial.Add (i, mats [i]);
			}

			int colorindex = sitesWithColors [s];
			//Debug.Log ("Couleur du site avant creation : " + colorindex);

			Material chosenMat = colorWithMaterial [colorindex];

			chosenMat = tileMat;

			// création de la tuile
			tilesList.Add(TileGenerator.CreateTile (vertices, chosenMat));
		}

		OnDrawGizmos ();
	}

	void Update()
	{
		Vector3 pickUpPosition;

		// If there is not enough bonus
		if (bonusCount < BONUSMAX)
        {
			// If we wait for the timer
			if (pickUpPopWaitTimer)
            {
				if (pickUpPopTimer < pickUpPopcooldown)
                {
					pickUpPopTimer++;
				}
                else
                {
					pickUpPopTimer = 0;
					pickUpPopWaitTimer = false;
				}
			// If apparition is possible
			}
            else
            {
				bonusCount++;

				// Bonus creation
				pickUpPosition = new Vector3 (UnityEngine.Random.Range (0, m_mapWidth), 1.4f, UnityEngine.Random.Range (0, m_mapHeight));
				Instantiate (pickUp, pickUpPosition, Quaternion.identity);
			}
		}		
	}

	public static void removeTile(GameObject p_tileToRemove)
    {
		tilesList.Remove (p_tileToRemove);
	}

    public static void removeBonus()
    {
        bonusCount--;
    }



    public bool DrawBounds = false;
	public bool DrawAllEdges = false;
	public bool DrawDelaunayTriangulation = false;
	public bool DrawManualVoronoiPolygons = false; // NOTE: this is super-slow, use DrawVoronoiRegions instead
	public bool DrawManualVoronoiLinesToCenter = false;
	public bool DrawSpanningTree = false;
	public bool randomizeVoronoiColours = true;
	public bool CloseExternalVoronoPolys = true;
	public bool DrawVoronoiRegions = true;

	void OnDrawGizmos ()
	{
		if (v == null)
			return;

		Gizmos.color = Color.red;
		if (m_points != null) {
			for (int i = 0; i < m_points.Count; i++) {
				Gizmos.DrawSphere (m_points [i], 0.2f);
			}
		}

		if (DrawAllEdges) {
			if (m_edges != null) {
				Gizmos.color = Color.gray;
				for (int i = 0; i< m_edges.Count; i++) {
					Vector2 left = (Vector2)m_edges [i].p0;
					Vector2 right = (Vector2)m_edges [i].p1;
					Gizmos.DrawLine ((Vector3)left, (Vector3)right);
				}
			}
		}

		if (DrawDelaunayTriangulation) {
			Gizmos.color = Color.magenta;
			if (m_delaunayTriangulation != null) {
				for (int i = 0; i< m_delaunayTriangulation.Count; i++) {
					Vector2 left = (Vector2)m_delaunayTriangulation [i].p0;
					Vector2 right = (Vector2)m_delaunayTriangulation [i].p1;
					Gizmos.DrawLine ((Vector3)left, (Vector3)right);
				}
			}
		}

		if (DrawSpanningTree) {
			if (m_spanningTree != null) {
				Gizmos.color = Color.green;
				for (int i = 0; i< m_spanningTree.Count; i++) {
					LineSegment seg = m_spanningTree [i];				
					Vector2 left = (Vector2)seg.p0;
					Vector2 right = (Vector2)seg.p1;
					Gizmos.DrawLine ((Vector3)left, (Vector3)right);
				}
			}
		}

		/** This is the correct source of Voronoi polygons: the "Regions" data from the Voronoi object.
		Note that the list is points, not lines, and you usually have to manually CLOSE the final point to the first */
		if (DrawVoronoiRegions) {
			//Debug.Log ("Found " + v.Regions ().Count + " regions to draw");
			foreach (List<Vector2> region in v.Regions()) {
				if (randomizeVoronoiColours)
                    /** Note: the Edges display above (in Gray) shows unconnected edges,
		            but this section re-uses the actual semi-polygons created automatically by the Voronoi algorithm. To prove
		            this you can optionally turn on colourization of the edges, so that that shared edges will show with same colours
		            */
					Gizmos.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
				else
					Gizmos.color = Color.white;

				for (int i = 0; i+1<region.Count; i++) {
					Vector2 s = (Vector2)region [i];
					Vector2 e = (Vector2)region [i + 1];

					if (randomizeVoronoiColours) {
						/** To make them easier to see, shift the vectors SLIGHTLY towards the center point.
		
		                REMoVED: WE CANT DO THAT WHEN USING THE REGIONS SHORTCUT, REGIONS DELETE THEIR POINTS, sadly :(
		
		                This lets you see EXACTLY what poly / partial poly the algorithm is giving us "for free",
		                so that triangulating it will be easy in your own projects */
						//s += (siteCoord - s) * 0.05f;
						//e += (siteCoord - e) * 0.05f;
					}
					Gizmos.DrawLine (s, e);

				}

				if (CloseExternalVoronoPolys) {
					Gizmos.DrawLine ((Vector2)region [region.Count - 1], (Vector2)region [0]);
				}
			}
		}

		/** This is the INcorrect way of getting polygons out; but it has an advantage: the Voronoi class deletes
		the Site at center of a Region when giving us Regions (bug: I'd like to fix that and have it return a data
		structure that includes the Site!).
		
		In the meantime, here's how to manually generate the polys by re-using the Boundaries code from Voronoi,
		much easier than trying to manually generate from raw edges.
		
		But ideally: use DrawVoronoiRegions instead
		*/
		if (DrawManualVoronoiPolygons) {
			/** Note, the SiteCoords are identical to the raw Points array you passed-in when creating the Voronoi object,
		    I think. So ... you could safely re-use that here instead of fetching it from the Voronoi object (maybe; could be
		    some filteing happening? Dupes removed, etc?) */
			List<Vector2> ses = m_points;// v.SiteCoords ();
			foreach (Vector2 siteCoord in ses) {
				if (randomizeVoronoiColours)
                    /** Note: the Edges display above (in Gray) shows unconnected edges,
		            but this section re-uses the actual semi-polygons created automatically by the Voronoi algorithm. To prove
		            this you can optionally turn on colourization of the edges, so that that shared edges will show with same colours
		            */
					Gizmos.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
				else
					Gizmos.color = Color.white;

				/** NB: this is the reason we had to change VoronoiDemo class and save the Voronoi object: the boundaries
			    are the Voronoi polygons, the most precious thing from the algorithm. They are saved as Regions data structure
			    when you run the algorithm - but that data structure throws-away the Site/Point that generates each Region.
			    */
				List<LineSegment> outlineOfSite = v.VoronoiBoundaryForSite (siteCoord);
				List<Vector2> pointsOnPolygonOutline = null;
				if (CloseExternalVoronoPolys)
					pointsOnPolygonOutline = new List<Vector2> ();
				foreach (LineSegment seg in outlineOfSite) {
					Vector2 s = (Vector2)seg.p0;
					Vector2 e = (Vector2)seg.p1;

					if (randomizeVoronoiColours) {
						/** To make them easier to see, shift the vectors SLIGHTLY towards the center point.
		
		                This lets you see EXACTLY what poly / partial poly the algorithm is giving us "for free",
		                so that triangulating it will be easy in your own projects */
						s += (siteCoord - s) * 0.05f;
						e += (siteCoord - e) * 0.05f;
					}
					Gizmos.DrawLine (s, e);

					if (CloseExternalVoronoPolys) {
						pointsOnPolygonOutline.Add (s);
						pointsOnPolygonOutline.Add (e);
					}
				}

				if (CloseExternalVoronoPolys) {
					List<Vector2> unduplicatedPoints = new List<Vector2> ();

					//Debug.Log( "Closing outline; "+pointsOnPolygonOutline.Count+" points on outline, with "+outlineOfSite.Count+" lines between them");
					foreach (Vector2 point in pointsOnPolygonOutline) {
						Vector2 dupe;
						if ((dupe = ListContainsVectorCloseToVector (unduplicatedPoints, point)) != Vector2.zero) {
							//Debug.Log( " - point: "+point);
							unduplicatedPoints.Remove (dupe);
						} else {
							//Debug.Log( " + point: "+point);
							unduplicatedPoints.Add (point);
						}
					}

					if (unduplicatedPoints.Count == 2) {
						// two points that need connecting
						Gizmos.DrawLine (unduplicatedPoints [0], unduplicatedPoints [1]);
					} else if (unduplicatedPoints.Count > 1)
						Debug.LogError ("Should only have 0 or 2 unconnected points in a single polygon; had: " + unduplicatedPoints.Count);
				}

				if (DrawManualVoronoiLinesToCenter) {
					foreach (LineSegment seg in outlineOfSite) {
						Gizmos.color = Color.gray;
						Gizmos.DrawLine ((Vector2)seg.p0, siteCoord);
					}
				}
			}
		}

		if (DrawBounds) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (new Vector2 (0, 0), new Vector2 (0, m_mapHeight));
			Gizmos.DrawLine (new Vector2 (0, 0), new Vector2 (m_mapWidth, 0));
			Gizmos.DrawLine (new Vector2 (m_mapWidth, 0), new Vector2 (m_mapWidth, m_mapHeight));
			Gizmos.DrawLine (new Vector2 (0, m_mapHeight), new Vector2 (m_mapWidth, m_mapHeight));
		}
	}

	/** Returns the dupe item in list so you can remove it; Voronoi library is abusing Vector2 class and returning non-equivalent equivalent objects */
	private Vector2 ListContainsVectorCloseToVector (List<Vector2> l, Vector2 p)
	{
		foreach (Vector2 v in l) {
			if (DoesPointEqualPointEpsilon (v, p))
				return v;
		}
		return Vector2.zero;
	}

	private bool DoesPointEqualPointEpsilon (Vector2 p1, Vector2 p2, float eps = 0.0001f)
	{
		if (Mathf.Abs (p1.x - p2.x) > eps)
			return false;
		if (Mathf.Abs (p1.y - p2.y) > eps)
			return false;
		return true;
	}
}