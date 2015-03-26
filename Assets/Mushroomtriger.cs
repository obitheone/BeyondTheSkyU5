using UnityEngine;
using System.Collections;

public class Mushroomtriger : MonoBehaviour {

	private Animator Resizator;
	// Use this for initialization
	void Start () {
		Resizator = GetComponent<Animator>();
		//Resizator.Play("MushroomResize");
	}
	
	// Update is called once per frame
	void Update () {
		//Animator anim = GetComponent<Animator>();
		if (Input.GetKeyUp (KeyCode.P))
			Resizator.CrossFade ("MushroomResize", 0f);
		else if(Input.GetKey(KeyCode.O))
			Resizator.CrossFade("Idle", 0f);
	}
}
