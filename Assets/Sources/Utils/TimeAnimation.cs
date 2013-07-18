
public class TimeAnimation {

	private float _current;
	private float _begin;
	private float _end;
	private float _collapse;
	
	public float Current {
		get { return _current; }
	}
	public float Begin {
		get { return _begin; }	
	}
	public float End {
		get { return _end; }	
	}
	public float Collapse {
		get { return _collapse; }	
		set { _collapse = value; }
	}
	
	public float Percent {
		get { return _end != 0 ? (_current - _begin) / (_end - _begin) : 0; }	
	}
	public float Interval {
		get { return _end - _begin; }	
	}
	
	public TimeAnimation(float collapse = 1) {
		_current = 0;
		_begin = 0;
		_end = 0;
		_collapse = collapse;
	}
		
	public float Update(float time = 1) {
		if (_current < _end) {
			_current += _collapse * time;
			if (_current > _end) {
				_current = _end;	
			}
		}
		return Percent;
	}
	
	public void Add(float val) {
		_end += val;
		if (_current > _end) {
			_current = _end;
		}
	}
	
	public void Reset() {
		_current = 0;
		_begin = 0;
		_end = 0;
	}
	
}
