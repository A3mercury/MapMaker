using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {

	public Transform tilePrefab;
	public Vector2 mapSize;
				
	void Start()
	{
		if (mapSize.x % 2 == 0) {
			mapSize.x++;
		}
		if (mapSize.y % 2 == 0) {
			mapSize.y++;
		}
	
		DrawMap();
	}
	
	// Method used to display the map to the screen
	public void DrawMap()
	{
		// Parent name
		string holderName = "MapTiles";
		
		// If the parent has been found, destroy it
		if (transform.FindChild (holderName)) {
			DestroyImmediate(transform.FindChild (holderName).gameObject);
		}
		
		// Create a new parent
		Transform mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = transform;
	
		// Draw the map with the mapSize
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
			
				// Create a new tile and put it into the correct position
				Vector3 tilePostition = new Vector3(-mapSize.x/2 + 0.5f + x, 0, -mapSize.y/2 + 0.5f + y);
				Transform newTile = Instantiate(tilePrefab, tilePostition, Quaternion.Euler(Vector3.right * 90)) as Transform;
				newTile.name = "Tile("+tilePostition.x+","+tilePostition.z+")";
				newTile.parent = mapHolder;
			}
		}
	}
}
