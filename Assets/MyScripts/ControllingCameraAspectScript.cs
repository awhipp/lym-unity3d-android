using UnityEngine;
using System.Collections;

public class ControllingCameraAspectScript : MonoBehaviour{
	
	// Use this for initialization
	void Start(){
		float targetRatio = 16f/9f; //The aspect ratio you did for the game.
		Camera cam = GetComponent<Camera>();
		cam.aspect = targetRatio;
	}
	
}