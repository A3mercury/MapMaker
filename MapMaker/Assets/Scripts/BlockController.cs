using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	public GameObject block;
	public MapMaker map;
	
	public GameObject[] blocks;
	
	private Transform blockHolder;
	
	void Start() 
	{
		// Start by instantiating the starting block
		block = Instantiate(block, new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
		
		// Create a new array with the size of whatever the number of possible blocks on the map can be
		blocks = new GameObject[(int)map.mapSize.x * (int)map.mapSize.y];
		
		// Set the parent for where the blocks are to go
		string holderName = "WorldBlocks";
		blockHolder = new GameObject(holderName).transform;
		blockHolder.parent = transform;
	}
	
	void Update() 
	{
		// Place a block
		if (Input.GetKey(KeyCode.Mouse0)) {
			PlaceBlock();
		}
	
		// Move the block around the map
		MoveBlock();
		
		// ##### DEBUG ##### \\
		if (Input.GetKeyDown (KeyCode.P)) {
			foreach (GameObject b in blocks) {
				if (b != null) {
					Debug.Log (b.name);
				}
			}
		}
		// ################# \\
	}
	
	// Move the block around the map with the mouse position
	public void MoveBlock()
	{
		// Cast out a ray from the camera
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		// Get the object that the ray is hitting
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {			
			
			// If the object that is hit is a tile
			if (hit.transform.gameObject.name == map.tilePrefab.name) {
				
				// Move the object to the next position
				block.transform.position = new Vector3(
					Mathf.Round(hit.point.x),
					0.25f,
					Mathf.Round(hit.point.z)
				);
			}
		}
	}
	
	public void PlaceBlock()
	{
		// Check to see if a block is located at the current position
		if ( ! CanPlaceBlock()) {
			return;
		}
								
		// Create a new block in the scene
		GameObject placedBlock = Instantiate(block, block.transform.position, Quaternion.identity) as GameObject;
		placedBlock.name = "Block(" + placedBlock.transform.position.x + "," + placedBlock.transform.position.z + ")";
		placedBlock.transform.parent = blockHolder;
		
		// Save the Block
		SaveBlock(placedBlock);
	}
	
	public bool CanPlaceBlock()
	{
		// Loop through all the blocks in the scene and check their positions
		if (blocks.Length > 0) {
			for (int i = 0; i < blocks.Length; i++) {
			
				// Check the position of all the blocks
				if (blocks[i] != null && blocks[i].transform.position == block.transform.position) {
					return false;
				}
			}
		}
		return true;
	}
	
	public void RemoveBlock()
	{
		//
	}
	
	public bool CanRemoveBlock()
	{
		//
		return true;
	}
	
	public void SaveBlocks()
	{
		// Loop through all the blocks in the scene and save them to a file/db
	}
	
	public void SaveBlock(GameObject saveBlock)
	{
		// Put the block in the blocks list
		for (int i = 0; i < blocks.Length; i++) {
			if (blocks[i] == null) {
				blocks[i] = saveBlock;
				break;
			}
		}
	}
}
