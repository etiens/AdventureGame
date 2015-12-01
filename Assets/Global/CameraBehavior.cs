using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraBehavior : MonoBehaviour {
	public Transform P_target;
	public float P_cameraDistance = 5;

	void Start() {
		if(!GlobalScript.Instance.P_playerDead)
			transform.position = new Vector3(0F,0F, P_target.position.z - P_cameraDistance);
	}
	//public void InitCamera(Transform
	
	// The camera will follow the player as he moves around.
	void Update(){
		if (GlobalScript.Instance.P_playerDead == false){
			transform.position = new Vector3(P_target.position.x,P_target.position.y, P_target.position.z - P_cameraDistance);
		}
	}
		
}
