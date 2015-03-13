using UnityEngine;
using System.Collections;

public class FinNivel : MonoBehaviour {

	private bool final= false;
	// Use this for initialization
	void Start () {
		final = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {//aqui mirar si ponemos algun tag mas como rocas y tal .
			final=true;
		}
	}

	Rect centerRectangle ( Rect someRect) { 
			someRect.x = ( Screen.width - someRect.width ) / 2; 
			someRect.y = ( Screen.height - someRect.height ) / 2;
		return someRect;
	}

	void OnGUI () {
		if (final) {

			//calculamos el centro de la pantalla
			// Make a background box
			GUI.Box (centerRectangle ( new Rect (10, 10, 100, 90)), "LEVEL END");

			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed

			// Make the second button.
			if (GUI.Button (centerRectangle (new Rect (20, 70, 80, 20)), "Main Screen")) {
				Application.LoadLevel("Menu_joc");
			}
		}
	}
}
