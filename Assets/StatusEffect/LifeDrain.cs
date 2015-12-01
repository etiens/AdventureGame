using UnityEngine;
using System.Collections;

public class LifeDrain : StatusEffect {
		
	public override void Init (bool hasTimer, BattleObject host, AttackObject atk)
	{
		base.Init(hasTimer, host, atk);
		float totalDrain = (atk.P_attackValue - (host.P_totalDefense)) * atk.P_statusMultiplier;
		if (atk.P_statusTimer < Utils.eps){
			H_owner.Heal (totalDrain);
		}
		
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
