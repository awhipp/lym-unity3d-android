using UnityEngine;
using System.Collections;

public class menu_driver : MonoBehaviour {

	public GameObject selector;
	public GameObject selector2;
	public Rigidbody rbody1;
	public Rigidbody rbody2;
	public Texture devLogo;
	//public Camera camera;

	double dpiBasedFontSize;
	bool isInstructions;
	int dpiBasedButtonSize;
	float highscore;
	
	public float GetPreferenceString (string prefKey) {
		if (Application.platform == RuntimePlatform.Android) {
			return PlayerPrefs.GetFloat("highscore", 0.00F);
		}
		return 0.00F;
	}


	void OnGUI() {

		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = (int) dpiBasedFontSize;
		centeredStyle.fontStyle = FontStyle.Bold;

		if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 5, Screen.width / 2, Screen.height / 6), "<size="+dpiBasedButtonSize+">Click to Play\nLose your Marbles!</size>"))
			Application.LoadLevel ("Main");


		GUI.Label (new Rect (Screen.width / 6, Screen.height / 2, Screen.width /3 * 2, Screen.height / 2), "Joystick Left and Right to Move\n Button to Jump", centeredStyle);

		GUI.Label (new Rect (Screen.width / 6, Screen.height / 5 * 4, Screen.width / 4, Screen.height / 6), "<color=#C9C9C9>High Score: " + highscore.ToString("F2") + "</color>", centeredStyle);

		GUI.Label (new Rect (Screen.width / 2, Screen.height / 5 * 4, Screen.width/3, Screen.height /2), devLogo);


	}

	double setFontBasedOnDpi(){
		double screendpi = Screen.dpi;
		int fontsize;
		if (!isInstructions)
			fontsize = 25;
		else
			fontsize = 22;

		if(screendpi <= 120) { //ldpi
			return fontsize * (3/4);
		}else if( screendpi <= 160 ){ //mdpi
			return fontsize;
		}else if( screendpi <= 240 ){ //hdpi
			return fontsize * 1.5;
		}else{ //xhdpi
			return fontsize * 2;
		}
	}

	// Use this for initialization
	void Start () {
		/*
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
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
		*/

		rbody1 = selector.GetComponent ("Rigidbody") as Rigidbody;
		rbody2 = selector2.GetComponent ("Rigidbody") as Rigidbody;
		dpiBasedButtonSize = (int) setFontBasedOnDpi();

		isInstructions = true;

		dpiBasedFontSize = setFontBasedOnDpi();

		if (GetPreferenceString ("highscore") == 0.00F) {
			highscore = 0.00F;
		} else {
			highscore = GetPreferenceString ("highscore");
		}

	}


	void Update () {

		selector.transform.Rotate (5F, 5F, 0F);
		selector2.transform.Rotate (5F, 5F, 0F);

		if (selector.transform.position.y < 0.85) {
			rbody1.velocity = new Vector3 (0, 8F, 0);
			rbody2.velocity = new Vector3 (0, 8F, 0);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) { 
			Application.Quit ();
		}
	}

}
