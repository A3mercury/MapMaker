using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockController : MonoBehaviour {

	public static bool doUpdate = true;
	public Dictionary<string, GameObject> blocks;
	public GameObject useBlock;
	public GameObject blockPrefab;
	public MapMaker map;
	
	private Transform blockHolder;
	
	void Start() 
	{
		// Start by instantiating the starting block
		//block = InstantiateBlock();
		
		// Create a new array with the size of whatever the number of possible blocks on the map can be
		blocks = new Dictionary<string, GameObject>();
		//blocks = new GameObject[(int)map.mapSize.x * (int)map.mapSize.y];
		
		// Set the parent for where the blocks are to go
		string holderName = "WorldBlocks";
		blockHolder = new GameObject(holderName).transform;
		blockHolder.parent = transform;
	}
	
	void Update() 
	{
		// Return if space is pressed
		if ( ! doUpdate) { 
			// Move the block out of view temporarily
			Destroy(useBlock);
			return;
		}
		
		// Build a new block
		if (Input.GetKeyDown(KeyCode.B)) {
			useBlock = CreateBlock();
		}
		
		// Place a block
		if (Input.GetKey(KeyCode.Mouse0)) {
			PlaceBlock(useBlock);
		}
	
		// Move the block around the map
		MoveBlock(useBlock);
		
		
		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (string B in blocks.Keys) {
				Debug.Log(blocks[B]);
			}
		}
	}
	
	public GameObject CreateBlock()
	{
		GameObject newBlock = InstantiateBlock(0, 0);
		newBlock.name = "Block(" + newBlock.transform.position.x + "," + newBlock.transform.position.z + ")";
		newBlock.transform.parent = blockHolder;
	
		return newBlock;
	}
	
	public GameObject InstantiateBlock(float posX, float posY)
	{
		return Instantiate(blockPrefab, new Vector3(posX, 0.25f, posY), Quaternion.identity) as GameObject;
	}
	
	// Move the block around the map with the mouse position
	public void MoveBlock(GameObject moveBlock)
	{
		if ( ! moveBlock || moveBlock == null) {
			return;
		}
	
		// Cast out a ray from the camera
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		// Get the object that the ray is hitting
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {			
		
			bool shouldMove = false;
			
			// If the object that is hit is a tile
			if (hit.transform.gameObject.name == map.tilePrefab.name) {
				shouldMove = true;
			} 
			// If the object hit is an existing block
			foreach (string b in blocks.Keys) {
				if (hit.transform.gameObject.name == b) {
					shouldMove = true;
				}
			}
			
			if (shouldMove) {
				// Move the object to the next position
				moveBlock.transform.position = new Vector3(
					Mathf.Round(hit.point.x),
					0.25f,
					Mathf.Round(hit.point.z)
				);
			}
		}
	}
	
	public void PlaceBlock(GameObject placedBlock)
	{
		// Check to see if a block is located at the current position
		// TODO: check for type when adding different kinds of blocks
		if (blockHolder.FindChild("Block("+placedBlock.transform.position.x+","+placedBlock.transform.position.z+")")) {
			return;
		}
								
		// Create a new block in the scene
		placedBlock = Instantiate(blockPrefab, placedBlock.transform.position, Quaternion.identity) as GameObject;
		placedBlock.name = "Block(" + placedBlock.transform.position.x + "," + placedBlock.transform.position.z + ")";
		placedBlock.transform.parent = blockHolder;
		
		// Save the Block
		SaveBlock(placedBlock);
	}
	
	public bool CanPlaceBlock(GameObject checkBlock)
	{
		// If the block does not exist, false
		if ( ! checkBlock || checkBlock == null) {
			return false;
		}
		
		// Loop through all the blocks in the scene and check their positions
		if (blocks.Values.Count > 0) {
			for (int i = 0; i < blocks.Values.Count; i++) {
			
				// Check the position of all the blocks
				if (blocks["Block("+checkBlock.transform.position.x+","+checkBlock.transform.position.z+")"] != null && blocks["Block("+checkBlock.transform.position.x+","+checkBlock.transform.position.z+")"].transform.position == checkBlock.transform.position) {
					return false;
				}
			}
		}
		return true;
	}
	
	public void RemoveBlock(GameObject removeBlock)
	{
		// Check to see if there is a block in the current mouse position
		if ( ! CanRemoveBlock(removeBlock)) {
			return;
		}
		
		// 
	}
	
	public bool CanRemoveBlock(GameObject removeBlock)
	{
		// Check to see if the block passed, is in the blocks array
		
				
		return false;
	}
	
	public void SaveBlocks()
	{
		// Loop through all the blocks in the scene and save them to a file/db
	}
	
	public void SaveBlock(GameObject saveBlock)
	{
		// Put the block in the blocks dictionary
		GameObject temp;
		if ( ! blocks.TryGetValue("Block("+saveBlock.transform.position.x+","+saveBlock.transform.position.z+")", out temp)) {
			blocks.Add("Block("+saveBlock.transform.position.x+","+saveBlock.transform.position.z+")", saveBlock);
		}
	}
}
