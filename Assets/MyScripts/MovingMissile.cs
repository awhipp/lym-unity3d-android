using UnityEngine;
using System.Collections;

public class MovingMissile : MonoBehaviour {

	public GameObject player;

	private float missile_speed;
	private Transform trans;
	private Rigidbody rbody;
	private Transform playerTrans;



	/* Scale because of rotation: x = depth, y = length, z = height*/

	// Use this for initialization
	void Start () {
		rbody = GetComponent ("Rigidbody") as Rigidbody;
		playerTrans = GetComponent ("Transform") as Transform;

		missile_speed = 0 - Random.Range (1.0F, 3.0F);
		rbody.velocity = new Vector3 (missile_speed, 0, 0);

		trans = GetComponent ("Transform") as Transform;
		trans.position = new Vector3(Random.Range (11.0F, 16.0F), Random.Range(0.25F, 2.5F), 0);
		trans.localScale = new Vector3(Random.Range (0.5F, 1.0F), Random.Range (0.6F, 1.5F), Random.Range (0.6F, 1.5F));

	}
	
	// Update is called once per frame
	void Update () {
		trans.position = new Vector3 (trans.position.x, trans.position.y, 0);
		rbody.velocity = new Vector3 (missile_speed, (rbody.velocity.y)/2.0F, 0);

		float rockPos = playerTrans.position.y + (Random.Range (-0.5f, 0.5f));
		if (rockPos < 0.25f)
			rockPos = 0.25f;

		if (rbody.position.x < -11 & missile_speed > -15) {
			trans.position = new Vector3(Random.Range (11.0F, 16.0F), rockPos, 0);
			missile_speed -= Random.Range (1.0F, 3.0F);
			trans.localScale = new Vector3(Random.Range (0.5F, 1.0F), Random.Range (0.6F, 1.5F), Random.Range (0.6F, 1.5F));
			rbody.velocity = new Vector3 (missile_speed, 0, 0);
			Debug.Log(rockPos);


		} else if (rbody.position.x < -11 & missile_speed <= -15) {
			missile_speed = 0 - Random.Range (12.0F, 15.0F);
			rbody.velocity = new Vector3 (missile_speed, 0, 0);
			
			trans.position = new Vector3(Random.Range (11.0F, 16.0F), rockPos, 0);
			Debug.Log(rockPos);
			trans.localScale = new Vector3(Random.Range (0.5F, 1.0F), Random.Range (0.6F, 1.5F), Random.Range (0.6F, 1.5F));

		} 

	}
}
