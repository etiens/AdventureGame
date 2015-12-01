using UnityEngine;
using System.Collections;

public class Limb : Enemy {
	Enemy _host;
	public void InitLimb(float hitPoints)
	{
		//_maxHitPoints = hitPoints;
		//_hitPoints = hitPoints;
	}
	public void SetHost(Enemy host)
	{
		_host = host;
	}
	public override void IsAttacked(AttackObject atk, Vector3 colPosition)
	{
		if(atk.ValidateAttack(this))
		{
			float value = 1.0f;
			LoseHP(value);
		}
	}
	public override void LoseHP(float amount)
	{
		Debug.Log(_host.transform.position);
		_host.LoseHP(amount);
		H_currentHealth -= amount;
		//Debug.Log (name+_currentHealth.ToString());
		if(H_currentHealth <= 0.0f)
			NoHP();
	}
	public override void NoHP()
	{
		_host.LimbDestroyed(this);
	}
}
