using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct StatsSave
{
	//combat stats
	public float currentHP;
	public float maxHP;
	public int bonusHP;
	public float bonusHPPercent;
	public float currentStamina;
	public float maxStamina;
	public int bonusStamina;
	public float bonusStaminaPercent;
	public float staminaRegenRate;
	public float healthRegenRate;
	public int attackValue;
	public int attackBonus;
	public float attackBonusPercent;
	public int defenseValue;
	public int defenseBonus;
	public float defenseBonusPercent;

	// Attack elements stats
	public float normalResistance;
	public float fireResistance;
	public float iceResistance;
	public float poisonResistance;
	public float lightningResistance;
	public float holyResistance;
	public float darkResistance;

	// Character general stats
	public int currentEssence;
	public int currentLevel;
}
public abstract class Personnage : BattleObject {



	// Use this for initialization
	public override void Awake () {
		base.Awake ();
 	}

	public override void OnTriggerEnter2D (Collider2D col)
	{
		if(col.tag == "Door")
		{
			if(col.GetComponent<Door>().CanUse)
				GlobalScript.Instance.EnterDoor(col.GetComponent<Door>());
		}
		else
			base.OnTriggerEnter2D(col);
	}	

	// Move and accelerate the character.
	public virtual void MoveCharacter(Vector2 newDirection){
		Vector2 movement = new Vector2 (0.0F, 0.0F);
		float angle = 0.0F;
		// If direction hasn't changed, accelerate
		if (Mathf.Abs( newDirection.x - H_objectDirection.x) <= Utils.eps 
	     && Mathf.Abs( newDirection.y - H_objectDirection.y) <= Utils.eps)
		{
			if (H_speed < H_maxSpeed){
				H_speed += H_acceleration;
				if (H_speed > H_maxSpeed)
				{
					H_speed = H_maxSpeed;
				}
			}
		} else {
			H_speed = H_minSpeed;
		}
		H_objectDirection = newDirection;
		angle = Mathf.Deg2Rad * Utils.Vec2Ang (H_objectDirection);
		movement.x = Mathf.Sin (angle) * H_speed;
		movement.y = Mathf.Cos (angle) * H_speed;
		rigidbody2D.velocity = movement;
	}
	
	public override void NoHP()
	{
		GlobalScript.Instance.PlayerDeath();
		Destroy(gameObject);
	}

	public void AwardEssence(int essence){
		H_currentEssence +=  essence;
	}
	
	public StatsSave GetStats()
	{
		StatsSave stats = new StatsSave();
		
		//combat stats
		stats.currentHP = H_currentHealth;
		stats.maxHP = H_maxHealth;
		stats.bonusHP = P_healthBonus;
		stats.bonusHPPercent = P_healthBonusPercent;
		stats.currentStamina = H_currentStamina;
		stats.maxStamina = H_maxStamina;
		stats.bonusStamina = P_staminaBonus;
		stats.bonusStaminaPercent = P_staminaBonusPercent;
		stats.staminaRegenRate = H_staminaRegenRate;
		stats.healthRegenRate = H_healthRegenRate;
		stats.attackValue = H_attackValue;
		stats.attackBonus = P_attackBonus;
		stats.defenseValue = H_defenseValue;
		stats.defenseBonus = P_defenseBonus;
		
		// Attack elements stats
		stats.normalResistance = H_normalResistance;
		stats.fireResistance = H_fireResistance;
		stats.iceResistance = H_iceResistance;
		stats.poisonResistance = H_poisonResistance;
		stats.lightningResistance = H_lightningResistance;
		
		// Character general stats
		stats.currentEssence = H_currentEssence;
		stats.currentLevel = H_currentLevel;
		
		return stats;
	}

	public bool SetStats(StatsSave stats)
	{
		//SHOULDN'T ACTUALLY APPLY THEM
		//TODO : ACTUALLY APPLY THEM
		
		
		// Character combat stats
		H_currentHealth = stats.currentHP;
		H_maxHealth = stats.maxHP;
		H_currentStamina = stats.currentStamina;
		H_maxStamina = stats.maxStamina;
		H_staminaRegenRate = stats.staminaRegenRate;
		H_healthRegenRate = stats.healthRegenRate;
		H_attackValue = stats.attackValue;
		P_attackBonus = stats.attackBonus;
		H_defenseValue = stats.defenseValue;
		P_defenseBonus = stats.defenseBonus;
		
		// Attack elements stats
		H_normalResistance = stats.normalResistance;
		H_fireResistance = stats.fireResistance;
		H_iceResistance = stats.iceResistance;
		H_poisonResistance = stats.poisonResistance;
		H_lightningResistance = stats.lightningResistance;
		
		// Character general stats
		H_currentEssence = stats.currentEssence;
		H_currentLevel = stats.currentLevel;
		UpdateStats();
		return true;
	}
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		// This vector will be used to compare the new input
		// direction with the current direction of the player
		Vector2 _newDirection = new Vector2 (0.0F, 0.0F);
		
		// First of all, verify that if the player is dashing
		// The direction cannot be changed during a dash.
		// Obtain current input direction
		if (Input.GetKey (KeyCode.W)) {
			_newDirection += new Vector2 (0.0F, 1);		
		}	
		if (Input.GetKey (KeyCode.S)) {
			_newDirection += new Vector2 (0.0F, -1);		
		}	
		if (Input.GetKey (KeyCode.A)) {
			_newDirection += new Vector2 (-1,0.0F);		
		}	
		if (Input.GetKey (KeyCode.D)) {
			_newDirection += new Vector2 (1,0.0F);		
		}
		
		
		if (Input.GetKeyDown(KeyCode.Space) && H_canDash && CheckStamina(H_dashCost)){
			H_animator.SetInteger("AnimationState", 1000);
			if (Mathf.Abs(_newDirection.x) >= Utils.eps || Mathf.Abs(_newDirection.y) >= Utils.eps) {
				Dash (_newDirection);
			} else {					
				Dash (H_objectDirection);
			}
		}
		if (H_canAction) {
			if (Mathf.Abs(_newDirection.x) >= Utils.eps || Mathf.Abs(_newDirection.y) >= Utils.eps) {
				MoveCharacter (Utils.Get8Dir(_newDirection));	
				H_animator.SetInteger("AnimationState", 999);
			}else{				
				H_animator.SetInteger("AnimationState", 0);
			}
			if (Input.GetMouseButtonDown(0)){
				if(CheckStamina(10)){
					PerformAttacks();
				}
			} else if (Input.GetMouseButtonDown(1)){
				if(CheckStamina(10)){
					PerformSmashes();
				}
				
			} else if (Input.GetKeyDown(KeyCode.Q)){
				if(CheckStamina(H_skillQ.staminaCost)){
					H_currentSkill = 0;
					H_ObjectState = ObjectState.PerformingSkill;
					H_skillQ.performSkill();
					
				}	
			} else if (Input.GetKeyDown(KeyCode.E)){
				if(CheckStamina(H_skillE.staminaCost)){
					H_currentSkill = 1;
					H_ObjectState = ObjectState.PerformingSkill;
					H_skillE.performSkill();
					
				}	
			} else if (Input.GetKeyDown(KeyCode.F)){
				if(CheckStamina(H_skillF.staminaCost)){
					H_currentSkill = 2;
					H_ObjectState = ObjectState.PerformingSkill;
					H_skillF.performSkill();
					
				}
			}
		}
	}
}






















