using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapMaker))]
public class MapEditorScript : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		
		MapMaker map = target as MapMaker;
		
		map.DrawMap();
	}
}
