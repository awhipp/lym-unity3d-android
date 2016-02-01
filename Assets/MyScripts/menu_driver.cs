using UnityEngine;
using System.Collections;

public class menu_driver : MonoBehaviour {

	public GameObject selector;
	public GameObject selector2;
	public Rigidbody rbody1;
	public Rigidbody rbody2;
	//	public Texture devLogo;
	//public Camera camera;

	double dpiBasedFontSize;
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

		if (GUI.Button (new Rect (0, 0, Screen.width, Screen.height ), "", centeredStyle))
			Application.LoadLevel ("Main");

		centeredStyle.fontSize = (int) (dpiBasedFontSize * 3/2);

		GUI.Label (new Rect (Screen.width / 6, Screen.height / 6, Screen.width /3 * 2, Screen.height / 2.6f), "Lose Your Marbles!", centeredStyle);

		centeredStyle.fontSize = (int) dpiBasedFontSize;

		GUI.Label (new Rect (Screen.width / 6, Screen.height / 2.6f, Screen.width /3 * 2, Screen.height / 2.6f), "Tap to Start\n\nJoystick: Move\n\nButton: Jump", centeredStyle);

		GUI.Label (new Rect (Screen.width / 3, Screen.height / 5 * 4, Screen.width / 3, Screen.height / 6), "<color=#C9C9C9>High Score: " + highscore.ToString("F2") + "</color>", centeredStyle);

		//		GUI.Label (new Rect (Screen.width / 2, Screen.height / 5 * 4, Screen.width/3, Screen.height /2), devLogo);


	}

	double setFontBasedOnDpi(){
		double screendpi = Screen.dpi;
		int fontsize = 40;

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

		rbody1 = selector.GetComponent ("Rigidbody") as Rigidbody;
		rbody2 = selector2.GetComponent ("Rigidbody") as Rigidbody;


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
