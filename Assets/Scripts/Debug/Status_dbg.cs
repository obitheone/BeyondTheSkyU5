using UnityEngine;
using System.Collections;

public class Status_dbg : MonoBehaviour {
	private Skills modecamera;
	private string mode_str; 
	public GUIStyle blanc;
	public GUIStyle verd;
	public GUIStyle vermell;

void OnGUI () {
		// Make a background box
		//int life = TP_Status.Instance.GetVida();
		//Camera
		modecamera = TP_Camera.Instance.GetMode();

		switch (modecamera) {
			case Skills.Follow:
								mode_str = "Camera: Follow";
								break;
			case Skills.Libre:
								mode_str = "Camera: Libre";
								break;
			case Skills.Orbit:
								mode_str = "Camera: Orbit";
								break;
			case Skills.Dios:
								mode_str = "Camera: GOD";
								break;
			case Skills.Puntos:
								mode_str = "Camera: Puntos";
								break;
			case Skills.Cinema:
								mode_str = "Camera: Cinematica";
								break;
			case Skills.Targetting:
								mode_str = "Camera: Targetting";
								break;
		}
		GUI.Box(new Rect(10,10,170,200),"");
		GUI.Label(new Rect(20,40,150,20), mode_str,blanc);

		//Vida
		if(TP_Status.Instance.IsDead())GUI.Label(new Rect(20,80,150,20),"Vida: " + TP_Status.Instance.GetVida(),vermell);
		else GUI.Label(new Rect(20,80,150,20),"Vida: " + TP_Status.Instance.GetVida(),blanc);

		//Debug.Log("jumping= "+TP_Status.Instance.IsJumping());
		if(TP_Status.Instance.IsJumping()) GUI.Label(new Rect(20,100,150,20),"Jumping",verd);
		else GUI.Label(new Rect(20,100,150,20),"Jumping",blanc);

		//Re-jumping
		if(TP_Status.Instance.IsReJumping()) GUI.Label(new Rect(20,120,150,20),"Re-jumping",verd);
		else GUI.Label(new Rect(20,120,150,20),"Re-jumping",blanc);
		 
		//Is dead
		if(TP_Status.Instance.IsDead())GUI.Label(new Rect(20,140,150,20),"Is Dead",verd);
		else GUI.Label(new Rect(20,140,150,20),"Is Dead",blanc);

		//Targetting
		if(TP_Status.Instance.IsTargetting())GUI.Label(new Rect(20,160,150,20),"Targetting",verd);
		else GUI.Label(new Rect(20,160,150,20),"Targetting",blanc);

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel ("Menu_joc");
	}
	
}

	