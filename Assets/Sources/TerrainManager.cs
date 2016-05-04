using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
	float[,,] element;
	int mapX, mapY;
	TerrainData terrainData;
	Vector3 terrainPosition;
	public Terrain myTerrain;
	//float[,,] map;
	//private Vector3 lastPos;

	private static TerrainManager _instance;
	public static TerrainManager Instance {
		get {
			return _instance;
		}
	}

	void Awake() {
		//map = new float[myTerrain.terrainData.alphamapWidth, myTerrain.terrainData.alphamapHeight, myTerrain.terrainData.alphamapLayers];

		element = new float[1, 1, myTerrain.terrainData.alphamapLayers];
		terrainData = myTerrain.terrainData;
		terrainPosition = myTerrain.transform.position;

		//lastPos = transform.position;

		_instance = this;
	}

	void Update() {
	}

	void UpdateMapOnTheTarget() {
		//just update if you move
		/*
		if (Vector3.Distance(transform.position, lastPos) > 1) {
			//print("paint");
			//convert world coords to terrain coords
			mapX = (int)(((transform.position.x - terrainPosition.x) / terrainData.size.x) * terrainData.alphamapWidth);
			mapY = (int)(((transform.position.z + 0.5f - terrainPosition.z) / terrainData.size.z) * terrainData.alphamapHeight);

			//map[mapY, mapX, 0] = element[0, 0, 0] = 0;
			map[mapY, mapX, 1] = element[0, 0, 4] = 1;

			myTerrain.terrainData.SetAlphamaps(mapX, mapY, element);

			lastPos = transform.position;
		}
		*/
	}

	public void Terrain(Vector3 position, int index) {

		mapX = (int)(((position.x - terrainPosition.x) / terrainData.size.x) * terrainData.alphamapWidth);
		mapY = (int)(((position.z - terrainPosition.z) / terrainData.size.z) * terrainData.alphamapHeight);

		//map[mapY, mapX, 0] = 
		//map[mapY, mapX, 1] = 

		for (int i = 0; i < myTerrain.terrainData.alphamapLayers; i++) element[0, 0, i] = 0f;

		element[0, 0, index] = 1f;

		myTerrain.terrainData.SetAlphamaps(mapX, mapY, element);
	}

	public static void UpdateTerrain(Vector3 position, int index) {
		if (!_instance) return;
		_instance.Terrain(position, index);
	}

}
