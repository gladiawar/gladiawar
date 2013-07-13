using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {
	
	private float _currentLife;
	private float _maxLife;
	
	public float CurrentLife {
		get { return _currentLife; }
		set {
			if (_timeAnimation != null) {
				_timeAnimation.Add(_currentLife - value);
			}
			_currentLife = value;
		}
	}
	public float MaxLife {
		get { return _maxLife; }
		set { _maxLife = value; }
	}
	
	public GameObject _background;
	public GameObject _life;
	public GameObject _damage;
	
	public TimeAnimation _timeAnimation;
	
	// Use this for initialization
	void Start () {
		_maxLife = 100;
		_currentLife = _maxLife;
		
		_life.transform.localPosition = new Vector3(_background.transform.localPosition.x, 0, 0);
		_life.transform.localScale = new Vector3(_background.transform.localScale.x, _background.transform.localScale.y, _background.transform.localScale.z);
		
		_damage.transform.localPosition = new Vector3(_background.transform.localPosition.x, 0, 0);
		_damage.transform.localScale = new Vector3(0, 0, 0);
		
		_timeAnimation = new TimeAnimation(10);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1) && CurrentLife > 0) {
			CurrentLife -= 10;	
		}
		
		float percent = _timeAnimation.Update(Time.deltaTime);
		float inverse = 1 - percent;
		
		if (percent != 1) {
			Vector3 scale = _background.transform.localScale;
			
			float lifeX = scale.x * _currentLife / _maxLife;
			_life.transform.localScale = new Vector3(lifeX, scale.y, scale.z);
			
			float damageX = lifeX + (scale.x * inverse * _timeAnimation.Interval / _maxLife);
			_damage.transform.localScale = new Vector3(damageX, scale.y, scale.z);
		}
		else {
				_timeAnimation.Reset();
				_damage.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		}
	}
	
}
