using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackObject : MovableObject {
	//Validates if the attack should hit the victim
	//Also adds the victim to the list of people attacked,
	List<MovableObject> _victims;
	// The 8 different types of attacks
	public AttackType P_attackType { get { return _attackType;}}
	public enum AttackType {Normal, Fire, Ice, Lightning, Poison};
	AttackType _attackType;
	// The attack value of the attack's creator.
	public int P_attackValue { get {return _attackValue;}}
	int _attackValue;
	// Attack multiplier
	public float P_multiplier { get { return _multiplier;}}
	float _multiplier;
	// Status effect attached to the attack.
	public StatusEffect P_statusEffect { get { return _statusEffect;} set {_statusEffect = value;}}
	StatusEffect _statusEffect;
	public float P_statusTimer { get { return _statusTimer;}}
	float _statusTimer;
	public float P_statusMultiplier { get { return _statusMultiplier;}}
	float _statusMultiplier;
	// Has the attack been initialized? To prevent problems with the countdowntimer
	bool _isInit;
	// Life timer for the attack. 
	float _timer;

	public bool ValidateAttack(MovableObject victim)
	{
		foreach(MovableObject m in _victims)
		{
			if(m == victim)
				return false;
		}
		_victims.Add(victim);
		return true;
	}

	// Use this for initialization
	public override void Awake () {
		base.Awake();
		_victims = new List<MovableObject>();
		_timer = 0.0F;
		_attackType = AttackType.Normal;
		_isInit = false;
	}	

	// Ends the attack
	public void endAttack(){_timer = 0.0F;}

	public void AddStatusEffect(float statusDuration, StatusEffect statusEffect, float statusMult){
		_statusTimer = statusDuration;
		_statusEffect = statusEffect;
		_statusMultiplier = statusMult;
	}

	public void InitializeAttack(float duration, float multiplier, int attackValue, AttackType type = AttackType.Normal){
		_timer = duration;
		_attackType = type;
		_multiplier = multiplier;
		_attackValue = attackValue;
		_isInit = true;
	}

	// Attack overrides both move functions since it does
	// not have a Rigid Body. This is to prevent unpredictable
	// collisions with other Rigid Bodies.
	public override void PerformMove(){
		if (H_movementTimer <= H_movementDuration) {
			Vector2 movement = new Vector2 (0.0F, 0.0F);
			movement.x = Mathf.Sin (H_movementAngle) * H_movementSpeed;
			movement.y = Mathf.Cos (H_movementAngle) * H_movementSpeed;
			transform.position += (Vector3)movement;
			H_movementTimer += Time.deltaTime;
		} else {
			H_movementTimer = 0.0F;
			H_moving = false;
		}
	}
	
	// Attack overrides both move functions since it does
	// not have a Rigid Body. This is to prevent unpredictable
	// collisions with other Rigid Bodies.
	public override void Move(Vector2 direction, float distance, float duration){
		if (duration <= Utils.eps) {
			Vector2 movement = new Vector2 (0.0F, 0.0F);
			float angle = 0.0F;
			angle = Mathf.Deg2Rad * Utils.Vec2Ang (direction);
			movement.x = Mathf.Sin (angle)  * distance;
			movement.y = Mathf.Cos (angle)  * distance;
			transform.position += (Vector3)movement;	
		} else {
			H_movementDuration = duration;
			H_movementAngle = Mathf.Deg2Rad * Utils.Vec2Ang (direction);
			H_movementSpeed = distance / duration;
			H_movementTimer = 0.0F;
			H_moving = true;
		}
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (_timer <= Utils.eps) {
			Destroy(gameObject);
		}
		if (_timer > Utils.eps && _isInit) {
			_timer -= Time.deltaTime;
		}
	}
}

