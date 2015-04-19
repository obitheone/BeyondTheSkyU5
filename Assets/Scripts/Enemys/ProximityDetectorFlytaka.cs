using UnityEngine;
using System.Collections;

public class ProximityDetectorFlytaka : MonoBehaviour {

	private Status_Controller_Flytaka SC;
	private bool inc_chase_time;
	// Use this for initialization
	void Start () {
		SC = GetComponent<Status_Controller_Flytaka> () as Status_Controller_Flytaka;
		inc_chase_time = false;
	}
	void LateUpdate()
	{
		if (inc_chase_time) SC.chasingTime += Time.deltaTime;
	}
	void OnTriggerStay(Collider other) {
			
		if ((other.gameObject.tag == "Player")||(other.gameObject.tag == "NPC")) {//aqui mirar si ponemos algun tag mas como rocas y tal .
				SC.player_view=true;
				SC.chasingTime=0;
				inc_chase_time = false;
				SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
			}
		}
	void OnTriggerExit(Collider other) {
		
		if ((other.gameObject.tag == "Player")||(other.gameObject.tag == "NPC")) {//aqui mirar si ponemos algun tag mas como rocas y tal .
			SC.player_view=false;
			inc_chase_time = true;
			SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
		}
	}
	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.tag == "Player")||(other.gameObject.tag == "NPC")) {//aqui mirar si ponemos algun tag mas como rocas y tal .
				SC.player_view=true;
				SC.chasingTime=0;
				inc_chase_time = false;
				SC.player_distace=Vector3.Distance(other.gameObject.transform.position,transform.position);
			}
		}
}
