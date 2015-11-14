using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Rigidbody rbody;
	public Transform trans;
	//public TextMesh timer;
	public ParticleSystem ps;
	public Texture jumpTexture;
	public SphereCollider ballcollider;

	float seconds;
	bool gameOver;
	string temp_time;
	bool abilityToTap;
	double dpiBasedFontSize;
	float distToGround; 

	// Use this for initialization
	void Start () {
		rbody = GetComponent ("Rigidbody") as Rigidbody;
		trans = GetComponent ("Transform") as Transform;
		ps.Clear ();
		ps.Pause ();
		seconds = 0;
		gameOver = false;
		abilityToTap = true;
		dpiBasedFontSize = setFontBasedOnDpi();
		distToGround = ballcollider.bounds.extents.y;

	}

	void OnGUI() {
		
		/*
		if (GUI.RepeatButton (new Rect (Screen.width/4 * 3, Screen.height/4 * 3, Screen.width/5, Screen.height/5), rightTexture))
			rbody.AddForce (Vector3.right * 15);

		if (GUI.RepeatButton (new Rect (Screen.width/20, Screen.height/4 *3, Screen.width/5, Screen.height/5), leftTexture))
			rbody.AddForce (Vector3.left * 15);


			*/


		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = (int) dpiBasedFontSize;
		centeredStyle.fontStyle = FontStyle.Bold;
		/*
		if (GUI.Button(new Rect(Screen.width / 6 - Screen.width/9, Screen.height / 4 * 3, Screen.width / 5, Screen.height / 6), jumpTexture) &  isGrounded() ) {
			rbody.velocity = new Vector3 (rbody.velocity.x, 8F, rbody.velocity.z);		
		}*/
		
		if (gameOver) {
			DestroyObject(rbody);
			GUI.Label (new Rect (Screen.width / 4, Screen.height / 5 * 4, Screen.width / 2, Screen.height / 6), "Survived for: " + seconds.ToString ("F2") + " seconds.", centeredStyle);
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 8, Screen.height / 5, Screen.width / 4, Screen.height / 6), "<size="+(int) dpiBasedFontSize+">Try Again</size>"))
				Application.LoadLevel ("Main");
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 8, Screen.height / 2, Screen.width / 4, Screen.height / 6), "<size="+(int) dpiBasedFontSize+">Main Menu</size>"))
				Application.LoadLevel ("Menu");
		} else {
			GUI.Label (new Rect (Screen.width / 4, Screen.height / 5 * 4, Screen.width / 2, Screen.height / 6), "Survived for: " + seconds.ToString ("F2") + " seconds.", centeredStyle);
		}
	}


	// Update is called once per frame
	void Update () {

		if (gameOver == true) {
			ps.Play ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) { 
			Application.LoadLevel ("Menu");
		}

		/* Allows movement */
		if (gameOver == false) {
			/* Game over condition */
			if (trans.position.x < -10) {
				SetPreferenceString("highscore", seconds);

				rbody.velocity = new Vector3 (-1F, 2F, 0);
				gameOver = true;
				abilityToTap = false;
				Invoke ("switchAbilityToTap", 2);
			}
			
			/* Game over condition */
			if (trans.position.x > 10) {
				SetPreferenceString("highscore", seconds);

				rbody.velocity = new Vector3 (1F, 2F, 0);
				gameOver = true;
				abilityToTap = false;
				Invoke ("switchAbilityToTap", 2);
				
			}

			seconds += Time.deltaTime;
			trans.position = new Vector3 (trans.position.x, trans.position.y, 0);

			if (Input.GetKey ("left")) {
				rbody.AddForce (Vector3.left * 15);
			}

			if (Input.GetKey ("right")) {
				rbody.AddForce (Vector3.right * 15);
			}

			if (Input.GetKey ("up") &  isGrounded() ) {
				rbody.velocity = new Vector3 (rbody.velocity.x, 8F, rbody.velocity.z);		
			}

			if (rbody.velocity.y < 0) {
				rbody.velocity = new Vector3 (rbody.velocity.x, rbody.velocity.y - 0.25F, rbody.velocity.z);		
			}

			/* Mobile and Touch */
			/*
			if (Input.touchCount > 0) {
				// Get movement of the finger since last frame
				if (Input.GetTouch(0).position.y > Screen.height/5 - 1 &  isGrounded() )
					rbody.velocity = new Vector3 (rbody.velocity.x, 8F, rbody.velocity.z);		

			}*/
		}
	}

	void switchAbilityToTap(){
		abilityToTap = !abilityToTap;
	}

	double setFontBasedOnDpi(){
		double screendpi = Screen.dpi;
		if (screendpi <= 120) { //ldpi
			return 20 * (3/4);
		}else if( screendpi <= 160 ){ //mdpi
			return 20;
		}else if( screendpi <= 160 ){ //hdpi
			return 20 * 1.5;
		}else if( screendpi <= 160 ){ //xhdpi
			return 20 * 2;
		}else if( screendpi <= 160 ){ //xxhdpi
			return 20 * 2.5;
		}else{ //xxxdpi
			return 20 * 3;
		}
	}

	bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1F);
	}

	void SetPreferenceString (string prefKey, float prefValue) {
		
		if (Application.platform == RuntimePlatform.Android) {
			if(prefKey == "highscore"){
				double old_highscore = PlayerPrefs.GetFloat("highscore", 0.00F);
				if(old_highscore < prefValue){
					PlayerPrefs.SetFloat (prefKey, prefValue);
					PlayerPrefs.Save();
				}
			}
		}
	}
}


/*
 * 
 * 
 * Legacy Code - 
 * Mouse click 

if (Input.GetMouseButton(0)) {
	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	RaycastHit hit;

	if (Physics.Raycast(ray, out hit)) {
		if (hit.transform.name == "Left" )rbody.AddForce (Vector3.left * 10);
		if (hit.transform.name == "Right" )rbody.AddForce (Vector3.right * 10);
	}
}

if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
	// Get movement of the finger since last frame
	Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

	float x = Mathf.Abs(touchDeltaPosition.x);
	float y = Mathf.Abs (touchDeltaPosition.y);
	if( x > y ){
		if (touchDeltaPosition.x > 0){
			rigidbody.AddForce (Vector3.right * 25);
		}else{
			rigidbody.AddForce (Vector3.left * 25);
		}
	}
}
*/