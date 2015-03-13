using UnityEngine;
using System.Collections;

public class SK_LiftingHook : MonoBehaviour {

	private Vector3 _prevPosition;
	private Vector3 _startPoint;
	private float _duration=1.5f;
	private float _startTime;
	private float _temp_gravity;

	public Vector3 hitpoint;


	// Use this for initialization
	void Start () {
		_temp_gravity = TP_Motor.Instance.gravity;
		TP_Motor.Instance.gravity = 0;
		_startPoint = transform.position; 
		_startTime = Time.time; 
	}
	void OnEnable ()
	{

		_temp_gravity = TP_Motor.Instance.gravity;
		TP_Motor.Instance.gravity = 0;
		_startPoint = transform.position; 
		_startTime = Time.time; 

	}
	void OnDisable()
	{
		TP_Motor.Instance.gravity=_temp_gravity;
	}
	void Update () {
		_prevPosition = transform.position;
		transform.position = Vector3.Lerp(_startPoint, hitpoint, (Time.time - _startTime) / _duration); 
	}

}
