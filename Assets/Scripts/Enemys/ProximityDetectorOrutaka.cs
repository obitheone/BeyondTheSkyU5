using UnityEngine;
using System.Collections;

public class ProximityDetectorOrutaka : MonoBehaviour {

	private Status_Controller_Orutaka SC;
	private bool inc_look_time;
	// Use this for initialization
	void Start () {
		SC = GetComponent<Status_Controller_Orutaka> () as Status_Controller_Orutaka;
		inc_look_time = false;
	}
	void LateUpdate()
	{
		if (inc_look_time) SC.lookingTime += Time.deltaTime;
	}
	void OnTriggerStay(Collider other) {
		
		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			SC.player_view=true;
			inc_look_time = true;
			SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
		}
	}
	void OnTriggerExit(Collider other) {
		
		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			SC.player_view=false;
			SC.lookingTime=0;
			inc_look_time = false;
			SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			SC.player_view=true;
			SC.lookingTime=0;
			inc_look_time = true;
			SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
		}
	}
}
