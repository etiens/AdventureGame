using UnityEngine;
using System.Collections;

public class DamageDealt : MonoBehaviour {

	float _timer;
	GUIText _currentText;
	bool _isInit = false;
	Vector2 _offSet = Vector2.zero;
	Vector3 _currentPos = Vector3.zero;
	BattleObject _parent;


	public void Init(float damageDealt, BattleObject parent, bool healing)
	{
		_parent = parent;
		_currentText = GetComponent<GUIText>();
		_isInit = true;
		_timer = 1.0F;
		_currentText.text = ((int)damageDealt).ToString();
		_currentText.transform.position = _parent.transform.position;
		if (healing){
			_currentText.color = new Color(0F,1F,0F,1F);
		} else {
			_currentText.color = new Color(0.6F, 0F, 0F, 1F);
		}
	}

	// Update is called once per frame
	void Update () {
		if (_isInit ){
			_timer -= Time.deltaTime;
			if (_timer < Utils.eps){
				Destroy(gameObject);
			}else{
				if (_parent != null)
				{
					_currentPos = _parent.transform.position;
				}else{
					_currentPos = _currentText.transform.position;
				}
				_offSet += new Vector2(0F, 0.1F *  Time.deltaTime);
				_currentText.transform.position = Camera.main.WorldToViewportPoint(_currentPos) + (Vector3)_offSet;
			}

		}
		}
}
