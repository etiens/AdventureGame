using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelControl : MonoBehaviour {
	public List<Door> Doors;
	public List<PlayerSpawn> PlayerSpawns;
	public List<EnemySpawn> EnemySpawns;
	Personnage _lePersonnage;
	private float _delayLoadTimer = 0.5f;
	private bool _isInit = false;
	
	void Awake()
	{
		GlobalScript.Instance.SetPlayerDeathState(true);
		//DelayLoading();
		//GlobalScript.Instance.InitLevel(this);
	}
	IEnumerator DelayLoading() {
		Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(0.5f);
		Debug.Log("AFter Waiting 2 seconds");

	}
	public Personnage StartLevel(Personnage playerPrefab, int doorId)
	{
		_lePersonnage = Instantiate(playerPrefab) as Personnage;
		foreach(PlayerSpawn ps in PlayerSpawns)
			if(ps.DoorCode == doorId)
				_lePersonnage.transform.position = ps.transform.position;
		//Camera.main.transform.parent = _lePersonnage.transform;
                                                               		//Camera.main.transform.localPosition = new Vector3(0,0,-10);

		foreach(EnemySpawn es in EnemySpawns)
			es.SpawnEnemy();
		return _lePersonnage;
	}
	void Update()
	{
		if(_delayLoadTimer > 0)
		{
			_delayLoadTimer-=Time.deltaTime;
		}
		else if(!_isInit)
		{
			Debug.Break();
			_isInit = true;
			GlobalScript.Instance.InitLevel(this);
		}
	}
}
