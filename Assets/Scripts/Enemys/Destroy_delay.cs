using UnityEngine;
using System.Collections;

public class Destroy_delay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 1f);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !TP_Status.Instance.IsDead())
		{
				TP_Status.Instance.SubsVida(1);
				int temp=TP_Status.Instance.GetVida();
		}
	}
}
