using UnityEngine;
using System.Collections;

public class MenuDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 150, 100), "Load Test Level 1"))
			Application.LoadLevel("levelTest");
		if (GUI.Button(new Rect(10, 120, 150, 100), "Load Test Boss 1"))
			Application.LoadLevel("bossTest");
		
	}
}
