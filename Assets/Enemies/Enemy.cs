using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : BattleObject {
	public enum AiState{Idle,Patrol,Approach,Combat}
	public AiState _aiState = AiState.Idle;
	public List<Limb> _limbs;
	public Transform hitstuff;
	protected float _aggroRange = 8;
	protected float _loseAggroRange = 10;
	protected float _fightMinRange = 2.5f;
	protected float _fightMaxRange = 4;
	protected float _attackRange = 1.1f;
	protected float _safeDistanceCooldown = 2f;
	protected float _attackCooldownMax = 2.5f;
	protected float _attackCooldownCur = -1.5f;

	//Pathfinding variables
	protected bool _hasSeen = false;
	protected Vector3 _lastSeen = Vector3.zero;
	public RaycastHit2D[] _rayHits;

	
	// Monster stats
	protected int H_essenceValue;

	protected bool _playerDead = false;


	//à changer quand il y aura plusieurs personnages, si c'est le cas
	protected Transform _target;
	
	// Use this for initialization
	public override void Awake () {
		base.Awake();
		foreach(Limb l in _limbs)
		{
			l.SetHost(this);
			l.InitLimb(1);
		}
		H_currentHealth = 250;
		H_essenceValue = 10;
	}
	//Overrides for generic attacks.
	public override void PerformAttack_1 ()
	{
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 1);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the object is in. 
			H_ObjectState = ObjectState.Attacking;
			H_canAction = false;
			H_canDash = false;
			// Do nothing
		} else if (H_attackTimeCounter <= 1.05F){
			H_attackTimeCounter += Time.deltaTime;
			
		} else if (H_attackTimeCounter <= 1.5F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.2F, 0.2F);
				Move(H_objectDirection, 1.0F, 0.2F);
				P_currentAttack.Scale(0.2F, 1.0F);
				H_hasAttacked = true;
			}
			
		} else if (H_attackTimeCounter <= 1.75F){
			// Initiates another attack, move the combo count up and destroy the current attack.
			H_attackCount = 1;
			H_attackTimeCounter = 0.0F;
			P_currentAttack.endAttack();
			H_hasAttacked = false;
			H_canAction = true;
		}else{
			ResetAttackState();
		}
	}
	
	//Overrides for generic attacks.
	public override void PerformAttack_2 ()
	{	
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 2);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the object is in. 
			H_canAction = false;
			H_canDash = false;
			// Do nothing
		} else if (H_attackTimeCounter <= 1.05F){
			H_attackTimeCounter += Time.deltaTime;
			
		} else if (H_attackTimeCounter <= 1.5F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.2F, 0.2F);
				Move(H_objectDirection, 1.0F, 0.2F);
				P_currentAttack.Scale(0.2F, 1.0F);
				H_hasAttacked = true;
			}
			
		} else if (H_attackTimeCounter <= 1.75F){
			// Initiates another attack, move the combo count up and destroy the current attack.
			H_attackCount = 2;
			H_attackTimeCounter = 0.0F;
			P_currentAttack.endAttack();
			H_hasAttacked = false;
			H_canAction = true;
		}else{
			ResetAttackState();
		}
	}
	
	//Overrides for generic attacks.
	public override void PerformAttack_3 ()
	{
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 3);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the object is in. 
			H_canAction = false;
			H_canDash = false;
			// Do nothing
		} else if (H_attackTimeCounter <= 1.05F){
			H_attackTimeCounter += Time.deltaTime;
			
		} else if (H_attackTimeCounter <= 1.5F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.2F, 0.25F);
				Move(H_objectDirection, 1.0F, 0.2F);
				P_currentAttack.Scale(0.2F, 1.0F);
				H_hasAttacked = true;
			}
			
		} else if (H_attackTimeCounter <= 1.75F){
			// End the combo
			H_attackCount = 3;
			H_attackTimeCounter = 0.0F;
			P_currentAttack.endAttack();
			H_hasAttacked = false;
			H_ObjectState = ObjectState.Smashing;
			H_canAction = true;
		}else{
			ResetAttackState();
		}
	}
	
	//Overrides for generic attacks.
	public override void PerformSmash_3_1 ()
	{
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 11);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the object is in. 
			H_canAction = false;
			H_canDash = false;
			// Do nothing
		} else if (H_attackTimeCounter <= 1.05F){
			H_attackTimeCounter += Time.deltaTime;
			
		} else if (H_attackTimeCounter <= 1.75F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.2F, 0.35F);
				gameObject.AddComponent<Poison>();
				P_currentAttack.AddStatusEffect(5F, gameObject.GetComponent<Poison>(), 0.1F);
				Move(H_objectDirection, 4.0F, 0.2F);
				P_currentAttack.Scale(0.6F, 1.5F);
				H_hasAttacked = true;
			}
			H_canAction = true;
		}else{
			ResetAttackState();
		}
	}

	public void SetTarget(Transform target)
	{
		_target = target;
	}

	//So it stops moving around once the player has died
	public void PlayerDied()
	{
		_target = null;
		_playerDead = true;
	}
	//Tries to see the target
	//If he can't, returns false
	//If he can, returns true and 
	// -sets TargetLocation to target's current location
	protected bool LocateTarget()
	{
		Vector2 dir = _target.position - transform.position;
		float mag = dir.magnitude;
		if(mag < _aggroRange)
		{
			LayerMask mask = 1 << 8;
			if(Physics2D.Raycast(transform.position,dir,Mathf.Sqrt(mag),mask))
			{
			   return false;
			}
			else
			{
				_lastSeen = _target.position;
				_hasSeen = true;
				return true;
			}
		}
		return false;
	}

	//Should be called when a limb runs out of life, to avoid losing references
	public virtual void LimbDestroyed(Limb limb)
	{
		_limbs.Remove(limb);
		Destroy(limb.gameObject);
	}
	public override void NoHP()
	{
		GlobalScript.Instance.LePersonnage.AwardEssence(H_essenceValue);
		foreach(Limb l in _limbs)
			Destroy (l);
		base.NoHP();
	}

	//might not be necessary
	//TODO : TEST
	public override void PerformAttacks (){
		
		switch (H_attackCount) {
			
		case 0 :
			PerformAttack_1();	
			break;
		case 1 : 
			PerformAttack_2();	
			break;
		case 2 : 
			PerformAttack_3();	
			break;
		case 3 : 
			PerformAttack_1();	
			break;
		default :
			break;
			
		}
	}
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(H_canAction && !_playerDead)
		{
			//AI();
			AI2();
		}

	}
	public virtual void AI2()
	{
		Vector2 dir = _target.position - transform.position;
		float mag = dir.magnitude;
		//Is the target in range?
		if(mag < _aggroRange)
		{
			LayerMask mask = (1 << 8);
			//LayerMask mask =  ~(1 << 9);
			//Is there a wall hiding the target
			//RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,mag,mask);

			//Debug.Log (hit.transform.name);
			//	Debug.DrawRay(transform.position,dir);

			//int numberhits = Physics2D.RaycastNonAlloc(transform.position,dir,_rayHits,Mathf.Sqrt(mag));
			//Debug.Log (numberhits.ToString()+" - "+transform.name);
			//if(numberhits > 1)			
			if(Physics2D.Raycast(transform.position,dir,mag,mask))
			{
				//hitstuff = hit.transform;
				//Debug.Log (hit.transform.name + this.name + _hasSeen.ToString ());
				//If you've seen him before, go there, else do nothing
				if(_hasSeen)
				{
					Vector3 newdir = _lastSeen - transform.position;
					float newmag = newdir.magnitude;
					//If you're where you last saw him, forget about it
					if(newmag < 1.5)
					{
						_hasSeen = false;
						H_ObjectState = ObjectState.Idle;
						_aiState = AiState.Idle;
					}
					else
					{
						ResetAttackState();
						H_ObjectState = ObjectState.Walking;
						H_objectDirection = newdir;
						rigidbody2D.velocity = H_objectDirection * 2;
						_aiState = AiState.Approach;
					}
				}
				else
				{
					//do nothing
					H_ObjectState = ObjectState.Idle;
					_aiState = AiState.Idle;
				}
			}
			else
			{

				_lastSeen = _target.position;
				_hasSeen = true;
				//If close enough, Attack, else, move closer
				if(mag < _attackRange)
				{
					rigidbody2D.velocity = Vector2.zero;
					if( H_ObjectState != ObjectState.Attacking && H_ObjectState != ObjectState.Smashing){
						PerformAttacks();
					}
					_attackCooldownCur = _attackCooldownMax;
					_aiState = AiState.Combat;
				}
				else
				{
					ResetAttackState();
					H_ObjectState = ObjectState.Walking;
					H_objectDirection = dir;
					rigidbody2D.velocity = H_objectDirection * 2;
					_aiState = AiState.Approach;
				}
			}
		}
		else
		{
			//do nothing
			H_ObjectState = ObjectState.Idle;
			_aiState = AiState.Idle;
		}
	}
	public virtual void AI()
	{		
		Vector2 dir = _target.position - transform.position;
		float mag = dir.magnitude;
		switch (_aiState)
		{
		case AiState.Idle:
			if(mag < _aggroRange)
			{
				_aiState = AiState.Approach;
				H_ObjectState = ObjectState.Walking;
			}
			break;
		case AiState.Patrol:

			break;
		case AiState.Approach:		
			if(mag < _fightMinRange)
			{
				//Attack him!!
				//H_ObjectState = ObjectState.Walking;
				//rigidbody2D.velocity = Vector2.zero;
				_aiState = AiState.Combat;
			}
			else if(mag < _loseAggroRange)
			{
				H_ObjectState = ObjectState.Walking;
				H_objectDirection = dir;
				rigidbody2D.velocity = H_objectDirection * 5;
			}
			else
			{
				_aiState = AiState.Idle;
				rigidbody2D.velocity = Vector2.zero;
				H_ObjectState = ObjectState.Idle;
			}
			break;
		case AiState.Combat:
			if(mag > _fightMaxRange)
			{
				_aiState = AiState.Approach;
				H_ObjectState = ObjectState.Walking;
			}
			//else if(_attackCooldownCur < 0)
			{
				if(mag < _attackRange)
				{
					rigidbody2D.velocity = Vector2.zero;
					if( H_ObjectState != ObjectState.Attacking && H_ObjectState != ObjectState.Smashing){
						PerformAttacks();
					}
					_attackCooldownCur = _attackCooldownMax;
				}
				else
				{
					ResetAttackState();
					H_ObjectState = ObjectState.Walking;
					H_objectDirection = dir;
					rigidbody2D.velocity = H_objectDirection * 2;
				}
			}
			/*else
			{
				_attackCooldownCur -= Time.deltaTime;
				H_ObjectState = ObjectState.Blocking;
				if(mag < _safeDistanceCooldown)
				{
					H_objectDirection = dir;
					rigidbody2D.velocity = H_objectDirection * -2;
				}
				else if(mag > _fightMinRange)
				{
					H_objectDirection = dir;
					rigidbody2D.velocity = H_objectDirection * 2;
				}
				else
				{
					rigidbody2D.velocity = Vector2.zero;
				}

			}*/
			break;
		default:
			break;
		}



	}


}
