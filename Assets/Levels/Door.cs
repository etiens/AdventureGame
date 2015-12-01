using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public bool CanUse = false;
	public string LevelName = "levelTest";
	public int DoorCode = 0;

	// Use this for initialization
	void Awake () {
		Destroy (GetComponent<SpriteRenderer>());
	}
	/*public void OnTriggerEnter2D (Collider2D col)
	{
		if(CanUse)
		{
			if(col.tag == "Player")
			{
				GlobalScript.Instance.SpawnPlayer(DoorCode);
			}
		}
	}*/
}	

