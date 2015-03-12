using UnityEngine;
using System.Collections;



public class FX_Blackhole : MonoBehaviour {
	
	public float time = 5.0f;
	public float radius = 10.0f;
	public float power = -100f; //implosion
	
	private GameObject _particlehole;

	void Start () 
	{
	}
	
	void Update () 
	{
		time -= Time.deltaTime; 

		if (time > 0) {
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
			foreach (Collider hit in colliders) {
					if ((hit) && (hit.GetComponent<Rigidbody>())) {
							hit.GetComponent<Rigidbody>().AddExplosionForce (power, explosionPos, radius, 3);
					}
			}
		} 
		else {
			Destroy (_particlehole);
			Destroy (gameObject);
				}
		
	}
}


