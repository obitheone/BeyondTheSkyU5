using UnityEngine;
using System.Collections;

public class Menu :  MonoBehaviour {

	public GUISkin skin;

	void OnGUI() {

		GUI.skin = skin;
		GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400));
			GUI.Box(new Rect(0, 0, 400, 400), "PROYECTO DE INVESTIGACION");
			
			GUI.Label (new Rect (40, 60, 320, 30), "CASO 1: Fuerzas del Personaje");

		GUI.Button (new Rect (40, 100, 160, 60), "Escena Basica");
			GUI.Button (new Rect (200, 100, 160, 60), "Escena Completa");

			GUI.Label (new Rect (40, 180, 320, 30), "CASO 2: Efectos de Agua");

			if(GUI.Button (new Rect (40, 220, 160, 60), "Escena Basica")){
				Application.LoadLevel("WavesGenerator");
			}
			GUI.Button (new Rect (200, 220, 160, 60), "Escena Completa");

			GUI.Button (new Rect (40, 320, 320, 60), "SALIR");
		GUI.EndGroup();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
