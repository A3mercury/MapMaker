using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockController : MonoBehaviour {
	
	public GameObject worldBlockPrefab;
	
	private GameObject[,] blocks;
	private GameObject block;
	private WorldBlock worldBlock;
	
	private MapMaker map;
	private Transform blockHolder;
	
	void Start()
	{
		// Set the properties of the objects we use
		map = GameObject.FindObjectOfType<MapMaker>() as MapMaker;
		blocks = new GameObject[(int)map.mapSize.x, (int)map.mapSize.y];
		for (int i = 0; i < map.mapSize.x; i++) {
			for (int j = 0; j < map.mapSize.y; j++) {
				blocks[i, j] = null;
			}
		}
		
		// Create a holder for the blocks
		blockHolder = new GameObject("BlockHolder").transform;
		blockHolder.parent = GameObject.Find ("BlockController").transform;
	}
	
		
	void Update()
	{		
		if (block && block != null) {
		
			// Place the block on the map
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				PlaceBlock();
			}
			
			// Move the block, following the mouse position
			MoveBlock();
		} else {
		
			// Build block
			if (Input.GetKeyDown(KeyCode.B)) {
				CreateBlock();
				worldBlock.BlockName = "TempBlock";
			}
		}
		
		if (Input.GetKeyDown(KeyCode.P)) {
			PrintBlocks();
		}
	}


	public void CreateBlock()
	{
		// Cast a Ray and get the position on the map the Block will begin at
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
		
			// Get the hit points as the nearest ints
			Vector3 hits = new Vector3(Mathf.RoundToInt(hit.point.x), 0.25f, Mathf.RoundToInt(hit.point.z));
		
			// If a WorldBlock was already found at this position
			GameObject foundBlock = GameObject.Find("Block("+hits.x+","+hits.z+")") as GameObject;
			if (foundBlock && foundBlock != null) {
		
				// If the WorldBlock was found and the type is the same, return without saving
				WorldBlock foundWorldBlock = foundBlock.GetComponent<WorldBlock>() as WorldBlock;
				if (foundWorldBlock.Type == worldBlock.Type) {
					return;
				}
			}
		
			// Create a new GameObject based on the BlockPrefab and give it the hit position
			block = Instantiate(worldBlockPrefab, hits, Quaternion.identity) as GameObject;
			
			// Set the other properties of the WorldBlock
			block.transform.parent = blockHolder;
			
			// Use worldBlock to set some other properties
			worldBlock = block.GetComponent<WorldBlock>() as WorldBlock;
			worldBlock.BlockName = "Block(" + hits.x + "," + hits.z + ")";
			worldBlock.Type = "DEFAULT";
		}
	}
	
	public void PlaceBlock()
	{
		// Create a new block in this position, 
		// if a block of the same type doesn't already exist here
		CreateBlock();
		
		// Set the parent as the block holder
		block.transform.parent = blockHolder;
		
		// Save the block 
		SaveBlock();
	}
	
	public void MoveBlock()
	{
		// Cast a Ray and get the position on the map the Block will be moved to
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
			
			block.transform.position = new Vector3(
				Mathf.Round(hit.point.x),
				0.25f,
				Mathf.Round(hit.point.z)
			);
		}	
	}

	public void SaveBlock()
	{
		int x = (int)(block.transform.position.x + map.mapSize.x/2);
		int y = (int)(block.transform.position.z + map.mapSize.y/2);
		blocks[x, y] = block;
	}
	
	public void PrintBlocks()
	{
		for (int i = 0; i < map.mapSize.x; i++) {
			for (int j = 0; j < map.mapSize.y; j++) {
				if (blocks[i, j] != null) {
					Debug.Log ("("+i+","+j+") " + blocks[i, j].name);
				} else {
					Debug.Log ("("+i+","+j+") null");
				}
			}
		}
	}
}


