using UnityEngine;
using System.Collections;



public class SK_Push : MonoBehaviour {

	public float timeKill=5;
	public Vector3 direction;
	private float randomSpeed=700.0f;
	// Use this for initialization

	void Start () {
		transform.eulerAngles = new Vector3(0,180,0);
		timeKill = Time.time;
		//randomSpeed = Random.Range(900f,1000f);
		float translation  = randomSpeed;
		transform.GetComponent<Rigidbody>().AddForce(-direction.x*translation, -direction.y*translation, -direction.z*translation);
	}
	
	// Update is called once per frame
	void Update () {

		//transform.Translate (direction.x*translation, direction.y*translation, direction.z*translation);
		
		if ( Time.time > timeKill+ 6) Destroy (gameObject);

	}

	void OnDestroy()
	{
		if (TP_Skills.Instance._beamobject==this.gameObject) TP_Skills.Instance.deactivatetractorbeam();
	}
}
