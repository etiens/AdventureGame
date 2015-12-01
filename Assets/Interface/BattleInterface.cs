using UnityEngine;
using System.Collections;

public class BattleInterface : Singleton<BattleInterface> {


	Personnage _lePersonnage;


	public GUIText P_stamina;
	public GUIText P_health;
	public GUIText P_essence;
	public DamageDealt P_DamageDealt;
	public DamageDealt P_currentDamageDealt;

	public void Initialize() {
		_lePersonnage = (Personnage)GlobalScript.Instance.LePersonnage;
		if (_lePersonnage.GetComponent<BloodMage>() == null){
			P_stamina.enabled = true;
		}
		P_health.enabled = true;
		P_essence.enabled = true;
	}

	public void UnInitialize(){
		P_stamina.enabled = false;
		P_health.enabled = false;
		P_essence.enabled = false;
	}
	
	public void ShowDamage(float damageDealt, BattleObject parent){		
		P_currentDamageDealt = Instantiate(P_DamageDealt) as DamageDealt;
		P_currentDamageDealt.Init(damageDealt, parent, false);
	}
	public void ShowHealing(float damageDealt, BattleObject parent){		
		P_currentDamageDealt = Instantiate(P_DamageDealt) as DamageDealt;
		P_currentDamageDealt.Init(damageDealt, parent, true);
	}

	void Update() {		
		if (_lePersonnage != null){
			P_stamina.text = "Stamina : " + _lePersonnage.P_currentStamina.ToString() + "/" + _lePersonnage.P_totalStamina.ToString();
			P_health.text =  "Health  : " + _lePersonnage.P_currentHealth.ToString() + "/" + _lePersonnage.P_totalHealth.ToString();
			P_essence.text =  "Essence : " + _lePersonnage.P_currentEssence.ToString();
		} else {
			P_stamina.enabled = false;
			P_health.enabled = false;
			P_essence.enabled = false;
		}

	}
}
