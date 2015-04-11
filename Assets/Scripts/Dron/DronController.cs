using UnityEngine;
using System.Collections;

public class DronController : MonoBehaviour {
	
    /*
	public Transform target;
	public float xdistance = 1.0f;
	public float ydistance = 2.2f;
	public float zdistance = 1.0f;
	public float XDamping = 2.0f;
	public float YDamping = 1.0f;
	public float ZDamping = 2.0f;*/

    public Transform followPos;
    public Transform talkingPos;
	private NavMeshAgent agent;
	public float rotDamping = 3.0f;
    public float movDamping = 2.0f;
	void Start () {
		//agent = GetComponent<NavMeshAgent>();
		//agent.SetDestination(target.position);
	}

	
	void LateUpdate () {
        /*
		float wantedX = target.position.x + xdistance;
		float currentX = transform.position.x;
		
		float wantedY = target.position.y + ydistance;
		float currentY = transform.position.y;
		
		float wantedZ = target.position.z - zdistance;
		float currentZ = transform.position.z;
		
		currentX = Mathf.Lerp (currentX, wantedX, XDamping * Time.deltaTime);
		currentY = Mathf.Lerp (currentY, wantedY, YDamping * Time.deltaTime);
		currentZ = Mathf.Lerp (currentZ, wantedZ, ZDamping * Time.deltaTime);
         */

	/*if (agent.enabled) 
		{
				
			//agent.SetDestination(target.position);
			agent.SetDestination(new Vector3 (wantedX, wantedY, wantedZ));
				
		} 
     */
        transform.position = Vector3.Lerp(transform.position, followPos.position, movDamping * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, followPos.transform.rotation, rotDamping * Time.deltaTime);
	}


	void Update ()
	{
		/*float distancia = Vector3.Distance (target.position, transform.position);
		Debug.Log (distancia);
		if (distancia > 10)
						agent.enabled = false;
		else
						
			if (distancia<2) agent.enabled = true;*/
	}
	
}