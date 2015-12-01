using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {
	public int DoorCode = 0;
	// Use this for initialization
	void Awake () {
		Destroy (GetComponent<SpriteRenderer>());
	}

}
