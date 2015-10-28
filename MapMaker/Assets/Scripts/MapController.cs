using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	public Camera mainCamera;
	public MapMaker map;
	

	// Use this for initialization
	void Start () 
	{	
		//
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		// TODO: If SPACE + MOUSE 0 is pressed, drag the camera aroud the map
		//		 for now it's just the arrow keys
		DragCamera();
		
		// Scroll the camera in and out
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			Camera.main.orthographicSize--;
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			Camera.main.orthographicSize++;
		}
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5, 50);
		
	}
	
	// Drag the camera around the map
	public void DragCamera()
	{
		float speed = 10;
		if (Input.GetKey(KeyCode.RightArrow)) {
			if (mainCamera.transform.position.x <= map.mapSize.x/2) {
				mainCamera.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
			}
		} 
		if (Input.GetKey(KeyCode.LeftArrow)) {
			if (mainCamera.transform.position.x >= -(map.mapSize.x/2)) {
				mainCamera.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
			}
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			if (mainCamera.transform.position.z <= map.mapSize.y/2 - 20) {
				mainCamera.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
			}
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			if (mainCamera.transform.position.z >= -(map.mapSize.y/2)) {
				mainCamera.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
			}
		}
	}
}
