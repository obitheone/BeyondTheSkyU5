using UnityEngine;
using System.Collections;

public class PROPS_Resize : MonoBehaviour {

	public Animator _anim;
	// Use this for initialization
	void Start () {
		//_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			Debug.Log ("Dentro");
			_anim.speed = 1.0f;
			_anim.enabled = true;

		}

	}
	void OnTriggerExit(Collider other) {

		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			_anim.speed =-1.0f;
		}

	}
}
