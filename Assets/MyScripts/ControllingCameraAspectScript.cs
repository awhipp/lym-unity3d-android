using UnityEngine;
using System.Collections;

public class ControllingCameraAspectScript : MonoBehaviour{
	
	// Use this for initialization
	void Start(){
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		Rect rect = camera.rect;
		float scalewidth;
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f){
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}else{ // add pillarbox

			scalewidth = 1.0f / scaleheight;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}
	
}