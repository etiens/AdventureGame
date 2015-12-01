using UnityEngine;
using System.Collections;

public class Poison : StatusEffect {

	float _damageRate;

	public override void Init (bool hasTimer, BattleObject host, AttackObject atk)
	{
		base.Init(hasTimer, host, atk);
		float totalDamage = (atk.P_attackValue - (host.P_totalDefense)) * host.P_poisonResistance * atk.P_statusMultiplier;
		if (atk.P_statusTimer < Utils.eps){
			H_host.LoseHP(totalDamage);
		}else {
			_damageRate = totalDamage / atk.P_statusTimer;
		}

	}
	
	// Update is called once per frame
	public override void Update () {
		if (H_isPaused)
			return;
		base.Update();
		if (H_host != null){
			H_host.LoseHP(_damageRate * Time.deltaTime);
		}
	}
}
