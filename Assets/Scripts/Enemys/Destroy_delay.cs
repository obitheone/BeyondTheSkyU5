using UnityEngine;
using System.Collections;

public class Destroy_delay : MonoBehaviour {

	public float Destroy_time=1f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, Destroy_time);
	}

	void  OnCollisionEnter (Collision hit)
	{
		if (hit.gameObject.tag == "Player" && !TP_Status.Instance.IsDead())
		{
				TP_Status.Instance.SubsVida(1);
				//int temp=TP_Status.Instance.GetVida();
		}
	}
}
