using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {


	public float time = 2.0f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
