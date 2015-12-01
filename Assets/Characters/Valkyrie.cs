using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Valkyrie : Personnage {
	
	// Use this for initialization
	public override void Awake () {
		base.Awake ();		
		// Health and Stamina
		H_maxHealth = 200;
		H_currentHealth = H_maxHealth;
		H_maxStamina = 120;
		H_currentStamina = H_maxStamina;
		// Attack and defense
		H_attackValue = 90;
		H_defenseValue = 75;
		P_attackBonus = 0;
		P_defenseBonus = 0;
		// Exp and level
		H_currentEssence = 0;
		H_currentLevel = 1;
		// Movement
		H_dashCost = 10;
		H_maxSpeed = 3.5F;
		H_minSpeed = 1.5F;
		H_acceleration = 0.12F;
		// Regeneration
		H_staminaRegenRate= 10F;
		H_healthRegenRate = 5F;
		// Resistances
		H_normalResistance    = 1F;
		H_fireResistance      = 1F;
		H_lightningResistance = 1F;
		H_iceResistance       = 1F;
		H_poisonResistance 	  = 1F;
		
	}
	public override void PerformDash (){
//		H_animator.SetInteger("AnimationState", 1000);
//		H_dashTimeCounter += Time.deltaTime;
//		if (H_dashTimeCounter <= 0.05F){
//			Move(H_dashDirection, 6F, 0.05F);
//		} else if ( H_dashTimeCounter <= 0.2F) {
//			Move(H_dashDirection, 12F, 0.2F);
//			H_invulnerability = true;
//		} else if (H_dashTimeCounter <= 0.25F) {
//			Move(H_dashDirection, 6F, 0.05F);
//			H_invulnerability = false;
//		} else if (H_dashTimeCounter > 0.33F) {
//			H_ObjectState = ObjectState.Idle;
//			H_dashTimeCounter = 0;
//			ResetAttackState();
//		}
	}
	
	public override void PerformAttack_1 (){
		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
		// the attack needs to be initialized.
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 1);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the character is in. 
			H_ObjectState = ObjectState.Attacking;
			H_canAction = false;
			H_canDash = false;
			H_inputNextAttack = false;
			H_inputNextSmash = false;
			// Build Up
		} else if (H_attackTimeCounter <= 0.205F){
			H_attackTimeCounter += Time.deltaTime;
			
			// The player cannot move, but he can dash out or continue the combo.
		} else if (H_attackTimeCounter <= 0.350F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.05F, 0.3F);
				P_currentAttack.Scale(0.4F, 1.3F);
				H_hasAttacked = true;
			}
			// If the player initiates another attack, move the combo count up and destroy the current attack.
			if(Input.GetMouseButtonDown(0)){
				H_inputNextAttack = true;
			}else if(Input.GetMouseButtonDown(1)){
				H_inputNextSmash = true;
			}
			H_canDash = true;
		}else if (H_attackTimeCounter <= 0.45F){
			H_attackTimeCounter += Time.deltaTime;
			if ((H_inputNextAttack || Input.GetMouseButtonDown(0)) && CheckStamina(10)){
				H_attackCount = 1;
				H_attackTimeCounter = 0.0F;
				P_currentAttack.endAttack();
				H_hasAttacked = false;
			}
			if ((H_inputNextSmash || Input.GetMouseButtonDown(1)) && CheckStamina(25)){
				H_attackCount = 1;
				H_attackTimeCounter = 0.0F;
				P_currentAttack.endAttack();
				H_hasAttacked = false;
				H_ObjectState = ObjectState.Smashing;
			}
		}else {
			ResetAttackState();
		}	
	}
	public override void PerformAttack_2 (){
		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
		// the attack needs to be initialized.
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 2);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the character is in. 
			H_ObjectState = ObjectState.Attacking;
			H_canAction = false;
			H_canDash = false;
			H_inputNextAttack = false;
			// Build Up
		} else if (H_attackTimeCounter <= 0.15F){
			H_attackTimeCounter += Time.deltaTime;
			
			// The player cannot move, but he can dash out or continue the combo.
		} else if (H_attackTimeCounter <= 0.25F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.1F, 0.45F);
				P_currentAttack.Scale(0.15F, 1.1F);
				P_currentAttack.RotateAroundParent(-70F, 48F, 0.1F, H_objectDirection);
				H_hasAttacked = true;
			}
			// If the player initiates another attack, move the combo count up and destroy the current attack.
			if(Input.GetMouseButtonDown(0)){
				H_inputNextAttack = true;
			}
			H_canDash = true;
		}else if (H_attackTimeCounter <= 0.45F){
			H_attackTimeCounter += Time.deltaTime;
			if ((H_inputNextAttack || Input.GetMouseButtonDown(0)) && CheckStamina(15)){
				H_attackCount = 2;
				H_attackTimeCounter = 0.0F;
				P_currentAttack.endAttack();
				H_hasAttacked = false;
			}
			if ((H_inputNextSmash || Input.GetMouseButtonDown(1)) && CheckStamina(40)){
				H_attackCount = 2;
				H_attackTimeCounter = 0.0F;
				P_currentAttack.endAttack();
				H_hasAttacked = false;
				H_ObjectState = ObjectState.Smashing;
			}
		}else {
			ResetAttackState();
		}
	}
	public override void PerformAttack_3 (){
		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
		// the attack needs to be initialized.
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 3);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the character is in. 
			H_ObjectState = ObjectState.Attacking;
			H_canAction = false;
			H_canDash = false;
			H_inputNextAttack = false;
			// Build Up
		} else if (H_attackTimeCounter <= 0.20F){
			H_attackTimeCounter += Time.deltaTime;
			
			// The player cannot move, but he can dash out or continue the combo.
		} else if (H_attackTimeCounter <= 0.30F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				Move (-H_objectDirection, 5.0F, 0F);
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.1F, 0.3F);
				P_currentAttack.Scale(0.15F, 1.1F);
				P_currentAttack.RotateAroundParent(-70F, 48F, 0.1F, H_objectDirection);
				// Spawn a second attack.
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.1F, 0.3F);
				P_currentAttack.Scale(0.15F, 1.1F);
				P_currentAttack.RotateAroundParent(70F, -48F, 0.1F, H_objectDirection);
				H_hasAttacked = true;
			}
			// If the player initiates another attack, move the combo count up and destroy the current attack.
			if(Input.GetMouseButtonDown(0)){
				H_inputNextAttack = true;
			}
			H_canDash = true;
		}else if (H_attackTimeCounter <= 0.45F){
			H_attackTimeCounter += Time.deltaTime;
			if ((H_inputNextAttack || Input.GetMouseButtonDown(0)) && CheckStamina(10)){
				H_attackCount = 0;
				H_attackTimeCounter = 0.0F;
				P_currentAttack.endAttack();
				H_hasAttacked = false;
			}
		}else {
			ResetAttackState();
		}
	}
	public override void PerformAttack_4 (){	
	}
	public override void PerformSmash_1_1 () {
		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
		// the attack needs to be initialized.
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 11);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the character is in. 
			H_ObjectState = ObjectState.Smashing;
			H_canAction = false;
			H_canDash = false;
			H_inputNextAttack = false;
			P_currentAttack = Instantiate(P_attack) as AttackObject;
			BeginAttackState(ref P_currentAttack, 0.2F, 0.2F, 0.5F);
			P_currentAttack.Scale(0.8F, 0.8F);
			P_currentAttack.RotateAroundParent(0F, 15F, 0F, H_objectDirection, 0.5F);
			gameObject.AddComponent<LifeDrain>();
			P_currentAttack.AddStatusEffect(0F, gameObject.GetComponent<LifeDrain>(), 0.5F);
			H_hasAttacked = true;
			// Build Up
		} else if (H_attackTimeCounter <= 0.45F){
			H_attackTimeCounter += Time.deltaTime;
			H_canDash = true;
			
			// The player cannot move, but he can dash out or continue the combo.
		} else if (H_attackTimeCounter <= 0.7F){
			H_attackTimeCounter += Time.deltaTime;
		}else {
			ResetAttackState();
		}	
	}
	public override void PerformSmash_2_1 ()
	{
		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
		// the attack needs to be initialized.
		if (H_attackTimeCounter < Utils.eps){
			// Begin the animation
			H_animator.SetInteger("AnimationState", 21);
			// Measure the elapsed time.
			H_attackTimeCounter += Time.deltaTime;
			// These boolean variables describe the state the character is in. 
			H_ObjectState = ObjectState.Smashing;
			H_canAction = false;
			H_canDash = false;
			H_inputNextAttack = false;
			H_inputNextSmash = false;
			// Build Up
		} else if (H_attackTimeCounter <= 0.35F){
			H_attackTimeCounter += Time.deltaTime;
			
			// The player cannot move, but he can dash out or continue the combo.
		} else if (H_attackTimeCounter <= 0.7F){
			H_attackTimeCounter += Time.deltaTime;
			// Initialize the attack.
			if (!H_hasAttacked){
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.15F, 0.6F, 0.3F);
				P_currentAttack.Scale(0.15F, 1.4F);
				P_currentAttack.RotateAroundParent(10F, 130F, 0.15F, H_objectDirection, 0.3F);
				// Spawn a second attack.
				P_currentAttack = Instantiate(P_attack) as AttackObject;
				BeginAttackState(ref P_currentAttack, 0.15F, 0.6F, 0.3F);
				P_currentAttack.Scale(0.15F, 1.4F);
				P_currentAttack.RotateAroundParent(-10F, -130F, 0.15F, H_objectDirection, 0.3F);
				H_hasAttacked = true;
			}
		}else {
			ResetAttackState();
		}
	}
	

	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
	}
}







