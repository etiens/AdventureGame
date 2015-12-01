using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public Enemy EnemyPrefab;
	public bool Respawns = false;
	public bool Spawns = true;
	public float RespawnTimer = 10.0f;
	float timer = -1;

	public bool SpawnEnemy()
	{
		if(Spawns)
		{
			if(Respawns)
			{
				if(timer < 0)
				{
					Enemy en = (Enemy)Instantiate(EnemyPrefab);
					en.transform.position = transform.position;
					timer = RespawnTimer;
					return true;
				}
			}
			else
			{
				Enemy en = (Enemy)Instantiate(EnemyPrefab);
				en.transform.position = transform.position;
				Spawns = false;
				return true;
			}
		}
		return false;
	}
	// Use this for initialization
	void Awake () {
		Destroy (GetComponent<SpriteRenderer>());
	}
	
	// Update is called once per frame
	void Update () {
		if(Respawns)
		{
			timer-= Time.deltaTime;
		}
	}
}
