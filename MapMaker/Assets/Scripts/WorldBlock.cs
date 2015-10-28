using UnityEngine;
using System.Collections;

public class WorldBlock : MonoBehaviour {
				
	private string blockName;
	public string type;

	// Use this for initialization
	void Start () 
	{
		//
	}
	
	// Getters and Setters //
	
	public string BlockName {
		get { 
			return blockName; 
		}
		set {
			blockName = value;
			if (gameObject && gameObject != null) {
				gameObject.name = value;
			}
		}
	}
	
	public string Type {
		get { 
			return type; 
		}
		set { 
			type = value; 
		}
	}
}
