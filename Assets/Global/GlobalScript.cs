using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class GlobalScript : Singleton<GlobalScript> {

	public Personnage LePersonnage {get {return _lePersonnage;}	set {_lePersonnage = value;}}
	public Personnage P_personnagePrefab;
	public bool P_playerDead = true;
	Personnage _lePersonnage;
	public List<Enemy> _lesEnemis;
	LevelControl _currentLevel;
	int _nextDoor = 0;
	public List<StatusEffect> _StatusEffects;

	StatsSave _lastStats;
	bool _hasSavedStats = false;

	// Mostly to make sure that 
	void Awake()
	{
		if (P_playerDead == false)
		{
			_lePersonnage = (Personnage)Instantiate(P_personnagePrefab);
			BattleInterface.Instance.Initialize();
			Camera.main.GetComponent<CameraBehavior>().P_target = _lePersonnage.transform;
			Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
			foreach (Enemy e in enemies) {
				e.SetTarget(_lePersonnage.transform);
				_lesEnemis.Add(e);
			}
		}
		DontDestroyOnLoad(transform.gameObject);
		//DontDestroyOnLoad(transform.root.gameObject);
	}
	public void SetPlayerDeathState(bool isDead)
	{
		P_playerDead = isDead;
	}
	// This function initializes a level
	// It spawns enemies, and loads the player
	public void InitLevel (LevelControl level) {
		_lePersonnage = level.StartLevel(P_personnagePrefab,_nextDoor);
		P_playerDead = false;
		if (_hasSavedStats) {
			_lePersonnage.SetStats (_lastStats);
			foreach(StatusEffect se in _StatusEffects)
			{
				se.ReInit(_lePersonnage);
			}
		}
		BattleInterface.Instance.Initialize();
		Camera.main.GetComponent<CameraBehavior>().P_target = _lePersonnage.transform;
		//This should be moved to the Level scripts
		Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		_lesEnemis.Clear();
		int enemycount = 1;
		foreach (Enemy e in enemies) {
			e.SetTarget(_lePersonnage.transform);
			_lesEnemis.Add(e);
			e.name = "Enemy"+enemycount.ToString();
			enemycount++;
		}
		Screen.showCursor = false;
	}
	public void PlayerDeath()
	{
		P_playerDead = true;
		foreach(Enemy e in _lesEnemis)
			e.PlayerDied();
	}
	//This is called when a player enters a door to change locations
	//It saves what needs to be transfered over to the next room
	//Such as player's stats and statuses
	//As well as the room he's about to enter
	public void EnterDoor(Door door)
	{
		_hasSavedStats = true;
		_lastStats = _lePersonnage.GetStats();
		_StatusEffects.Clear ();
		StatusEffect[] statusEf = FindObjectsOfType(typeof(StatusEffect)) as StatusEffect[];
		foreach (StatusEffect se in statusEf) 
		{
			if(se.GetHost() == _lePersonnage.GetComponent<BattleObject>())
			{
				se.Pause();
				_StatusEffects.Add(se);
			}
			else
				Destroy (se.gameObject);
		}


		BattleInterface.Instance.UnInitialize();
		_nextDoor = door.DoorCode;
		Application.LoadLevel(door.LevelName);
	}
	/*public void SpawnPlayer(int DoorCode)
	{
		foreach(PlayerSpawn ps in P_PlayerSpawns)
		{
			if(ps.DoorCode == DoorCode)
			{
				//Destroy(_lePersonnage.gameObject);
				//_lePersonnage = (Personnage)Instantiate(P_personnagePrefab);
				_lePersonnage.transform.position = ps.transform.position;
			}
		}

	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
