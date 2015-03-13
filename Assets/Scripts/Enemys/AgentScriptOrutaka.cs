using UnityEngine;
using System.Collections;

public class AgentScriptOrutaka : MonoBehaviour {

	public Transform target;
	public float patrolSpeed = 2.0f;
	public float patrolWaitTime = 1f;	// Tiempo que espera cuando alcanza un punto ruta.
	public Transform[] patrolWayPoints;	// Ruta que sigue el que apatrulla la ciudad
	public float LookTimer = 0f;	// Tiempo que lleva observando.
	public int DamageAttack=25;
//La carga diabolica
	public float fireRate = 5f;
	private float nextFire = 0.0f;
	public float timeTakenDuringAttack = 0.1f;
	private bool _isAttacking;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private float _timeStartedLerping;
///

	private NavMeshAgent agent;
	private int wayPointIndex = 0;	// indice dle waypoint donde nos encontramos.
	private float patrolTimer = 0f;	// Tiempo que lleva esperando en el waypoint.la m
	
	private const int ATTACK=0;
	private const int LOOK=1;
	private const int PATROL=2;
	
	private const int CHARGE_ATTACK=0;
	
	private Status_Controller_Orutaka SC;
	private int state = 0;
	private int attacktype =0;
	private Transform LastPlayerPosition;


	
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = TP_Skills.Instance.player.transform;
		SC = GetComponent<Status_Controller_Orutaka> () as Status_Controller_Orutaka;
	}

	void FixedUpdate()
	{ 
		if(_isAttacking)
		{
			float timeSinceStarted = Time.time - _timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringAttack;
			
			transform.position = Vector3.Lerp (_startPosition, _endPosition, percentageComplete);
			
			if(percentageComplete >= 1.0f)
			{
				_isAttacking = false;
			}
		}

	}
	void Update () {
		state = SC.state;
		
		switch (state) 
		{
		case PATROL: 
			Patrolling ();
			break;
			
		case LOOK: 
			look ();
			break;
			
		case ATTACK: 
			
			attacktype=SC.attack_type;
			
			switch (attacktype) {
			case CHARGE_ATTACK:
				attack();
				break;
			default:
				break;
			}
			break;
		default:break;
			
		} 
	}

	void look()
	{
		if (!_isAttacking) {
				agent.enabled = false;
				//encarar hacia el player//
				Vector3 relativePos = target.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation (relativePos);
				transform.rotation = rotation;
		}
	}

	void Patrolling ()
	{
		if (!_isAttacking) {
				agent.enabled = true;
				agent.speed = patrolSpeed;

				if (agent.remainingDistance <= agent.stoppingDistance) {
						patrolTimer += Time.deltaTime;
						if (patrolTimer >= patrolWaitTime) {
								if (wayPointIndex == patrolWayPoints.Length - 1) {
										wayPointIndex = 0;
								} else {
										wayPointIndex++;
								}
								patrolTimer = 0;
						}
				} else {
						patrolTimer = 0;
				}
				agent.destination = patrolWayPoints [wayPointIndex].position;
		}
	}

	void attack()
	{
		if (Time.time > nextFire) {
						nextFire = Time.time + fireRate;
						agent.enabled = false;
						//Cargar de forma loca e inusitada hacia la posicion del player... corro como un loco!!!//
						_isAttacking = true;
						_timeStartedLerping = Time.time;
						_startPosition = transform.position;
						_endPosition = new Vector3(target.position.x,transform.position.y,target.position.z);	
				} 
		else {
				look();
			}
	}

	void OnTriggerEnter(Collider other)
	{

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.9f);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders[i].tag == "Player" && !TP_Status.Instance.IsDead())
			{
				TP_Status.Instance.SubsVida(DamageAttack);
				int temp=TP_Status.Instance.GetVida();
			}
			++i;
		}


	}
}
