using UnityEngine;
using System.Collections;

public class WavesGen : MonoBehaviour {

	public GameObject waves;

	void OnTriggerEnter(Collider other) {

		waves.GetComponent<ParticleSystem>().randomSeed = 30;

		Vector3 vec = other.transform.position;
		Instantiate (waves, vec, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

}
