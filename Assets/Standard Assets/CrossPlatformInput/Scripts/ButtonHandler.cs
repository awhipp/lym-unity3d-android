using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

		public string Name;
		public GameObject player;
		SphereCollider sc;
		Rigidbody rbody;
		float distToGround; 

		void Start(){
			RectTransform rt = GetComponent ("RectTransform") as RectTransform;
			sc = player.GetComponent ("SphereCollider") as SphereCollider;
			rt.position = new Vector3(Screen.width/5,Screen.height/2,0);
			rt.sizeDelta = new Vector2 (Screen.width / 75, Screen.height / 75);
			rbody = player.GetComponent("Rigidbody") as Rigidbody;
			distToGround = sc.bounds.extents.y;
		}

        void OnEnable()
        {

        }

        public void SetDownState()
        {
			CrossPlatformInputManager.SetButtonDown(Name);
			if(isGrounded())
				rbody.velocity = new Vector3 (rbody.velocity.x, 8F, rbody.velocity.z);		
		}

		bool isGrounded(){
			return Physics.Raycast(player.transform.position, -Vector3.up, distToGround + 0.1F);
		}


        public void SetUpState()
        {
			CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}
