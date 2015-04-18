using UnityEngine;
using System.Collections;

public class AgentScriptFlytaka : MonoBehaviour {

	public Transform target;
	public float chasingSpeed = 5.0f;
	public float patrolSpeed = 2.0f;
	public float patrolWaitTime = 1f;	// Tiempo que espera cuando alcanza un punto ruta.
	public float chasingWaitTime =2f;   //tiempo que le sigue cuando ya esta fuera de vision
	public Transform[] patrolWayPoints;	// Ruta que sigue el que apatrulla la ciudad
	public float chaseTimer = 0f;	// Tiempo que lleva persiguiendo.
	public GameObject tempshoot;	//la bala que dispara;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;

	private NavMeshAgent agent;
	private int wayPointIndex = 0;	// indice dle waypoint donde nos encontramos.
	private float patrolTimer = 0f;	// Tiempo que lleva esperando en el waypoint.la m

	/* STATES */
	private const int ATTACK=0;
	private const int CHASE=1;
	private const int PATROL=2;
	/**/

	private const int RANGE_ATTACK=0;
	private const int MEELE_ATTACK=1;

	private Status_Controller_Flytaka SC;
	private int state = 0;
	private int attacktype =0;


	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = TP_Skills.Instance.player.transform;
		SC = GetComponent<Status_Controller_Flytaka> () as Status_Controller_Flytaka;
	}

	void Update () {
		state = SC.state;

		switch (state) 
		{
			case PATROL: 
				Patrolling ();
			break;

			case CHASE: 
				ChasingFlytaka ();
			break;

			case ATTACK: 
				
				attacktype=SC.attack_type;
				
				switch (attacktype) {
					case RANGE_ATTACK:
							Range_attack();
						break;
					case MEELE_ATTACK: 
							meele_attack();
						break;
					default:
						break;
				}
				break;
			default:break;

		} 
	}

	void ChasingFlytaka()
	{
			agent.enabled = true;	
			agent.speed = chasingSpeed;
			agent.SetDestination (target.position);
	}

	void Patrolling ()
	{
		agent.enabled = true;
		agent.speed = patrolSpeed;

		if (agent.remainingDistance <= agent.stoppingDistance) {
			patrolTimer += Time.deltaTime;
			if (patrolTimer >= patrolWaitTime) {
				if (wayPointIndex == patrolWayPoints.Length - 1) {wayPointIndex = 0;}
				else { wayPointIndex++;}
				patrolTimer = 0;
			}
		} 
		else {
				patrolTimer = 0;
			}
		agent.destination = patrolWayPoints[wayPointIndex].position;
	}

	void Range_attack()
	{
		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		relativePos = rotation.eulerAngles;
		relativePos.x = 0f;
		rotation.eulerAngles = relativePos;
		transform.rotation = rotation;

		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			GameObject newProjectile = Instantiate( tempshoot,new Vector3( transform.position.x, transform.position.y+0.6f, transform.position.z), transform.rotation ) as GameObject;

			Vector3 direction =  (transform.position-target.position).normalized;

			newProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(0,0,5);
		}
	}
	void meele_attack()
	{
		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		relativePos = rotation.eulerAngles;
		relativePos.x = 0f;
		rotation.eulerAngles = relativePos;
		transform.rotation = rotation;
	
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			GameObject newProjectile = Instantiate( tempshoot, new Vector3( transform.position.x, transform.position.y+0.6f, transform.position.z), transform.rotation ) as GameObject;
			Vector3 direction =  (transform.position-target.position).normalized;
			newProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection( 0,0,5);
		}
	}

}
