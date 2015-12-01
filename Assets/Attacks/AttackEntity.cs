using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackEntity : MonoBehaviour{
	
	public int P_comboIndex { get { return _comboIndex;}}
	private int _comboIndex;
	
	public SmashEntity P_smash { get { return _smash;}}
	public AttackEntity P_nextAttack { get { return _nextAttack;}}
	private SmashEntity _smash;
	private AttackEntity _nextAttack;
	
	public bool P_interruptable { get { return _interruptable;}}
	private bool _interruptable;
	
	public float P_buildUpDuration { get { return _buildUpDuration;}}
	public float P_attackDuration { get { return _attackDuration;}}
	public float P_cooldownDuration { get { return _cooldownDuration;}}
	private float _buildUpDuration;
	private float _attackDuration;
	private float _cooldownDuration;

	private Personnage _personnage;



	public AttackEntity (){	}

	public void initialize(int index, SmashEntity smash, AttackEntity nextAttack, bool interruptable, float buildUp, float attackDuration, float cooldown, Personnage personnage){
		_comboIndex = index;
		_smash = smash;
		_nextAttack = nextAttack;
		_interruptable = interruptable;
		_buildUpDuration = buildUp;
		_attackDuration = attackDuration;
		_cooldownDuration = cooldown;
		_personnage = personnage;
	}

//	public void perform(){
//		// The attackTimerCounter measures the time since the combo has been started. If it's zero, then 
//		// the attack needs to be initialized.
//		if (H_attackTimeCounter < Utils.eps){
//			// Begin the animation
//			H_animator.SetInteger("AnimationState", 1);
//			// Measure the elapsed time.
//			H_attackTimeCounter += Time.deltaTime;
//			// These boolean variables describe the state the character is in. 
//			H_ObjectState = ObjectState.Attacking;
//			H_canAction = false;
//			H_canDash = false;
//			H_inputNextAttack = false;
//			H_inputNextSmash = false;
//			// Build Up
//		} else if (H_attackTimeCounter <= 0.205F){
//			H_attackTimeCounter += Time.deltaTime;
//			
//			// The player cannot move, but he can dash out or continue the combo.
//		} else if (H_attackTimeCounter <= 0.350F){
//			H_attackTimeCounter += Time.deltaTime;
//			// Initialize the attack.
//			if (!H_hasAttacked){
//				P_currentAttack = Instantiate(P_attack) as AttackObject;
//				BeginAttackState(ref P_currentAttack, 0.05F, 0.3F);
//				P_currentAttack.Scale(0.4F, 1.3F);
//				H_hasAttacked = true;
//			}
//			// If the player initiates another attack, move the combo count up and destroy the current attack.
//			if(Input.GetMouseButtonDown(0)){
//				H_inputNextAttack = true;
//			}else if(Input.GetMouseButtonDown(1)){
//				H_inputNextSmash = true;
//			}
//			H_canDash = true;
//		}else if (H_attackTimeCounter <= 0.45F){
//			H_attackTimeCounter += Time.deltaTime;
//			if ((H_inputNextAttack || Input.GetMouseButtonDown(0)) && CheckStamina(10)){
//				H_attackCount = 1;
//				H_attackTimeCounter = 0.0F;
//				P_currentAttack.endAttack();
//				H_hasAttacked = false;
//			}
//			if ((H_inputNextSmash || Input.GetMouseButtonDown(1)) && CheckStamina(25)){
//				H_attackCount = 1;
//				H_attackTimeCounter = 0.0F;
//				P_currentAttack.endAttack();
//				H_hasAttacked = false;
//				H_ObjectState = ObjectState.Smashing;
//			}
//		}else {
//			ResetAttackState();
//		}	
//	}
}

