using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TGMap : MonoBehaviour {
	public int size_x = 30;
	public int size_z = 30;
	public float tileSize = 5.0f;

	public Texture2D terrainTiles;
	public int tileResolution;
	public TDMap map;
	Texture2D texture;
	Color[][] tiles;

	// Use this for initialization
	void Start () {
		BuildMesh();
	}

	Color[][] ChopUpTiles(){
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		Color[][] tiles = new Color[numTilesPerRow*numRows][];

		for(int y=0; y < numRows; y++) {
			for(int x=0; x < numTilesPerRow; x++) {
				tiles[y*numTilesPerRow + x] = terrainTiles.GetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution);
			}
		}
		return tiles;
	}

	void BuildTexture() {
		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;

		map = GetComponent<TDMap>();
		map.BuildMap(size_x, size_z);
		// TDMap map = new TDMap(size_x, size_z);
		texture = new Texture2D(texWidth, texHeight);
	 	tiles = ChopUpTiles();

		for(int y=0; y < size_z; y++) {
			for(int x=0; x < size_x; x++) {
				Color[] p = tiles[map.GetTile(x,y)];
				texture.SetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
			}
		}

		texture.filterMode = FilterMode.Point; // Stops the colors from blending, can be used to set individual pixels
		texture.Apply();

		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;
		Debug.Log("Done Texture");
	}

	public void BuildMesh() {
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;

		// Generate the mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[numTris * 3];

		int x, z;
		for(z=0; z < vsize_z; z++) {
			for(x=0; x < vsize_x; x++) {
				vertices[z * vsize_x + x] = new Vector3(x*tileSize, 0, z*tileSize);
				normals[z * vsize_x + x] = Vector3.up;
				uv[z * vsize_x + x] = new Vector2((float)x / size_x, (float)z / size_z);
			}
		}
		Debug.Log("Done Verts");

    // This data traversal is very similar to the image manipulation from 347
    for(z=0; z < size_z; z++) {
			for(x=0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

		    triangles[triOffset + 3] = z * vsize_x + x;
		    triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
		    triangles[triOffset + 5] = z * vsize_x + x + 1;
			}
		}
		Debug.Log("Done Triangles");

    // Create a new mesh
    Mesh mesh = new Mesh();
    // Populate mesh with the data
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.normals = normals;
    mesh.uv = uv;
    // Apply the mesh to the filter/renderer/sollider
    MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();

		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
		Debug.Log("Done Mesh");
		BuildTexture();
  }

	public void DsplRange(Vector3 centerPoint, int range, int color) {
		Vector3 originPoint = (centerPoint / 5f) - new Vector3(range/5 + 1, 0, range/5 + 1);
		SetTexture(range*2/5 + 1, range*2/5 + 1, color, originPoint);
	}


	void SetTexture(int xRange, int yRange, int type, Vector3 originPoint) {
		int xStart = (int)originPoint.x;
		int yStart = (int)originPoint.z;
		int xEnd =  xRange+(int)originPoint.x;
		int yEnd = yRange+(int)originPoint.z;

		// Check bounds
		if(xStart < 0)
			xStart = 0;
		if(yStart < 0)
			yStart = 0;
		if(xEnd > size_x)
			xEnd = size_x;
		if(yEnd > size_z)
			yEnd = size_z;

		for(int y = yStart; y < yEnd; y++){
			for(int x = xStart; x < xEnd; x++) {
				Color[] p = tiles[type];
				texture.SetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
			}
		}
		texture.Apply();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;
	}

	public void ResetTexture(int xRange, int yRange, Vector3 originPoint) {
		int xStart = (int)originPoint.x;
		int yStart = (int)originPoint.z;
		int xEnd =  xRange+(int)originPoint.x;
		int yEnd = yRange+(int)originPoint.z;

		// Check bounds
		if(xStart < 0)
			xStart = 0;
		if(yStart < 0)
			yStart = 0;
		if(xEnd > size_x)
			xEnd = size_x;
		if(yEnd > size_z)
			yEnd = size_z;

		for(int y = yStart; y < yEnd; y++){
			for(int x = xStart; x < xEnd; x++) {
				Color[] p = tiles[map.GetTile(x, y)];
				texture.SetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
			}
		}
		texture.Apply();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;
	}


	// TODO: Look into which is better map.GetComponent<TDMap>() or tileSetter.OccupySpace() for GameManger
	public void OccupySpace(int x, int y, int occupant)	{
		map.SetOccupant(x, y, occupant);
	}

	public int GetOccupant(int x, int y) {
		return map.GetOccupant(x, y);
	}
}
