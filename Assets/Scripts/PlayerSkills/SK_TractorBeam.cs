using UnityEngine;
using System.Collections;

public class SK_TractorBeam : MonoBehaviour {

	public float speed =10.0f;
	public float offset_lateral=0f;
	public float offset_horizontal=0f;
	public float energy;
	public GameObject player;
	
	private Vector3 _prevPosition;
	// Use this for initialization
	void Start () {
		energy = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		_prevPosition = transform.position;
		float step = speed * Time.deltaTime;

		transform.position = Vector3.Lerp(transform.position, new Vector3 (
			(player.transform.right.x*offset_lateral)+(player.transform.position.x+player.transform.forward.x*2), 
			(2.0f+player.transform.position.y+offset_horizontal),
			(player.transform.right.z*offset_lateral)+(player.transform.position.z+player.transform.forward.z*2)), step); 
		
		} 

	void OnDisable()
	{
		GetComponent<Rigidbody>().useGravity=true;
		GetComponent<Rigidbody>().velocity =  20*(transform.position -_prevPosition) ;//Añadimos la incercia al finalizar el movimiento.
	}

	void OnEnable()
	{
		GetComponent<Rigidbody>().useGravity=false;
	}

}
