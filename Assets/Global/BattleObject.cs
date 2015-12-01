using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleObject : MovableObject {
	
	// General variables
	protected bool H_canAction	;
	public enum ObjectState {Idle,Walking,Dashing,Casting,Blocking,Hitstun,Knockdown,Stun,Attacking,Smashing,PerformingSkill};
	public ObjectState P_ObjectState{get{return H_ObjectState;}}
	protected ObjectState H_ObjectState = ObjectState.Idle;

	// Character combat stats
	public int P_currentHealth {get {return (int)H_currentHealth;}}
	public int P_totalHealth {get {return (int)H_totalHealth;}}
	public int P_currentStamina {get {return (int)H_currentStamina;}}
	public int P_totalStamina {get {return (int)H_totalStamina;}}
	public int P_totalAttack {get {return H_totalAttack;}}
	public int P_totalDefense {get {return H_totalDefense;}}
	protected float H_totalHealth;
	protected float H_currentHealth;
	protected float H_maxHealth;
	public int P_healthBonus;
	public float P_healthBonusPercent;
	protected float H_totalStamina;
	protected float H_currentStamina;
	protected float H_maxStamina;
	public int P_staminaBonus;
	public float P_staminaBonusPercent;
	protected float H_staminaRegenRate;
	protected float H_healthRegenRate;
	protected int H_totalAttack;
	protected int H_attackValue;
	public int P_attackBonus;
	public float P_attackBonusPercent;
	protected int H_totalDefense;
	protected int H_defenseValue;
	public int P_defenseBonus;
	public float P_defenseBonusPercent;

	// Attack elements stats
	public float P_normalResistance {get {return H_normalResistance;}}
	public float P_fireResistance {get {return H_fireResistance;}}
	public float P_iceResistance {get {return H_iceResistance;}}
	public float P_poisonResistance {get {return H_poisonResistance;}}
	public float P_lightningResistance {get {return H_lightningResistance;}}
	protected float H_normalResistance;
	protected float H_fireResistance;
	protected float H_iceResistance;
	protected float H_poisonResistance;
	protected float H_lightningResistance;
	
	// Character general stats
	public int P_currentEssence{ get {return H_currentEssence;}}
	protected int H_currentEssence;
	protected int H_currentLevel;

		
	// Attack variables
	public AttackObject P_attack;
	public AttackObject P_currentAttack;
	protected float H_attackTimeCounter;
	protected bool H_inCombat;
	protected bool H_hasAttacked;
	protected bool H_inputNextAttack;
	protected bool H_inputNextSmash;
	protected int H_attackCount;
	protected int H_smashCount;
	
	// Active Skills variable
	protected ActiveSkill H_skillQ;
	protected ActiveSkill H_skillE;
	protected ActiveSkill H_skillF;		
	protected int H_currentSkill; // A value of 0 is Q, 1 is E, 2 is F.

	// Animation variables
	public GameObject P_hitParticle;
		
	// Dash variables
	protected Vector2 H_dashDirection;
	protected int H_dashCost;
	protected float H_dashTimeCounter;
	protected float H_dashSpeed;
	protected bool H_invulnerability;
	protected bool H_canDash;

	// Use this for initialization
	public override void Awake () {
		base.Awake();
		// These variables are initialized the same way for each
		// character/monster.
		H_dashDirection = new Vector2 (1.0F, 0.0F);
		H_dashTimeCounter = 0.0F;
		H_invulnerability = false;
		H_canDash = true;
		H_canAction = true;
		H_hasAttacked = false;
		H_inputNextAttack = false;
		H_inputNextSmash = false;
		H_inCombat = true;
		H_attackCount = 0;
		H_smashCount = 0;
		H_attackTimeCounter = 0.0F;
		// Bonuses modified by status effects. They all begin at 0.
		P_healthBonus = 0;
		P_healthBonusPercent = 0F;
		P_staminaBonus = 0;
		P_staminaBonusPercent = 0F;
		P_attackBonus = 0;
		P_attackBonusPercent = 0F;
		P_defenseBonus = 0;
		P_defenseBonusPercent = 0F;
		// These variables are unique to each character/monster and
		// should be initialized correctly for each.		
		// Health and Stamina
		H_maxHealth = 100F;
		H_currentHealth = H_maxHealth;
		H_maxStamina = 100F;
		H_currentStamina = H_maxStamina;
		// Attack and defense
		H_attackValue = 100;
		H_defenseValue = 50;
		// Exp and level
		H_currentEssence = 0;
		H_currentLevel = 1;
		// Movement
		H_dashCost = 15;
		H_maxSpeed = 3F;
		H_minSpeed = 1F;
		H_acceleration = 0.1F;
		// Regeneration
		H_staminaRegenRate= 10F;
		H_healthRegenRate = 2F;
		// Resistances
		H_normalResistance    = 1F;
		H_fireResistance      = 1F;
		H_lightningResistance = 1F;
		H_iceResistance       = 1F;
		H_poisonResistance 	  = 1F;
	}
	public virtual void PerformSkill(){
		if (H_currentSkill == 0){
			H_skillQ.performSkill();
		}else if(H_currentSkill == 1){
			H_skillE.performSkill();
		}else if(H_currentSkill == 2){
			H_skillF.performSkill();
		}
	}

	public virtual void PerformDash(){}

	public virtual void PerformAttack_1 (){	}
	public virtual void PerformAttack_2 (){	}
	public virtual void PerformAttack_3 (){	}
	public virtual void PerformAttack_4 (){	}
	public virtual void PerformAttack_5 (){	}
	
	public virtual void PerformSmash_0_1 (){ }
	public virtual void PerformSmash_1_1 (){ }
	public virtual void PerformSmash_2_1 (){ }
	public virtual void PerformSmash_3_1 (){ }
	public virtual void PerformSmash_4_1 (){ }
	public virtual void PerformSmash_5_1 (){ }

	public virtual void PerformAttacks (){

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
			PerformAttack_4();	
			break;
		case 4 : 
			PerformAttack_5();	
			break;
		default :
			break;
			
		}
	}
	public virtual void PerformSmashes(){
		
		switch (H_attackCount) {
			
		case 0 :
			PerformSmash_0_1();	
			break;
		case 1 : 
			PerformSmash_1_1();	
			break;
		case 2 : 
			PerformSmash_2_1();	
			break;
		case 3 : 
			PerformSmash_3_1();	
			break;
		case 4 : 
			PerformSmash_4_1();		
			break;
		default :
			break;
			
		}
	}

	public virtual void UpdateStats(){
		H_totalAttack = (int)(H_attackValue + H_attackValue * P_attackBonusPercent + P_attackBonus);
		H_totalDefense = (int)(H_defenseValue + H_defenseValue * P_defenseBonusPercent + P_defenseBonus);
		H_totalHealth = H_maxHealth + H_maxHealth * P_healthBonusPercent + P_healthBonus;
		H_totalStamina = H_maxStamina + H_maxStamina * P_staminaBonusPercent + P_staminaBonus;
	}

	public virtual void Heal ( float value, bool percent = false){
		if (percent){
			float healing = H_maxHealth * value;
			if (H_currentHealth + healing > H_maxHealth){
				H_currentHealth = H_maxHealth;
			} else {
				H_currentHealth += healing;
			}
			BattleInterface.Instance.ShowHealing(healing, this);
		} else {
			if (H_currentHealth + value > H_maxHealth){
				H_currentHealth = H_maxHealth;
			} else {
				H_currentHealth += value;
			}
			BattleInterface.Instance.ShowHealing(value, this);
		}
	}
	
	public virtual bool CheckStamina(float staminaCost){
		if (H_currentStamina - staminaCost > 0){
			H_currentStamina -= staminaCost;
			return true;
		} else {
			return false;
		}
	} 

	public virtual void BeginAttackState(ref AttackObject currentAttack, float duration, float multiplier, float distance = 0.3F){
		UpdateStats();
		currentAttack.transform.parent = transform;
		currentAttack.RotateAroundParent(0F, 0F, 0F, H_objectDirection, distance);
		currentAttack.InitializeAttack (duration, multiplier, P_totalAttack);
		H_canDash = false;
		H_canAction = false;
	}
	
	public virtual void ResetAttackState(){
		H_attackCount = 0;
		H_attackTimeCounter = 0.0F;
		H_canDash = true;
		H_canAction = true;
		H_inputNextAttack = false;
		H_inputNextSmash = false;
		H_hasAttacked = false;
		H_ObjectState = ObjectState.Idle;
		
	}

	public virtual void Dash (Vector2 newDirection){
		if (Mathf.Abs(newDirection.x) >= Utils.eps || Mathf.Abs(newDirection.y) >= Utils.eps) {
			H_dashDirection = newDirection;
		}else{
			H_dashDirection = new Vector2 (0.0F, -1.0F);
		}
		H_ObjectState = ObjectState.Dashing;
		H_canDash = false;
		H_canAction = false;
	}


	
	//HP and ATTACK RELATED STUFF
	//public List<Attack> _recentAttacks;
	public virtual void OnTriggerEnter2D (Collider2D col)
	{
		//Debug.Log(col.name);
		if(col.tag == "Attack")
		{
			if(col.transform.parent != transform)
				IsAttacked (col.transform.GetComponent<AttackObject>(), col.transform.position);
		}
	}	

	// The object has just been hit with an attack. 
	public virtual void IsAttacked(AttackObject atk, Vector3 colPosition)
	{
		if(atk.ValidateAttack(this))
		{
			bool blocked = false;
			if(H_ObjectState == ObjectState.Blocking)
			{
				//Compare Positions, to see if succesful guard
				Vector2 oppDir = atk.transform.parent.position - transform.position;
				if(Vector2.Angle(H_objectDirection,oppDir) < 46.0f)
				{
					blocked = true;
					Debug.Log("BLOCKED");
				}
			}
			if(!blocked && H_invulnerability == false){
				// This function calculates the damage dealt before applying resistances.
				float damageDealt = CalculateDamage(atk, H_defenseValue+P_defenseBonus+(int)(H_defenseValue*P_defenseBonusPercent));

				switch (atk.P_attackType){
				case AttackObject.AttackType.Normal : 
					damageDealt *= H_normalResistance;
					GameObject.Instantiate(P_hitParticle,colPosition,Quaternion.identity);
					break;
				case AttackObject.AttackType.Fire : 
					// A MODIFIER : créer une particule pour chaque type 
					// d'attaque!
					damageDealt *= H_fireResistance;
					GameObject.Instantiate(P_hitParticle,colPosition,Quaternion.identity);
					break;
				case AttackObject.AttackType.Ice : 
					// A MODIFIER : créer une particule pour chaque type 
					// d'attaque!
					damageDealt *= H_iceResistance;
					GameObject.Instantiate(P_hitParticle,colPosition,Quaternion.identity);
					break;
				case AttackObject.AttackType.Lightning : 
					// A MODIFIER : créer une particule pour chaque type 
					// d'attaque!
					damageDealt *= H_lightningResistance;
					GameObject.Instantiate(P_hitParticle,colPosition,Quaternion.identity);
					break;
				case AttackObject.AttackType.Poison : 
					// A MODIFIER : créer une particule pour chaque type 
					// d'attaque!
					damageDealt *= H_poisonResistance;
					GameObject.Instantiate(P_hitParticle,colPosition,Quaternion.identity);
					break;
				}
				BattleInterface.Instance.ShowDamage(damageDealt, this);
				if(atk.P_statusEffect != null){
					atk.P_statusEffect.Init(true, this, atk);
				}
				LoseHP(damageDealt);
			}
		}
		
	}
	public virtual void LoseHP(float amount)
	{
		H_currentHealth -= amount;
		if(H_currentHealth < 1)
			NoHP();
	}
	
	// The damage formula is as follow : 
	// (ATTACK VALUE - DEFENSE VALUE ) * ATTACK MULTIPLIER * RESISTANCE= DAMAGE DEALT
	// There is a lower limit of 10 * multiplier 
	// and an upper limit of 1000 * multiplier before resistances.
	public virtual float CalculateDamage(AttackObject atk, float defenseValue){
		
		float damageDealt = (float)(atk.P_attackValue - (H_defenseValue+P_defenseBonus+(int)(H_defenseValue*P_defenseBonusPercent)));
		if (damageDealt < 10F) {
			damageDealt = 10F;
		} else if (damageDealt > 1000){
			damageDealt = 1000F;
		}
		return damageDealt *  atk.P_multiplier;
	}

	public virtual void NoHP()
	{
		Destroy(gameObject);
	}

	// The player only regenerates stamina while walking,
	// idling or blocking. He regens at a third of the 
	// normal rate if blocking.
	public virtual void RegenerateStamina(){
		if (H_currentStamina < H_maxStamina){
			if(P_ObjectState != ObjectState.Attacking &&
			   P_ObjectState != ObjectState.Smashing  &&
			   P_ObjectState != ObjectState.Dashing   &&
			   P_ObjectState != ObjectState.Stun	  &&
			   P_ObjectState != ObjectState.Blocking  &&
			   P_ObjectState != ObjectState.Casting){
				H_currentStamina += H_staminaRegenRate * Time.deltaTime;
			}else if (P_ObjectState == ObjectState.Blocking){
				H_currentStamina += H_staminaRegenRate/3 * Time.deltaTime;
			}
		}
	}

	public virtual void RegenerateHealth(){
		if (H_currentHealth < H_maxHealth){
			H_currentHealth += H_healthRegenRate * Time.deltaTime;
		}
	}

	// Update is called once per frame
	public override void Update () {
		base.Update ();
		UpdateDirection();
		RegenerateStamina();
		RegenerateHealth();
		if (H_ObjectState == ObjectState.Idle)
		{
			H_animator.SetInteger("AnimationState",0);
		}else if (H_ObjectState == ObjectState.Walking){
			H_animator.SetInteger("AnimationState",999);
		}else if (H_ObjectState == ObjectState.Dashing) {
			PerformDash();
		}else if (H_ObjectState == ObjectState.Attacking) {
			PerformAttacks();
		}else if(H_ObjectState == ObjectState.Smashing) {
			PerformSmashes();
		}else if(H_ObjectState == ObjectState.PerformingSkill){
			PerformSkill();
		}
	}
}
