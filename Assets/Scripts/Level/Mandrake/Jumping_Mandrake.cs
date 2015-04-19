using UnityEngine;
using System.Collections;

public class Jumping_Mandrake : MonoBehaviour
{
	public Transform[] Target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;
	
	public Transform Projectile;      
	private Transform myTransform;
	private GameObject skye;
	
	void Awake()
	{
		myTransform = transform;
		skye = GameObject.Find("Skye");
	}
	
	void Start()
	{          
	}
	
	
	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Log("I'M IN!!");
			//yield return new WaitForSeconds(1.5f);
			skye.GetComponent<TP_Controller> ().enabled = false;
			Projectile.transform.position = transform.position;

			for (int i = 0; i < Target.Length; ++i) {
				// Calculate distance to target
				float target_Distance = Vector3.Distance (Projectile.position, Target [i].position);
			
				// Calculate the velocity needed to throw the object to the target at specified angle.
				float projectile_Velocity = target_Distance / (Mathf.Sin (2 * firingAngle * Mathf.Deg2Rad) / gravity);
			
				// Extract the X  Y componenent of the velocity
				float Vx = Mathf.Sqrt (projectile_Velocity) * Mathf.Cos (firingAngle * Mathf.Deg2Rad);
				float Vy = Mathf.Sqrt (projectile_Velocity) * Mathf.Sin (firingAngle * Mathf.Deg2Rad);
			
				// Calculate flight time.
				float flightDuration = target_Distance / Vx;
			
				// Rotate projectile to face the target.
				Projectile.rotation = Quaternion.LookRotation (Target [i].position - Projectile.position);
			
				float elapse_time = 0;
			
				while (elapse_time < flightDuration) {
					Projectile.Translate (0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
				
					elapse_time += Time.deltaTime;
				
					yield return null;
				}
			}
			skye.GetComponent<TP_Controller> ().enabled = true;
		}
	}  
}