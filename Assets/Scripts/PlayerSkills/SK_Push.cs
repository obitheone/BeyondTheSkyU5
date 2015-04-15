using UnityEngine;
using System.Collections;



public class SK_Push : MonoBehaviour {

	public float timeKill=5;
	public Vector3 direction;
	private float randomSpeed=6.0f;
	// Use this for initialization

	void Start () {
		transform.eulerAngles = new Vector3(0,180,0);
		timeKill = Time.time;
		randomSpeed = Random.Range(7.0f,9.0f);
	}
	
	// Update is called once per frame
	void Update () {
		float translation  = Time.deltaTime * randomSpeed;
		transform.Translate (direction.x*translation, direction.y*translation, direction.z*translation);
		
		if ( Time.time > timeKill+ 6) Destroy (gameObject);

	}
}
