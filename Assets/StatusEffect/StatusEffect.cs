using UnityEngine;
using System.Collections;

public class StatusEffect : MonoBehaviour {

	protected bool H_hasTimer = false;
	protected float H_timer;
	protected BattleObject H_host;
	protected BattleObject H_owner;
	protected int H_parentAttackValue;
	protected float H_multiplier;
	protected bool H_isInit = false;
	protected bool H_isPaused = false;

	public virtual void Init (bool hasTimer, BattleObject host, AttackObject atk){
		DontDestroyOnLoad(transform.gameObject);
		H_hasTimer = hasTimer;
		H_host = host;
		H_parentAttackValue = atk.P_attackValue;
		H_multiplier = atk.P_statusMultiplier;
		H_timer = atk.P_statusTimer;
		H_owner = atk.transform.parent.GetComponent<BattleObject>();
		H_isInit = true;

	}
	public void Pause()
	{
		H_isPaused = true;
	}
	public BattleObject GetHost()
	{
		return H_host;
	}
	public void ReInit(BattleObject newHost)
	{
		H_host = newHost;
		H_isPaused = false;
	}

	public virtual void EndStatus(){
		Destroy(this);
	}
	// Update is called once per frame
	public virtual void Update () {
		if (H_isPaused)
			return;
		if (H_hasTimer && H_isInit){
			H_timer -= Time.deltaTime;
			if(H_timer < Utils.eps){
				EndStatus();
			}
		}
	}
}
