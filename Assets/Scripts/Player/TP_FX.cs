using UnityEngine;
using System.Collections;

public class TP_FX : MonoBehaviour {

	public static TP_FX Instance;
	public int groundType;
	public GameObject footprintwater;
	public GameObject footprintground;

	void OnControllerColliderHit (ControllerColliderHit hit){

		switch (hit.gameObject.tag) {
		case "ground":
			TP_Status.Instance.SetGround(1);
			break;
		case "water":
			TP_Status.Instance.SetGround(2);
			break;
		default:
			TP_Status.Instance.SetGround(0);
			break;
		}
	}

	void Update()
	{
		if (((TP_Motor.Instance.moveVector.z != 0) || (TP_Motor.Instance.moveVector.x != 0)) && (!TP_Status.Instance.IsJumping ())) {
			DrawFootPrints (TP_Status.Instance.GetGround ());
		} 
		else 
		{
			DrawFootPrints (0);
		}
	}

	void DrawFootPrints(int groundType)
	{

		switch (groundType)
		{
			case 1: 
				footprintground.GetComponent<ParticleSystem>().enableEmission=true;
				//footprintwater.GetComponent<ParticleSystem>().enableEmission=false;
				break;
			case 2: 
				footprintground.GetComponent<ParticleSystem>().enableEmission=false;
				//footprintwater.GetComponent<ParticleSystem>().enableEmission=true;
				break;
			default:
				footprintground.GetComponent<ParticleSystem>().enableEmission=false;
				//footprintwater.GetComponent<ParticleSystem>().enableEmission=false;
				break;
		}
	}
}
